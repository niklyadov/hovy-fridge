using HovyFridge.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public abstract class QueryBuilder<TEntity, TContext>
    : IDisposable where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    protected TContext Context { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected static IQueryable<TEntity> Query;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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

    public async Task<List<TEntity>> ToListAsync()
    {
        return await Query.ToListAsync();
    }

    public QueryBuilder<TEntity, TContext> Join<TTargeIEntity, TKey>(Expression<Func<TEntity, TKey>> tKey,
        Expression<Func<TTargeIEntity, TKey>> uKey) where TTargeIEntity : class
    {
        Query = Query.Join(Context.Set<TTargeIEntity>(), tKey, uKey, (tblT, tblU) => tblT);
        return this;
    }

    public QueryBuilder<TEntity, TContext> JoinWithPredicate<TTargeIEntity, TKey>(Expression<Func<TEntity, TKey>> tKey,
        Expression<Func<TTargeIEntity, TKey>> uKey,
        params Expression<Func<TTargeIEntity, bool>>[]? whereExpressions) where TTargeIEntity : class
    {
        if (whereExpressions == null) return this;

        var targetSets = Context.Set<TTargeIEntity>().AsQueryable();
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

    public QueryBuilder<TEntity, TContext> Skip(int count)
    {
        Query = Query.Skip(count);

        return this;
    }

    public QueryBuilder<TEntity, TContext> Limit(int count)
    {
        Query = Query.Take(count);

        return this;
    }
    public QueryBuilder<TEntity, TContext> WithId(long entityId)
    {
        Query = Query.Where(e => e.Id == entityId);

        return this;
    }

    public QueryBuilder<TEntity, TContext> WhereDeleted()
    {
        Query = Query.Where(e => e.IsDeleted);

        return this;
    }

    public QueryBuilder<TEntity, TContext> WhereNotDeleted()
    {
        Query = Query.Where(e => !e.IsDeleted);

        return this;
    }


    public QueryBuilder<TEntity, TContext> Paginate(Pagination pagination)
    {
        var totalCount = Query.Count();

        return Skip(pagination.GetOffset(totalCount))
            .Limit(pagination.ItemsPerPage);
    }

    public TEntity? FirstOrDefault()
    {
        return Query.FirstOrDefault();
    }

    public async Task<TEntity?> FirstOrDefaultAsync()
    {
        return await Query.FirstOrDefaultAsync();
    }

    public TEntity Single()
    {
        return Query.Single();
    }

    public async Task<TEntity> SingleAsync()
    {
        return await Query.SingleAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateAsync(ICollection<TEntity> entities)
    {
        Context.UpdateRange(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        Context.Add(entity);
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task AddAsync(ICollection<TEntity> entity)
    {
        await Context.AddRangeAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedDateTime = DateTime.UtcNow;

        return await UpdateAsync(entity);
    }

    public async Task<ICollection<TEntity>> DeleteAsync(ICollection<TEntity> entities)
    {
        entities.Select(entity =>
        {
            entity.IsDeleted = true;
            entity.DeletedDateTime = DateTime.UtcNow;
            return entity;
        });

        return await UpdateAsync(entities);
    }

    public async Task DeleteAsync()
    {
        await DeleteAsync(await ToListAsync());
    }

    public async Task<TEntity> UndoDeleteAsync(TEntity entity)
    {
        entity.IsDeleted = false;
        entity.DeletedDateTime = null;

        return await UpdateAsync(entity);
    }

    public async Task UndoDeleteAsync(ICollection<TEntity> entities)
    {
        entities.Select(entity =>
        {
            entity.IsDeleted = false;
            entity.DeletedDateTime = null;
            return entity;
        });

        await UpdateAsync(entities);
    }

    public async Task UndoDeleteAsync()
    {
        await UndoDeleteAsync(await ToListAsync());
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}