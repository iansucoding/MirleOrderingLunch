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

    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        protected UserViewModel ConvertToViewModel(User user) => new UserViewModel
        {
            UserId = user.Id,
            UserName = user.Name,
            Email = user.Email,
            Balance = user.Balance,
            RoleId = user.RoleId,
            RoleName = user.RoleId.HasValue && user.Role != null ? user.Role.Name : null,
            GroupId = user.GroupId,
            GroupName = user.GroupId.HasValue && user.Group != null ? user.Group.Name : null,
        };

        public UserViewModel GetById(long id)
        {
            var user = _repository.GetQueryable()
                .Include(u => u.Group)
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return ConvertToViewModel(user);
            }
            return null;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _repository.GetQueryable()
                .Include(u => u.Group)
                .Include(u => u.Role).Select(user => ConvertToViewModel(user));
        }

        public IEnumerable<UserViewModel> Find(Func<User, bool> predicate)
        {
            return _repository.GetQueryable()
                .Include(u => u.Group)
                .Include(u => u.Role)
                .Where(predicate)
                .Select(user => ConvertToViewModel(user));
        }

        public ReturnViewModel Create(UserBaseModel model)
        {
            var result = new ReturnViewModel();
            var entity = new User
            {
                Name = model.UserName,
                Email = model.Email,
                Balance = model.Balance,
                RoleId = model.RoleId,
                GroupId = model.GroupId,
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

        public ReturnViewModel Update(UserViewModel model)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(model.UserId);
            if (entity == null)
            {
                result.Message = "user not found";
                return result;
            }
            entity.Name = model.UserName;
            entity.Email = model.Email;
            entity.Balance = model.Balance;
            entity.RoleId = model.RoleId;
            entity.GroupId = model.GroupId;
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
                result.Message = "user not found";
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
        public void AddUserBalance(long userId, int totalCost)
        {
            var user = _repository.GetById(userId);
            user.Balance += totalCost;
            _repository.Update(user);
        }

    }
}
