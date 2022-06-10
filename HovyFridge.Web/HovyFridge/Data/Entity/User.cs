namespace HovyFridge.Data.Entity
{
    public class User : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string? TelegramChatId { get; set; }

        public string? Email { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public long? RegistrationTimestamp { get; set; }

        public long? LastLoginTimestamp { get; set; }

        public UserRole UserRole { get; set; }
    }


    public enum UserRole
    {
        Manager,
        Administrator,
        Root
    }
}