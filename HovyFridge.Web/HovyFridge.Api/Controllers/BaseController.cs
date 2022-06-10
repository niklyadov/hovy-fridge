using HovyFridge.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [BearerTokenAuth("hello", "madonna")]
    public class BaseController : ControllerBase
    {
    }
}