//namespace HovyFridgeApi.Middleware;

//using HovyFridgeApi.Extensions;
//using HovyFridgeApi.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;

//public class Authorization
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger _logger;

//    private JwtTokensService? _tokensService = null;
//    private AuthService? _authService = null;

//    public Authorization(RequestDelegate next, ILogger<Authorization> logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task InvokeAsync(HttpContext context, JwtTokensService tokensService, AuthService authService)
//    {
//        _tokensService = tokensService;
//        _authService = authService;

//        var token = context.Request.Headers["Authorization"].ToString();

//        if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
//        {
//            await context.AccessDenied("Token is not passed.");
//            return;
//        }

//        var tokenBody = token.Substring(7);

//        var parsedToken = _tokensService.ParseToken(tokenBody);
//        if (parsedToken is not null)
//        {
//            var initUserResult = _authService.InitInstanceWithToken(parsedToken);
//            if (initUserResult.IsFailed)
//            {
//                await context.AccessDenied(string.Join(',', initUserResult.Errors));
//                return;
//            }

//            var userResult = await _authService.GetCurrentUser();
//            if (userResult.IsSuccess && await _tokensService.IsValidTokenAsync(userResult.Value, tokenBody))
//            {
//                await _next.Invoke(context);
//            }
//        }

//        await context.AccessDenied("The token is not correct.");
//    }
//}