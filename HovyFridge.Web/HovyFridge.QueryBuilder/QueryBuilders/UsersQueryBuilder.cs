using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders
{
    public class UsersQueryBuilder : QueryBuilder<User, ApplicationContext>
    {
        public UsersQueryBuilder(ApplicationContext context) : base(context)
        {
        }
    }
}
