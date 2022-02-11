using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using HovyFridge.Api.Repositories;

namespace HovyFridge.Api.Controllers
{
    public abstract class BaseController<T> : Controller
        where T : Controller
    {
        protected readonly Configuration Configuration;
        protected readonly ILogger Logger;
        protected readonly ProductRepository ProductRepository;

        public BaseController(IServiceProvider serviceProvider, 
            IOptions<Configuration> configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration.Value;
            Logger = loggerFactory.CreateLogger<T>();
            ProductRepository = serviceProvider.GetRequiredService<ProductRepository>();
        }
    }
}
