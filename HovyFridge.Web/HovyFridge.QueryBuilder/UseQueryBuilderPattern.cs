using HovyFridge.QueryBuilder.Services;
using HovyFridge.QueryBuilder.Repository;
using HovyFridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HovyFridge.QueryBuilder
{
    public static class IServiceCollectionUseQueryBuilderExtension
    {
        public static void QueryBuilderPattern(this IServiceCollection services)
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
