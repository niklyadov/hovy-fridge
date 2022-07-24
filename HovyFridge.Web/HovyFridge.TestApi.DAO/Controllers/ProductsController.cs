using HovyFridge.Data.Entity;
using HovyFridge.TestApi.DAO.Data;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.TestApi.DAO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDao _productDao;

        public ProductsController(IServiceProvider serviceProvider)
        {
            _productDao = serviceProvider.GetRequiredService<ProductDao>();
        }

        public IEnumerable<Product> GetProductsWithBarcode(string barcode)
        {
            return _productDao.GetProductsListByBarcode(barcode);
        }
    }
}
