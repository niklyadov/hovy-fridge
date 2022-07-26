using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IVersionsService
    {
        VersionInfo? GetLastVersionInfo();
        FileStream GetVersionFileStream(Guid versionId);
        VersionInfo? GetVersionInfo(Guid versionId);
    }
}