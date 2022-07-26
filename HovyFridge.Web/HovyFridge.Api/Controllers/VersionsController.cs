using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    public class VersionsController : BaseController
    {
        private readonly IVersionsService _versionsService;

        public VersionsController(IVersionsService versionsService)
        {
            _versionsService = versionsService;
        }

        [HttpGet]
        [Route("last")]
        public VersionInfo? GetLastVersionInfo()
        {
            return _versionsService.GetLastVersionInfo();
        }

        [HttpGet]
        [Route("{versionId}")]
        public VersionInfo? GetLastVersionInfo([FromRoute] Guid versionId)
        {
            return _versionsService.GetVersionInfo(versionId);
        }
    }
}