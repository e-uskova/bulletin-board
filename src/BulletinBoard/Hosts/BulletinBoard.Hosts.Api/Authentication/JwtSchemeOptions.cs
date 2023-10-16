using Microsoft.AspNetCore.Authentication;

namespace BulletinBoard.Hosts.Api.Authentication
{
    public class JwtSchemeOptions : AuthenticationSchemeOptions
    {
        public bool IsActived { get; set; }
    }
}
