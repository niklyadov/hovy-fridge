﻿using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class FridgesRepository : BaseRepository<Fridge, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public FridgesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override Task<List<Fridge>> GetAll(bool ignoreDeleted = true)
        {
            return _dbContext.Fridges.Include(f => f.Products)
                .Where(f => !ignoreDeleted || !f.IsDeleted).Select(o => new Fridge()
                {
                    Id = o.Id,
                    IsDeleted = o.IsDeleted,
                    Name = o.Name,
                    Products = new List<Product>(),
                    ProductsAmount = o.Products.Where(p => !p.IsDeleted).ToList().Count
                }).ToListAsync();
        }

        public override async Task<Fridge?> GetById(long id)
        {
            return await _dbContext.Fridges
                                   .Include(f => f.Products)
                                   .Where(f => f.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public override async Task<Fridge> Delete(Fridge fridge)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var product in fridge.Products)
                {
                    product.IsDeleted = true;
                    _dbContext.Entry(product).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }

                var deletedEntityResult = await base.Delete(fridge);

                transaction.Commit();

                return deletedEntityResult;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}