namespace HovyFridge.Web.Models
{
    public class AppConfiguration
    {
        public string Url { get; set; } = "http://localhost:5187/";

        public TelegramBotConfiguration TelegramBot { get; set; }
            = new TelegramBotConfiguration();
    }

    public class TelegramBotConfiguration
    {
        public string Token { get; set; } = "";
    }
}