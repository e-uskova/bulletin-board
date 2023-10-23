using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly IUserService _userService;

        public TokenController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="dto">Модель данных для аутентификации/></param>
        /// <returns>Токен.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(AuthDto dto)
        {
            // Check user credentials id DB
            var user = await _userService.GetFirstWhere(u => u.Email == dto.Login && u.Password == dto.Password);
            if (user == null)
            {
                return BadRequest("Неверное имя пользователя или пароль");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Email, dto.Login),
                new Claim("Id", user.Id.ToString()),
            };

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
