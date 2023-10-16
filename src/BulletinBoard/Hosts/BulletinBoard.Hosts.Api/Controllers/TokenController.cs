using BulletinBoard.Contracts.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BulletinBoard.Hosts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthDto dto)
        {
            // TODO Check user credentials id DB

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, dto.Login));
            claims.Add(new Claim("User", "User"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );

            return Ok(new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
    }
}
