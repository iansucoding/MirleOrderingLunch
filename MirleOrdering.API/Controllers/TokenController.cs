using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MirleOrdering.Api.Services;
using MirleOrdering.Api.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private AuthService _authService;

        public TokenController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            var user = _authService.Authenticate(login);
            if (user != null)
            {
                var token = _authService.BuildToken(user);
                return Ok(new { token, user });
            }
            return Unauthorized();
        }
    }
}
