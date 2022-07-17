using HovyFridge.Data;
using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public class FridgesQueryBuilder : QueryBuilder<Fridge, ApplicationContext>
{
    public FridgesQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}