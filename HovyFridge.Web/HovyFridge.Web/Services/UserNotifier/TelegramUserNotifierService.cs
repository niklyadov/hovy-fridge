using FluentResults;
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

        public async Task<Result<string>> SendConfirmationLink(Data.Entity.User user, string confirmationLink, string confirmationMessage = "Click on the button")
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                if (string.IsNullOrEmpty(confirmationLink))
                    throw new ArgumentNullException(nameof(confirmationLink));

                if (string.IsNullOrEmpty(user.TelegramChatId))
                    throw new ArgumentNullException(nameof(user.TelegramChatId));

                var keyboard = new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithUrl("Click here", confirmationLink));

                await _telegramBotClient.SendTextMessageAsync(new ChatId(user.TelegramChatId), $"Hi, {user.Name}!\n{confirmationMessage}", replyMarkup: keyboard);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.ToString());
            }
        }
    }
}