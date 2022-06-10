using HovyFridge.Web.Services.Common;
using HovyFridge.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Text;

namespace HovyFridge.Web.Services.CodeConfirmation
{
    public class CodeConfirmationAttribute : TypeFilterAttribute
    {
        public CodeConfirmationAttribute() : base(typeof(CodeConfirmationFilter))
        {
        }
    }

    public class CodeConfirmationFilter : IResultFilter
    {
        private readonly CodeConfirmationService _confirmationService;
        private readonly UsersService _usersService;
        private readonly AppConfiguration _configuration;

        public CodeConfirmationFilter(CodeConfirmationService confirmationService,
           UsersService usersService, IOptions<AppConfiguration> options)
        {
            _confirmationService = confirmationService;
            _usersService = usersService;
            _configuration = options.Value;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public async void OnResultExecuting(ResultExecutingContext context)
        {
            string? confirmationToken = null;
            if (context.HttpContext.Request.Query.TryGetValue("confirmationToken", out var confirmationTokenValue))
            {
                confirmationToken = confirmationTokenValue.ToString();
            }

            string url = $"{_configuration.Url}{context.HttpContext.Request.Path}?confirmationToken=" + "{0}";

            var currentUser = _usersService.CurrentUser;

            if (currentUser == null)
            {
                throw new Exception("Current user is not provided");
            }

            var confirmResult = await _confirmationService.CheckConfirmation(currentUser, confirmationToken);

            if (!confirmResult)
            {
                //context.HttpContext.Response.StatusCode = 403;
                var bytes = Encoding.UTF8.GetBytes("Please, check your Telegram account");

                await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);

                _confirmationService.SendConfirmationAsync(currentUser, url);

                context.Cancel = true;
            }
        }
    }
}