using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.API.Controllers
{
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET: api/<controller>
        [HttpGet]
        public SettingViewModel Get()
        {
            return _settingService.GetLastOne();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SettingViewModel model)
        {
            if (model == null)
            {
                return BadRequest("product is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _settingService.Update(model);
            if (result.IsSuccess)
            {
                return CreatedAtRoute("GetUser", new { id = model.SettingId }, model);
            }
            return BadRequest(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_settingService.GetById(id) == null)
            {
                return NotFound();
            }
            return Json(_settingService.Delete(id));
        }
    }
}
