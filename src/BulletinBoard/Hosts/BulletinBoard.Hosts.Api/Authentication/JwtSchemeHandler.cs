using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace BulletinBoard.Hosts.Api.Authentication
{
    public class JwtSchemeHandler : AuthenticationHandler<JwtSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Обрвботчик аутентификации с помощью JWT-токена (проверка наличия токена и его валидность).
        /// Если токен валидный, то аутентифицируем пользователя.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        public JwtSchemeHandler(
            IOptionsMonitor<JwtSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Получение токена из заголовка (COOKIES)
            var tokenPresented = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("jwt", out var token);

            // Проверяем наличие токена, если токена нет - не можем аутентифицировать пользователя
            if (!tokenPresented)
            {
                return Task.FromResult(AuthenticateResult.Fail("Token not found"));
            }

            // Формируем токен
            var handler = new JwtSecurityTokenHandler();

            var parts = token.Split(".".ToCharArray());

            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];

            var bytesToSign = Encoding.UTF8.GetBytes($"{header}.{payload}");
            var secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var alg = new HMACSHA256(secret);
            var hash = alg.ComputeHash(bytesToSign);

            // Вычисляем подпись
            var calculatedSignature = Base64UrlEncode(hash);

            // Сравниваем подпись с подписью из токена
            if (calculatedSignature.Equals(signature))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is invalid"));

            }

            // Забираем данные пользователя из токена

            var jwtToken = handler.ReadJwtToken(token);

            // Аутентифицируем пользователя

            var claims = jwtToken.Claims;
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private static string Base64UrlEncode(byte[] bytes)
        {
            var value = Convert.ToBase64String(bytes);

            value = value.Split("=")[0];
            value = value.Replace('+', '-');
            value = value.Replace('/', '_');

            return value;
        }
    }
}
