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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        protected ProductViewModel ConvertToViewModel(Product product) => new ProductViewModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Description = product.Description,
            Price = product.Price,
            Seq = product.Seq,
            CategoryId = product.Category == null ? -1 : product.Category.Id,
            CategoryName = product.Category == null ? null : product.Category.Name
        };

        public ProductViewModel GetById(long id)
        {
            return ConvertToViewModel(_repository.GetById(id));
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return _repository.GetQueryable()
                .Include(x => x.Category)
                .Select(product => ConvertToViewModel(product));
        }

        public IEnumerable<ProductViewModel> Find(Func<Product, bool> predicate)
        {
            return _repository.GetQueryable()
                .Include(x => x.Category)
                .Where(predicate)
                .Select(product => ConvertToViewModel(product));
        }

        public ReturnViewModel Create(ProductBaseModel model)
        {
            var result = new ReturnViewModel();
            if (!model.CategoryId.HasValue)
            {
                result.Message = "CategoryId is null ?";
                return result;
            }
            var entity = new Product
            {
                Name = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Seq = model.Seq,
                CategoryId = model.CategoryId.Value,
                AddedOn = DateTime.Now.Date,
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
        public ReturnViewModel Update(ProductViewModel model)
        {
            var result = new ReturnViewModel();
            if (!model.CategoryId.HasValue)
            {
                result.Message = "CategoryId is null ?";
                return result;
            }
            var entity = _repository.GetById(model.ProductId);
            if (entity == null)
            {
                result.Message = "product not found";
                return result;
            }

            entity.Name = model.ProductName;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Seq = model.Seq;
            entity.CategoryId = model.CategoryId.Value;
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
                result.Message = "product not found";
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

            result.IsSuccess = true;
            return result;
        }
        public ReturnViewModel Patch(long id, string name, int price, string desc)
        {
            var result = new ReturnViewModel();
            if (string.IsNullOrEmpty(name))
            {
                result.Message = "product name is null or empty";
                return result;
            }
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                result.Message = "product not found";
                return result;
            }

            entity.Name = name;
            entity.Description = desc;
            entity.Price = price;
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

        public bool IsProductExisted(long id)
        {
            return _repository.GetById(id) != null;
        }
    }
}
