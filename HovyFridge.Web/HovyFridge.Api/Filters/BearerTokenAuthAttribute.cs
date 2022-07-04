using HovyFridge.Api.Extensions;
using HovyFridge.Api.Services;
using HovyFridge.Services.Auth;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HovyFridge.Api.Filters
{
    public class BearerTokenAuthAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public BearerTokenAuthAttribute(string name, string value) =>
            (_name, _value) = (name, value);

        public override async void OnResultExecuting(ResultExecutingContext context)
        {
            var svc = context.HttpContext.RequestServices;

            var tokensService = svc.GetRequiredService<JwtTokensService>();
            var authService = svc.GetRequiredService<AuthService>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                await context.HttpContext.AccessDenied("Token is not passed.");
                return;
            }

            var tokenBody = token.Substring(7);

            var parsedToken = tokensService.ParseToken(tokenBody);
            if (parsedToken is not null)
            {
                var initUserResult = authService.InitInstanceWithToken(parsedToken);
                if (initUserResult.IsFailed)
                {
                    await context.HttpContext.AccessDenied(string.Join(',', initUserResult.Errors));
                    return;
                }

                var userResult = await authService.GetCurrentUser();
                if (userResult.IsSuccess && await tokensService.IsValidTokenAsync(userResult.Value, tokenBody))
                {
                    context.HttpContext.Response.Headers.Add(_name, _value);
                    base.OnResultExecuting(context);
                }
            }

            await context.HttpContext.AccessDenied("The token is not correct.");
        }
    }
}