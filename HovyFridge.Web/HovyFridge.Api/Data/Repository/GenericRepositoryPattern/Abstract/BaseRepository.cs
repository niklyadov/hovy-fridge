using HovyFridge.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract
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

        public virtual async Task<List<TEntity>> GetAll(bool ignoreDeleted = true)
        {
            var set = _dbContext.Set<TEntity>().Where(p => !ignoreDeleted || !p.IsDeleted);

            return await set.ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetByIds(List<long> ids)
        {
            return await _dbContext.Set<TEntity>()
                .Where(l => ids.Contains(l.Id)).ToListAsync();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var entry = _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> Save(TEntity entity)
        {
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> DeleteById(long id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new Exception($"Entity with id {id} is not found!");

            entity.IsDeleted = true;

            return await Update(entity);
        }

        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"Entity with was null");

            entity.IsDeleted = true;

            return await Update(entity);
        }

        public virtual async Task<TEntity> RestoreById(long id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
                throw new Exception($"Entity with id {id} is not found!");

            entity.IsDeleted = false;

            return await Update(entity);
        }

        public virtual async Task<TEntity> Restore(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"Entity with was null");

            entity.IsDeleted = false;

            return await Update(entity);
        }
    }
}