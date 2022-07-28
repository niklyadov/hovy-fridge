using HovyFridge.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class FridgesQueryBuilder : QueryBuilder<Fridge, ApplicationContext>
{
    public FridgesQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public FridgesQueryBuilder IncludeProducts()
    {
        Query = Context.Fridges.Include(f => f.Products);

        return this;
    }
}