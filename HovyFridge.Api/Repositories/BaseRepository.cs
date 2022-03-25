using HovyFridge.Api.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : ApplicationContext
    {
        protected readonly TContext Context;
        protected BaseRepository(TContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<List<TEntity>> GetAll() =>
            await Context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetById(int id) => 
            await Context.Set<TEntity>().FindAsync(id);

        public async Task<bool> Contains(int id) =>
            await Context.Set<TEntity>().AnyAsync(x => x.Id == id);

        public async Task<TEntity> Add(TEntity entity)
        {
            var entry = Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();
            }

            return entity;
        }
    }
}
