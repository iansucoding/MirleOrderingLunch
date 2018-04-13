using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Api.Services;
using MirleOrdering.Service.ViewModels;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class HelperController : Controller
    {
        private readonly HelperService _helperService;

        public HelperController(HelperService helperService)
        {
            _helperService = helperService;
        }
        // GET: api/helper/roles-and-groups
        [HttpGet("roles-and-groups",Name = "GetRolesAndGroups")]
        public Dictionary<string, IEnumerable<SelectionViewModel>> GetRolesAndGroups()
        {
            var dict = new Dictionary<string, IEnumerable<SelectionViewModel>>();
            dict.Add("roles", _helperService.GetRoleSelection());
            dict.Add("groups", _helperService.GetGroupSelection());
            return dict;
        }
        // GET: api/helper/categories
        [HttpGet("categories", Name = "GetCategories")]
        public IEnumerable<SelectionViewModel> GetCategories()
        {
            return _helperService.GetCategories();
        }
    }
}
