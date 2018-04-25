using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MirleOrdering.Api.ViewModels;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MirleOrdering.Api.Services
{
    public class AuthService
    {
        private IConfiguration _config;
        private IUserService _userService;

        public AuthService(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }
        public UserViewModel Authenticate(LoginModel login)
        {
            return _userService
                .Find(user => user.Email == login.Email && user.Password == login.Password)
                .FirstOrDefault();
        }

        public string BuildToken(UserViewModel user)
        {
            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("userName", user.UserName),
                new Claim("email", user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
                new Claim("guid", Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"], // issuer
                _config["Jwt:Issuer"], // audience 
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserViewModel GetUserFromClaimsPrincipal(ClaimsPrincipal claims)
        {
            if (claims.HasClaim(c => c.Type == "userId"))
            {
                string userName = claims.Claims.FirstOrDefault(c => c.Type == "userName").Value;
                long userId = long.Parse(claims.Claims.FirstOrDefault(c => c.Type == "userId").Value);
                return _userService.GetById(userId);
            }
            return null;
        }
    }
}
