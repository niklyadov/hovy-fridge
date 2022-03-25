using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Entity.Common
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; private set; }
        public DbSet<Fridge> Fridges { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<ProductHistory> ProductHistories { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fridge>()
                .HasMany(f => f.Products)
                .WithMany(p => p.LocatedIn);


            //modelBuilder.Entity<Fridge>()
            //    .HasMany(n => n.AllowedUsers);

            //modelBuilder.Entity<Product>()
            //    .HasOne(n => n.Owner);

            //modelBuilder.Entity<User>()
            //    .HasOne(n => n.FavoriteFridge);

            //modelBuilder.Entity<ProductHistory>()
            //    .HasOne(n => n.User);
        }
    }
}
