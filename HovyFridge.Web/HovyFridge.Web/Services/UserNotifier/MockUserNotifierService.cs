using HovyFridge.Data.Entity;
using HovyFridge.Web.Models;
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
        public async Task<ServiceResult<string>> SendConfirmationLink(User user, string message)
        {
            return await Task.Run(() =>
            {
                if (user is null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (message is null)
                {
                    throw new ArgumentNullException(nameof(message));
                }

                _logger.LogInformation($"Message for user {user}: {message}");

                return new ServiceResultSuccess<string>();
            });
        }
    }
}
