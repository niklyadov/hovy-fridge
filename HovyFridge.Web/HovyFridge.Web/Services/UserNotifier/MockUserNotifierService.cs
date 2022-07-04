using HovyFridge.Data.Entity;
using HovyFridge.Web.Services.Common;

namespace HovyFridge.Web.Services.UserNotifier
{
    public class MockUserNotifierService : IUserNotifierService
    {
        private readonly ILogger _logger;
        public MockUserNotifierService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MockUserNotifierService>();
        }
        public async Task<ServiceResult<string>> SendConfirmationLink(User user, string confirmationLink, string confirmationMessage)
        {
            return await Task.Run(() =>
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                if (confirmationLink is null)
                    throw new ArgumentNullException(nameof(confirmationLink));

                if (confirmationMessage is null)
                    throw new ArgumentNullException(nameof(confirmationMessage));

                _logger.LogInformation($"Message for user {user}: {confirmationLink} with message {confirmationMessage}");

                return new ServiceResultSuccess<string>();
            });
        }
    }
}