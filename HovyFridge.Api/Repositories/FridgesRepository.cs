using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Repositories
{
    public class FridgesRepository : BaseRepository<Fridge, ApplicationContext>
    {
        public FridgesRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        private async Task<IQueryable<Fridge>> GetAllAsync()
            => Context.Set<Fridge>();

        /// <summary>
        /// Простой список холодильников без подробностей
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fridge>> GetFridgesListAsync()
        {
            return await Context.Set<Fridge>()
                //.Where(x => x.AllowedUsers != null && 
                //            x.AllowedUsers.Contains(user))
                .Include(x => x.Products)
                .Select(x => new Fridge()
                {
                    Id = x.Id,
                    Name = x.Name,
                    //IsFavorite = x.FavoriteInUsers != null &&
                    //    x.AllowedUsers.Contains(user) != null,
                    ProductsCount = x.Products != null ? x.Products.Count() : 0
                }).ToListAsync();
        }

        public async Task<Fridge?> GetDetailedFridgeById(int id)
        {
            return await Context.Set<Fridge>()
                .Where(x => x.Id == id)
                .Include(x => x.Products)
                .Select(x => new Fridge()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = x.Products,
                    ProductsCount = x.Products != null ? x.Products.Count() : 0
                }).FirstOrDefaultAsync();
        }

        public async Task<Fridge?> PushProduct(int fridgeId, Product product)
        {
            var fridge = await Context.Set<Fridge>()
                .Where(x => x.Id == fridgeId)
                .Include(x => x.Products)
                .FirstOrDefaultAsync();

            if (fridge == null)
                return null;

            if (fridge.Products == null)
                fridge.Products = new List<Product>();

            fridge.Products.Add(product);

            return await Update(fridge);
        }

        public async Task<Fridge?> PopProduct(int fridgeId, Product product)
        {
            var fridge = await Context.Set<Fridge>()
                .Where(x => x.Id == fridgeId)
                .Include(x => x.Products)
                .FirstOrDefaultAsync();

            if (fridge == null)
                return null;

            if (fridge.Products != null && 
                fridge.Products.Contains(product))
            {
                fridge.Products.Remove(product);
                return await Update(fridge);
            }

            return fridge;
        }
    }
}
