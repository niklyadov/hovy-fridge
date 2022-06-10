using HovyFridge.Web.Services.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HovyFridge.Web.Services.UserNotifier
{
    public class TelegramUserNotifierService : IUserNotifierService
    {
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _telegramBotClient;

        public TelegramUserNotifierService(ILoggerFactory loggerFactory, ITelegramBotClient telegramBotClient)
        {
            _logger = loggerFactory.CreateLogger<MockUserNotifierService>();
            _telegramBotClient = telegramBotClient;
        }

        public async Task<ServiceResult<string>> SendConfirmationLink(HovyFridge.Data.Entity.User user, string confirmationLink)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(confirmationLink))
            {
                throw new ArgumentNullException(nameof(confirmationLink));
            }

            if (string.IsNullOrEmpty(user.TelegramChatId))
            {
                throw new ArgumentNullException(nameof(user.TelegramChatId));
            }

            var keyboard = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithUrl("Click here", confirmationLink));

            await _telegramBotClient.SendTextMessageAsync(new ChatId(user.TelegramChatId), $"Hi, {user.Name}!\nPlease, to give access to the page", replyMarkup: keyboard);

            return new ServiceResultSuccess<string>();
        }
    }
}