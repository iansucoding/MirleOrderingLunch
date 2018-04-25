using Microsoft.EntityFrameworkCore;
using MirleOrdering.Data.Models;
using MirleOrdering.Repo;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MirleOrdering.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }
        protected OrderViewModel ConvertToViewModel(Order order) => new OrderViewModel
        {
            OrderId = order.Id,
            UserId = order.UserId,
            UserName = order.User == null ? null : order.User.Name,
            ProductId = order.ProductId,
            ProductName = order.Product == null ? null : order.Product.Name,
            ProductPrice = order.Product == null ? (int?)null : order.Product.Price,
            Amount = order.Amount,
            OrderedOn = order.AddedOn.Date
        };

        public OrderViewModel GetById(long id)
        {
            var entity = _repository.GetById(id);
            if (entity != null)
            {
                return ConvertToViewModel(entity);
            }
            return null;
        }
        public IEnumerable<OrderViewModel> GetAll()
        {
            return _repository.GetAll().Select(x => ConvertToViewModel(x));
        }
        public IEnumerable<OrderViewModel> Find(Func<Order, bool> predicate)
        {
            return _repository.GetQueryable().Where(predicate).Select(x => ConvertToViewModel(x));
        }

        public ReturnViewModel Create(OrderBaseModel model)
        {
            var result = new ReturnViewModel();
            var entity = new Order
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Amount = model.Amount,
                AddedOn = DateTime.Now
            };
            try
            {
                _repository.Create(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            result.Message = entity.Id.ToString();
            result.IsSuccess = true;
            return result;
        }
        public ReturnViewModel Update(OrderViewModel model)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(model.OrderId);
            if (entity == null)
            {
                result.Message = "setting not found";
                return result;
            }
            entity.Amount = model.Amount;
            entity.ModifiedOn = DateTime.Now;

            try
            {
                _repository.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

            result.IsSuccess = true;
            return result;
        }

        public ReturnViewModel Delete(long id)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                result.Message = "order not found";
                return result;
            }
            try
            {
                _repository.Delete(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            result.Message = id.ToString();
            result.IsSuccess = true;
            return result;
        }

        public ReturnViewModel DeleteTodayListByUser(long userId)
        {
            var result = new ReturnViewModel();
            var today = DateTime.Now.Date;
            var entities = _repository.Find(x => x.UserId == userId && x.AddedOn.Date == today);
            if (entities == null || !entities.Any())
            {
                result.Message = "user's order not found";
                return result;
            }
            try
            {
                foreach (var entity in entities)
                {
                    _repository.Delete(entity);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

            result.IsSuccess = true;
            return result;
        }
        public OrderViewModel GetViewOrderById(long orderId)
        {
            var result = _repository.GetQueryable()
                .Include(x => x.Product)
                .Where(x => x.Id == orderId).FirstOrDefault();
            return result == null ? null : ConvertToViewModel(result);
        }

        public IEnumerable<OrderViewModel> GetTodayOrders()
        {
            var date = DateTime.Now.Date;
            var result = _repository.GetQueryable()
                .Include(x => x.Product)
                .Where(x => x.AddedOn.Date == date)
                .OrderBy(x => x.UserId)
                .Select(x => ConvertToViewModel(x));
            return result;
        }

        public IEnumerable<OrderViewModel> GetUserOrders(long userId, bool isTodayOnly = false)
        {
            var today = DateTime.Now.Date;
            var result = _repository.GetQueryable()
                .Include(x => x.Product)
                .Where(x => x.UserId == userId && (isTodayOnly == false || x.AddedOn.Date == today))
                .Select(x => ConvertToViewModel(x));
            return result;
        }

        public IEnumerable<OrderViewModel> SearchOrders(string term, long? userId, DateTime? orderedStartOn, DateTime? orderedEndOn)
        {
            var query = _repository.GetQueryable()
                .Include(x => x.Product)
                .AsQueryable();
            if (string.IsNullOrEmpty(term))
            {
                query = query.Where(x => x.Product.Name.Contains(term));
            }
            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId);
            }
            if (orderedStartOn.HasValue && orderedEndOn.HasValue)
            {
                query = query.Where(x => x.AddedOn >= orderedStartOn.Value && x.AddedOn <= orderedEndOn.Value);
            }
            var result = query.ToList();
            return result == null ? null : result.Select(x => ConvertToViewModel(x));
        }
    }
}
