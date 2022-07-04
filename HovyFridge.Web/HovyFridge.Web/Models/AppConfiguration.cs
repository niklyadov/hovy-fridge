namespace HovyFridge.Web.Models
{
    public class AppConfiguration
    {
        public string BaseUrl { get; set; } = "";

        public TelegramBotConfiguration TelegramBot { get; set; }
            = new TelegramBotConfiguration();
    }

    public class TelegramBotConfiguration
    {
        public string Token { get; set; } = "";
    }
}