using HovyFridge.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSuggestion> ProductSuggestion { get; set; }
        public DbSet<FridgeAccessLevel> FridgeAccessLevels { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618
        {
            Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// configures one-to-many relationship
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Fridge)
            //    .WithMany(f => f.Products);

            //modelBuilder.Entity<Product>().HasAlternateKey(p => p.FridgeId);
        }
    }
}