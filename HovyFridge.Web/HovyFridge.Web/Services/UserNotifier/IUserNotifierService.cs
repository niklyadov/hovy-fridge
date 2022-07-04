using FluentResults;
using HovyFridge.Data.Entity;

namespace HovyFridge.Web.Services.UserNotifier
{
    public interface IUserNotifierService
    {
        public Task<Result<string>> SendConfirmationLink(User user, string confirmationLink, string confirmationMessage);
    }
}