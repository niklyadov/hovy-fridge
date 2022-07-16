using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public abstract class QueryBuilder<TEntity> : IDisposable where TEntity : class, new()
{
    protected DbContext Context { get; set; }

    /// <summary>
    /// The query object
    /// </summary>
    protected static IQueryable<TEntity> Query;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryBuilder{TEntity}"/> class.
    /// </summary>
    protected QueryBuilder(DbContext context)
    {
        Context = context;
        Query = Context.Set<TEntity>();
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="QueryBuilder{TEntity}"/> to <see cref="List{TEntity}"/>.
    /// </summary>
    /// <param name="queryBuilder">The query builder.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator List<TEntity>(QueryBuilder<TEntity> queryBuilder)
    {
        return Query.ToList();
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="QueryBuilder{TEntity}"/> to <see cref="TEntity"/>.
    /// </summary>
    /// <param name="queryBuilder">The query builder.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator TEntity?(QueryBuilder<TEntity> queryBuilder)
    {
        return Query.FirstOrDefault();
    }

    /// <summary>
    /// Execute query and return the result as list 
    /// </summary>
    /// <returns></returns>
    public List<TEntity> ToList()
    {
        return Query.ToList();
    }

    /// <summary>
    /// Joins with the specified t key.
    /// </summary>
    /// <typeparam name="TTargetEntity">The type of the target entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="tKey">The t key.</param>
    /// <param name="uKey">The u key.</param>
    /// <returns></returns>
    public QueryBuilder<TEntity> Join<TTargetEntity, TKey>(Expression<Func<TEntity, TKey>> tKey, Expression<Func<TTargetEntity, TKey>> uKey) where TTargetEntity : class
    {
        Query = Query.Join(Context.Set<TTargetEntity>(), tKey, uKey, (tblT, tblU) => tblT);
        return this;
    }

    /// <summary>
    /// Joins with predicate.
    /// </summary>
    /// <typeparam name="TTargetEntity">The type of the target entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="tKey">The t key.</param>
    /// <param name="uKey">The u key.</param>
    /// <param name="whereExpressions">The where expressions.</param>
    /// <returns></returns>
    public QueryBuilder<TEntity> JoinWithPredicate<TTargetEntity, TKey>(Expression<Func<TEntity, TKey>> tKey, Expression<Func<TTargetEntity, TKey>> uKey, params Expression<Func<TTargetEntity, bool>>[] whereExpressions) where TTargetEntity : class
    {
        if (whereExpressions == null) return this;

        var targetSets = Context.Set<TTargetEntity>().AsQueryable();
        targetSets = whereExpressions.Aggregate(targetSets, (current, expression) => current.Where(expression));

        Query = Query.Join(targetSets, tKey, uKey, (tblT, tblU) => tblT);

        return this;
    }

    /// <summary>
    /// Loop through expressions and include it into the query
    /// </summary>
    /// <param name="includeExpressions">The include expressions.</param>
    /// <returns></returns>
    public QueryBuilder<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
    {
        foreach (var includeExpression in includeExpressions)
        {
            Query = Query.Include(includeExpression);
        }

        return this;
    }

    /// <summary>
    /// Execute the query and get the first item.
    /// </summary>
    /// <returns></returns>
    public TEntity? FirstOrDefault()
    {
        return Query.FirstOrDefault();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Context.Dispose();
        Query = null;
    }
}