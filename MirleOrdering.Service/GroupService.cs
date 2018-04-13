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
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _repository;

        public GroupService(IRepository<Group> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Group> GetAll()
        {
            return _repository.GetQueryable().Include(r => r.Users);
        }

        public ReturnViewModel Delete(long id)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                result.Message = "group not found";
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

        public IEnumerable<SelectionViewModel> GetSelection()
        {
            return _repository.GetAll().Select(role => new SelectionViewModel
            {
                Value = role.Id,
                Text = role.Name
            });
        }

        public GroupViewModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<GroupViewModel> IGenericService<Group, GroupViewModel, GroupBaseModel>.GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupViewModel> Find(Func<Group, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ReturnViewModel Create(GroupBaseModel model)
        {
            throw new NotImplementedException();
        }

        public ReturnViewModel Update(GroupViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
