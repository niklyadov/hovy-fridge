﻿using HovyFridge.Data.Entity;
using HovyFridge.Extensions;
using HovyFridge.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace HovyFridge.Web.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UsersService usersService
            )
    : base(options, logger, encoder, clock)
        {
            _usersService = usersService;
        }

        private readonly UsersService _usersService;

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
            }

            // Get authorization key
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var authHeaderRegex = new Regex(@"Basic (.*)");

            if (!authHeaderRegex.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
            }

            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
            var authUsername = authSplit[0];
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");

            var authUserResult = _usersService.GetUserByUsername(authUsername);

            if (!authUserResult.TryGetResult(out var authUser))
            {
                return Task.FromResult(AuthenticateResult.Fail("User is not found."));
            }

            if (authUser.PasswordHash != authPassword)
            {
                return Task.FromResult(AuthenticateResult.Fail("Incorrect username or password."));
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, authUser.Name),
                new Claim(ClaimTypes.Role, authUser.UserRole.GetName<UserRole>() ?? "")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            _usersService.CurrentUser = authUser;

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}