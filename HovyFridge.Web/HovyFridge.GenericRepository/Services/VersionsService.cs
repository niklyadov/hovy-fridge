using HovyFridge.Entity;
using HovyFridge.Services;

namespace HovyFridge.GenericRepository.Services;

public class VersionsService : IVersionsService
{
    private List<VersionInfo> _versions = new List<VersionInfo>()
        {
            new VersionInfo()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                MajorNumber = 1,
                MinorNumber = 2,
                Name = "Fresh fish",
                DownloadUrl = "https://github.com/niklyadov/hovy-fridge/releases/download/1.2/HovyFridge_v.1.2.apk"
            }
        };


    public VersionInfo? GetVersionInfo(Guid versionId)
    {
        return _versions.FirstOrDefault(v => v.Id == versionId);
    }

    public VersionInfo? GetLastVersionInfo()
    {
        return _versions.LastOrDefault();
    }

    public FileStream GetVersionFileStream(Guid versionId)
    {
        return File.OpenRead(Path.Combine(Environment.CurrentDirectory, "version_files", $"{versionId}.apk"));
    }
}
