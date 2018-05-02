using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Api.Services;
using MirleOrdering.Api.ViewModels;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/helper")]
    public class HelperController : Controller
    {
        private readonly HelperService _helperService;
        private readonly AppService _appService;
        private readonly IHostingEnvironment _env;

        public HelperController(HelperService helperService, AppService appService, IHostingEnvironment env)
        {
            _helperService = helperService;
            _appService = appService;
            _env = env;
        }
        // GET: api/helper/roles-and-groups
        [HttpGet("roles-and-groups", Name = "GetRolesAndGroups")]
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

        // POST: api/helper/upload
        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                var result = _appService.Upload(file).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
