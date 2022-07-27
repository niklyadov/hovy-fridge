using HovyFridge.DAO.Services;
using HovyFridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HovyFridge.DAO
{
    public static class IServiceCollectionUseDAOExtension
    {
        public static void UseDataAccessObjectPattern(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddScoped<IAuthService, AuthServiceRepository>();
            services.AddScoped<IProductsService, ProductsServiceRepository>();
            services.AddScoped<IFridgesService, FridgesServiceRepository>();
            services.AddScoped<IVersionsService, VersionsService>();
            services.AddScoped<IUsersService, UsersServiceRepository>();
            services.AddScoped<IFridgeAccessLevelsService, FridgeAccessLevelsServiceRepository>();
            services.AddScoped<IProductSuggestionsService, ProductSuggestionsServiceRepository>();
        }
    }
}
