using HovyFridge.Data.Entity;
using HovyFridge.Web.Models;
using HovyFridge.Web.Repos;
using HovyFridge.Web.Services.Common;

namespace HovyFridge.Web.Services
{
    public class UsersService
    {
        public UsersService(UsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
        }


        private readonly UsersRepo _usersRepo;
        public User? CurrentUser { get; set; }

        public ServiceResult<User> GetUserByUsername(string username)
        {
            var userByUsername = _usersRepo.GetUserByUsername(username);

            if (userByUsername == null)
            {
                return new ServiceResultFail<User>();
            }

            return new ServiceResultSuccess<User>(userByUsername);
        }
    }
}