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
    public class RoleService : IRoleService
    {
        private IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public ReturnViewModel Create(RoleBaseModel model)
        {
            throw new NotImplementedException();
        }

        public ReturnViewModel Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleViewModel> Find(Func<Role, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetQueryable().Include(r => r.Users);
        }

        public RoleViewModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectionViewModel> GetSelection()
        {
            return _roleRepository.GetAll().Select(role => new SelectionViewModel
            {
                Value = role.Id,
                Text = role.Name
            });
        }

        public ReturnViewModel Update(RoleViewModel model)
        {
            throw new NotImplementedException();
        }

        IEnumerable<RoleViewModel> IGenericService<Role, RoleViewModel, RoleBaseModel>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
