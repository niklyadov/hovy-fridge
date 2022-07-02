using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class UsersRepository : BaseRepository<User, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public UsersRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}