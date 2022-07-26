using HovyFridge.GenericRepository.Repository;
using HovyFridge.GenericRepository.Services;
using HovyFridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HovyFridge.GenericRepository
{
    public static class IServiceCollectionUseGenericRepoExtension
    {
        public static void UseGenericRepositoryPattern(this IServiceCollection services)
        {
            services.AddScoped<FridgesRepository>();
            services.AddScoped<FridgeAccessLevelsRepository>();

            services.AddScoped<ProductsRepository>();
            services.AddScoped<ProductSuggestionsRepository>();

            services.AddScoped<RecipesRepository>();
            services.AddScoped<ShoppingListsRepository>();

            services.AddScoped<UsersRepository>();

            // Add services to the container.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IFridgesService, FridgesService>();
            services.AddScoped<IVersionsService, VersionsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IFridgeAccessLevelsService, FridgeAccessLevelsService>();
            services.AddScoped<IProductSuggestionsService, ProductSuggestionsService>();
        }
    }
}
