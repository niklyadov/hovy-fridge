using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public class FridgesQueryBuilder : QueryBuilder<Fridge>
{
    public FridgesQueryBuilder(DbContext context) : base(context)
    {
    }
}