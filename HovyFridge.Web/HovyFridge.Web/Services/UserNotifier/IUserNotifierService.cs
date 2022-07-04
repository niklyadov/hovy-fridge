using HovyFridge.Data.Entity;
using HovyFridge.Web.Models;
using HovyFridge.Web.Services.Common;

namespace HovyFridge.Web.Services.UserNotifier
{
    public interface IUserNotifierService
    {
        public Task<ServiceResult<string>> SendConfirmationLink(User user, string confirmationLink, string confirmationMessage);
    }
}