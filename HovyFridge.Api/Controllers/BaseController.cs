using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using HovyFridge.Api.Services;

namespace HovyFridge.Api.Controllers
{
    public abstract class BaseController<T> : ControllerBase
        where T : ControllerBase
    {
        protected readonly Configuration Configuration;
        protected readonly ILogger Logger;
        protected readonly FridgesService FridgesService;
        protected readonly ProductsService ProductsService;

        public BaseController(IServiceProvider serviceProvider, 
            IOptions<Configuration> configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration.Value;
            Logger = loggerFactory.CreateLogger<T>();
            FridgesService = serviceProvider.GetRequiredService<FridgesService>();
            ProductsService = serviceProvider.GetRequiredService<ProductsService>();
        }
    }
}
