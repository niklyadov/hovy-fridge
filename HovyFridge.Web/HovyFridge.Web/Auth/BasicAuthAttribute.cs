using HovyFridge.Data.Entity;
using HovyFridge.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace HovyFridge.Web.Auth
{
    public class BasicAuthAttribute : AuthorizeAttribute
    {
        public BasicAuthAttribute(params UserRole[] allowedRoles)
        {
            Policy = "BasicAuthentication";
            Roles = string.Join(',', allowedRoles.Select(role => role.GetName<UserRole>()));
        }
    }
}