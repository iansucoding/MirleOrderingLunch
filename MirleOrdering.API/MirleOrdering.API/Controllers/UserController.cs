using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_userService.GetAll());
        }

        // GET api/user/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(long id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        // POST: api/user
        [HttpPost]
        public IActionResult Create([FromBody]UserBaseModel user)
        {
            if (user == null)
            {
                return BadRequest("user is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _userService.Create(user);
            if (result.IsSuccess)
            {
                return CreatedAtRoute("GetUser", new { id = long.Parse(result.Message) }, new { userId = result.Message });
            }
            return BadRequest(result);
        }
        // PUT api/user/5
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Update(int id, [FromBody]UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("user is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _userService.Update(user);
            if (result.IsSuccess)
            {
                return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
            }
            return BadRequest(result);
        }
        // DELETE api/user/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(_userService.Delete(id));
        }
    }
}
