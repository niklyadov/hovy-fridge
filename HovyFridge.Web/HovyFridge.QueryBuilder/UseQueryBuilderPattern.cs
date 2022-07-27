﻿using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.QueryBuilder.Repository;
using HovyFridge.QueryBuilder.Services;
using HovyFridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HovyFridge.QueryBuilder
{
    public static class IServiceCollectionUseQueryBuilderExtension
    {
        public static void UseQueryBuilderPattern(this IServiceCollection services)
        {

            // Generic Repositories.
            services.AddScoped<FridgesRepository>();
            services.AddScoped<FridgeAccessLevelsRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<ProductSuggestionsRepository>();
            services.AddScoped<RecipesRepository>();
            services.AddScoped<ShoppingListsRepository>();
            services.AddScoped<UsersRepository>();

            // Query Builders.
            services.AddScoped<FridgesQueryBuilder>();
            services.AddScoped<FridgeAccessLevelsQueryBuilder>();
            services.AddScoped<ProductsQueryBuilder>();
            services.AddScoped<ProductSuggestionsQueryBuilder>();
            services.AddScoped<RecipesQueryBuilder>();
            services.AddScoped<ShoppingListsQueryBuilder>();
            services.AddScoped<UsersQueryBuilder>();

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
