using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders
{
    public class ShoppingListsQueryBuilder : QueryBuilder<ShoppingList, ApplicationContext>
    {
        public ShoppingListsQueryBuilder(ApplicationContext context) : base(context)
        {
        }
    }
}
