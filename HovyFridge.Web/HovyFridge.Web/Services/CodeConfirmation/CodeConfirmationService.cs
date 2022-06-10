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

        internal async void SendConfirmationAsync(User currentUser, string accessUrl)
        {
            var confirmationToken = _jwtTokensService.GenerateNewTokenForUser(currentUser, TokenServiceName);

            await _userNotifier.SendConfirmationLink(currentUser, string.Format(accessUrl, confirmationToken));
        }

        //public async Task<ServiceResult<string>> SendConfirmationAsync(UserModel? user)
        //{
        //    if (user is null)
        //    {
        //        throw new InvalidOperationException("User is not provided");
        //    }

        //    if (user.ConfirmationToken is null)
        //    {
        //        throw new InvalidOperationException("ConfirmationToken is not null");
        //    }

        //    if (user.ConfirmationToken.LifeUntill < DateTime.Now || string.IsNullOrEmpty(user.ConfirmationToken.Body) || user.ConfirmationToken is null)
        //    {
        //        user.ConfirmationToken = new ConfirmationToken
        //        {
        //            Body = Guid.NewGuid().ToString(),
        //            LifeUntill = DateTime.Now.AddMinutes(20)
        //        };
        //    }

        //    await _userNotifier.SendConfirmationLink(user, $"token {user.ConfirmationToken}");

        //    return new ServiceResultSuccess<string>(user.ConfirmationToken.Body);
        //}

        //public ServiceResult<string> CheckConfirmation(UserModel? user, string? realConfirmationToken)
        //{
        //    if (user is null)
        //    {
        //        throw new InvalidOperationException("User is not provided");
        //    }

        //    if (user.ConfirmationToken is null)
        //    {
        //        return new ServiceResultFail<string>("Confirmation token is empty");
        //    }

        //    if (string.IsNullOrEmpty(user.ConfirmationToken.Body))
        //    {
        //        return new ServiceResultFail<string>("Confirmation token is empty");
        //    }

        //    if (user.ConfirmationToken.LifeUntill < DateTime.Now)
        //    {
        //        user.ConfirmationToken = null;
        //        return new ServiceResultFail<string>("Confirmation token is deprecated");
        //    }

        //    if (user.ConfirmationToken.Body != realConfirmationToken)
        //    {
        //        return new ServiceResultFail<string>("Confirmation token did'nt match");
        //    }

        //    return new ServiceResultSuccess<string>();
        //}
    }
}