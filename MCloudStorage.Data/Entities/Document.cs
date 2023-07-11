using MCloudStorage.API.Entities.Enums;
using MCloudStorage.Data.Entities.Enums;
using MCloudStorage.Data.Models;

namespace MCloudStorage.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string? FileReference { get; set; }
        public string? FileLink { get; set; }
        public string? ParentLocation { get; set; }
        public string UserId { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string Avatar { get; set; } = "";
        public FileType FileType { get; set; }
        public FileStatus FileStatus { get; set; }
    }
}
