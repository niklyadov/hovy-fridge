using HovyFridge.Web.Services.Common;
using HovyFridge.Web.Models;
using HovyFridge.Web.Services;
using HovyFridge.Web.Services.UserNotifier;
using HovyFridge.Services.Auth;
using HovyFridge.Data.Entity;

namespace HovyFridge.Web.Services.CodeConfirmation
{
    public class CodeConfirmationService
    {
        private readonly ILogger _logger;

        private readonly IUserNotifierService _userNotifier;
        private readonly JwtTokensService _jwtTokensService;

        private const string TokenServiceName = "confirmation_service";

        public CodeConfirmationService(ILoggerFactory loggerFactory, IUserNotifierService userNotifier, JwtTokensService tokensService)
        {
            _logger = loggerFactory.CreateLogger<CodeConfirmationService>();
            _userNotifier = userNotifier;
            _jwtTokensService = tokensService;
        }

        internal async Task<bool> CheckConfirmation(User currentUser, string? confirmationToken)
        {
            if (string.IsNullOrEmpty(confirmationToken)) return false;
            return await _jwtTokensService.IsValidTokenAsync(currentUser, confirmationToken, TokenServiceName);
        }

        internal async void SendConfirmationAsync(User currentUser, string accessUrl, string actionName = "default")
        {
            var confirmationToken = _jwtTokensService.GenerateNewTokenForUser(currentUser, TokenServiceName);

            await _userNotifier.SendConfirmationLink(currentUser, string.Format(accessUrl, confirmationToken), 
                $"Please click here to confirm your action (for {actionName})");
        }
    }
}