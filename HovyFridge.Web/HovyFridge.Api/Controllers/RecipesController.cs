using HovyFridge.Api.Services;
using HovyFridge.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]\
    [Route("[controller]")]
    public class RecipesController : BaseController
    {
        private readonly RecipesService _recipesService;
        public RecipesController(RecipesService usersService)
        {
            _recipesService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var result = await _recipesService.GetAllAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(long id)
        {
            var result = await _recipesService.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe user)
        {
            var result = await _recipesService.AddAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(Recipe user)
        {
            var result = await _recipesService.UpdateAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeById(long id)
        {
            var result = await _recipesService.DeleteByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreRecipeById(long id)
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
