using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;

namespace MirleOrdering.Api.Services
{
    public class HelperService
    {
        private readonly IRoleService _roleService;
        private readonly IGroupService _groupService;
        private readonly ICategoryService _categoryService;

        public HelperService(IRoleService roleService, IGroupService groupService, ICategoryService categoryService)
        {
            _roleService = roleService;
            _groupService = groupService;
            _categoryService = categoryService;
        }

        public IEnumerable<SelectionViewModel> GetRoleSelection()
        {
            return _roleService.GetSelection();
        }
        public IEnumerable<SelectionViewModel> GetGroupSelection()
        {
            return _groupService.GetSelection();
        }

        internal IEnumerable<SelectionViewModel> GetCategories()
        {
            return _categoryService.GetSelection();
        }
    }
}
