using FluentResults;
using HovyFridge.Data.Entity;

namespace HovyFridge.Web.Services.UserNotifier
{
    public class MockUserNotifierService : IUserNotifierService
    {
        private readonly ILogger _logger;
        public MockUserNotifierService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MockUserNotifierService>();
        }
        public async Task<Result<string>> SendConfirmationLink(User user, string confirmationLink, string confirmationMessage)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (user is null)
                        throw new ArgumentNullException(nameof(user));

                    if (confirmationLink is null)
                        throw new ArgumentNullException(nameof(confirmationLink));

                    if (confirmationMessage is null)
                        throw new ArgumentNullException(nameof(confirmationMessage));

                    _logger.LogInformation($"Message for user {user}: {confirmationLink} with message {confirmationMessage}");

                    return Result.Ok();
                } catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            });
        }
    }
}