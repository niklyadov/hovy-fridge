using HovyFridge.Web.Services;
using HovyFridge.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsersService _usersService;

        public AuthController(IServiceProvider serviceProvider)
        {
            _usersService = serviceProvider.GetRequiredService<UsersService>();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.UserName))
            {
                ModelState.AddModelError("UserName", "Username must be set");
            }
            else if (string.IsNullOrEmpty(viewModel.Password))
            {
                ModelState.AddModelError("Password", "Password must be set");
            }
            else
            {
                var registerResult = await _usersService.RegisterAsync(viewModel.UserName, viewModel.Password);

                if (registerResult.IsFailed)
                {
                    foreach (var error in registerResult.Errors)
                        ModelState.AddModelError("", error.Message);
                }
            }

            return View();
        }
    }
}
