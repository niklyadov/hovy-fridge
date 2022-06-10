namespace HovyFridge.Api.Data.Entity
{
    public class VersionInfo
    {
        public Guid Id { get; set; }
        public int MajorNumber { get; set; }
        public int MinorNumber { get; set; }
        public string Name { get; set; } = "Undefined";
        public string DownloadUrl { get; set; }
    }
}