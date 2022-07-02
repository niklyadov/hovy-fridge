using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    public class VersionsController : BaseController
    {
        private readonly VersionsService _versionsService;

        public VersionsController(VersionsService versionsService)
        {
            _versionsService = versionsService;
        }

        [HttpGet]
        [Route("versions/last")]
        public VersionInfo GetLastVersionInfo()
        {
            return _versionsService.GetLastVersionInfo();
        }

        [HttpGet]
        [Route("versions/{versionId}")]
        public VersionInfo GetLastVersionInfo([FromRoute] Guid versionId)
        {
            return _versionsService.GetVersionInfo(versionId);
        }
    }
}