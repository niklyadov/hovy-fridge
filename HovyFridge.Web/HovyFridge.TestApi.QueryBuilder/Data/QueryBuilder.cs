using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public abstract class QueryBuilder<TEntity, TContext> : IDisposable 
    where TEntity : class, new()
    where TContext : DbContext
{
    protected TContext Context { get; set; }
    
    protected static IQueryable<TEntity> Query;
    
    protected QueryBuilder(TContext context)
    {
        Context = context;
        Query = Context.Set<TEntity>();
    }
    
    public static implicit operator List<TEntity>(QueryBuilder<TEntity, TContext> queryBuilder)
    {
        return Query.ToList();
    }
    
    public static implicit operator TEntity?(QueryBuilder<TEntity, TContext> queryBuilder)
    {
        return Query.FirstOrDefault();
    }

    public List<TEntity> ToList()
    {
        return Query.ToList();
    }
    
    public QueryBuilder<TEntity, TContext> Join<TTargetEntity, TKey>(Expression<Func<TEntity, TKey>> tKey, 
        Expression<Func<TTargetEntity, TKey>> uKey) where TTargetEntity : class
    {
        Query = Query.Join(Context.Set<TTargetEntity>(), tKey, uKey, (tblT, tblU) => tblT);
        return this;
    }
    
    public QueryBuilder<TEntity, TContext> JoinWithPredicate<TTargetEntity, TKey>(Expression<Func<TEntity, TKey>> tKey, 
        Expression<Func<TTargetEntity, TKey>> uKey, 
        params Expression<Func<TTargetEntity, bool>>[]? whereExpressions) where TTargetEntity : class
    {
        if (whereExpressions == null) return this;

        var targetSets = Context.Set<TTargetEntity>().AsQueryable();
        targetSets = whereExpressions.Aggregate(targetSets, (current, expression) => current.Where(expression));

        Query = Query.Join(targetSets, tKey, uKey, (tblT, tblU) => tblT);

        return this;
    }

    public QueryBuilder<TEntity, TContext> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
    {
        foreach (var includeExpression in includeExpressions)
        {
            Query = Query.Include(includeExpression);
        }

        return this;
    }

    public TEntity? FirstOrDefault()
    {
        return Query.FirstOrDefault();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}