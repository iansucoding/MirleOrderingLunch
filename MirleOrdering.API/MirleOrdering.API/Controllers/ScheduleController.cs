using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.API.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        // GET: api/schedule
        [HttpGet]
        public IEnumerable<ScheduleViewModel> Get()
        {
            return _scheduleService.GetAll();
        }

        // GET api/schedule/range?startOn=&endOn=
        [HttpGet("range")]
        public IEnumerable<ScheduleViewModel> Get(DateTime startOn, DateTime endOn)
        {
            startOn = startOn.Date;
            endOn = endOn.Date;
            return _scheduleService.Find(x => x.AvailableOn >= startOn && x.AvailableOn <= endOn);
        }

        // GET api/schedule/5
        [HttpGet("{id}",Name = "GetSchedule")]
        public ScheduleViewModel Get(int id)
        {
            return _scheduleService.GetById(id);
        }

        // GET api/schedule/today
        [HttpGet("today")]
        public CategoryViewModel GetTodaySchedule()
        {
            return _scheduleService.GetTodaySchedule();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ScheduleBaseModel model)
        {
            if (model == null)
            {
                return BadRequest("schedule is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _scheduleService.Create(model);
            if (result.IsSuccess)
            {
                var data = new
                {
                    ScheduleId = long.Parse(result.Message),
                    model.AvailableOn.Date,
                    model.Remark,
                    model.CategoryId
                };
                return CreatedAtRoute("GetSchedule", new { id = data.ScheduleId }, data);
            }
            return BadRequest(result);
        }

        // PUT api/schedule/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ScheduleViewModel model)
        {
            if (model == null)
            {
                return BadRequest("schedule is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _scheduleService.Update(model);
            if (result.IsSuccess)
            {
                model.AvailableOn = model.AvailableOn.Date;
                return CreatedAtRoute("GetSchedule", new { id = model.ScheduleId }, model);
            }
            return BadRequest(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_scheduleService.GetById(id) == null)
            {
                return NotFound();
            }
            return Json(_scheduleService.Delete(id));
        }
    }
}
