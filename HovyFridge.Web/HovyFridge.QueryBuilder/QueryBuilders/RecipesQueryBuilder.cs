using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders
{
    public class RecipesQueryBuilder : QueryBuilder<Recipe, ApplicationContext>
    {
        public RecipesQueryBuilder(ApplicationContext context) : base(context)
        {
        }
    }
}
