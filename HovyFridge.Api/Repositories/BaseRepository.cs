using HovyFridge.Api.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : ApplicationContext
    {
        private readonly TContext _dbContext;
        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAll() =>
            await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetById(int id) => 
            await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<bool> Contains(int id) =>
            await _dbContext.Set<TEntity>().AnyAsync(x => x.Id == id);

        public async Task<TEntity> Add(TEntity entity)
        {
            var entry = _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }
    }
}
