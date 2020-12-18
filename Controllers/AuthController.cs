using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using spokie.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace spokie.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       [HttpPost, Route("login")]
       public IActionResult Login([FromBody] LoginModel user)
        {
            if(user == null)
            {
                return BadRequest("Invalid client request");
            }

            if(user.UserName == "Spokie" && user.Password == "city!123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecret!2020"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Manager")
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            } else
            {
                return Unauthorized();
            }
        }
    }
}
