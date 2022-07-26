using HovyFridge.Entity;
using HovyFridge.Web.Auth;
using HovyFridge.Web.Models;
using HovyFridge.Web.Services;
using HovyFridge.Web.Services.CodeConfirmation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace HovyFridge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CodeConfirmationService _confirmationService;
        private readonly UsersService _usersService;
        private readonly AppConfiguration _appConfiguration;

        public HomeController(ILogger<HomeController> logger, CodeConfirmationService confirmationService, UsersService usersService, IOptions<AppConfiguration> options)
        {
            _logger = logger;
            _confirmationService = confirmationService;
            _usersService = usersService;
            _appConfiguration = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [BasicAuth(UserRole.Manager)]
        public async Task<IActionResult> Privacy([FromQuery] string? confirmationToken)
        {
            var currentUser = _usersService.CurrentUser;

            if (currentUser is null) return Unauthorized();

            var baseUrl = _appConfiguration.BaseUrl;

            var pageUrl = Request.Path.ToString().Trim('/');
                pageUrl = $"{baseUrl}{pageUrl}";
            var pageUrlWithToken = pageUrl + "?confirmationToken={0}";

            if (string.IsNullOrEmpty(confirmationToken))
            {
                _confirmationService.SendConfirmationAsync(currentUser, pageUrlWithToken, "privacy page");
                return View("NoToken");
            }

            var confirmationResult = await _confirmationService.CheckConfirmation(currentUser, confirmationToken);

            if (!confirmationResult)
            {
                ViewBag.PageUrl = pageUrl;
                return View("InvalidToken");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}