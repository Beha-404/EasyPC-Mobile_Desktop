using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Security.Claims;
using System.Linq;
using EasyPC.Services.Interfaces;

namespace EasyPC.API
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

            var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request!.Headers["Authorization"]!);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split(':');

            var username = credentials[0];
            var password = credentials[1];

            var user = _userService.Login(username, password);
            if (user == null)
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}