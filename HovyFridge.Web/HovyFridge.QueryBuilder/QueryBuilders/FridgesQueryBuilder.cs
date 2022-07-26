using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class FridgesQueryBuilder : QueryBuilder<Fridge, ApplicationContext>
{
    public FridgesQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}