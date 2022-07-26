using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]\
    [Route("[controller]")]
    public class ShoppingListController : BaseController
    {
        private readonly IShoppingListsService _recipesService;
        public ShoppingListController(IShoppingListsService usersService)
        {
            _recipesService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingLists()
        {
            var result = await _recipesService.GetAllAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingListById(long id)
        {
            var result = await _recipesService.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingList(ShoppingList user)
        {
            var result = await _recipesService.AddAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShoppingList(ShoppingList user)
        {
            var result = await _recipesService.UpdateAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingListById(long id)
        {
            var result = await _recipesService.DeleteByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreShoppingListById(long id)
        {
            var result = await _recipesService.RestoreByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}
