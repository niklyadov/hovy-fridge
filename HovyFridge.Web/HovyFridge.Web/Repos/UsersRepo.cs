using HovyFridge.Data.Entity;
using HovyFridge.Web.Auth;
using HovyFridge.Web.Models;

namespace HovyFridge.Web.Repos
{
    public class UsersRepo
    {
        private readonly List<User> _users = new List<User>
        {
            new User()
            {
                Id = 1,
                Name = "nikita",
                PasswordHash = "nikita",
                UserRole = UserRole.Manager,
                TelegramChatId = "427384175"
            }
        };

        public User? GetUserByUsername(string username)
            => _users.Where(user => user.Name.Equals(username)).FirstOrDefault();
    }
}