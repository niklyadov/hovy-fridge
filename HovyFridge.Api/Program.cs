using HovyFridge.Api;
using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;
using HovyFridge.Api.Repositories;
using HovyFridge.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HovyFridge
{
    public class Program
    {
        public static void Main(string[] args)
                => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }

    public static class VersionInfo
    {
        public static string Version { get; set; } = "v0.1.0";
    }

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.Configure<Configuration>(Configuration.GetSection("AppConfiguration"));

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddFile(Configuration.GetSection("Logging")));

            services.Configure<HostOptions>(hostOptions =>
                hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore);

            #region Swagger

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "HovyFridge",
                    Version = VersionInfo.Version
                });
            });

            #endregion

            #region DbConnection

            var dbConnection = Configuration.GetConnectionString("DefaultConnection");
            var dbVersion = Configuration.GetValue<string>("ConnectionMysqlMariaDbVersion");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(dbConnection,
                    new MariaDbServerVersion(dbVersion)));

            #endregion

            services.AddScoped<FridgesRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<ProductsHistoryRepository>();

            services.AddScoped<FridgesService>();
            services.AddScoped<ProductsService>();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment, ILoggerFactory loggerFactory)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI();
            }

            applicationBuilder.UseRouting();

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseAuthorization();


            applicationBuilder.UseEndpoints(endpoints =>
                        endpoints.MapControllerRoute(
                                    name: "default",
                                    pattern: "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}