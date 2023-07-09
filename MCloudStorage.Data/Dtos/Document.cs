using MCloudStorage.API.Entities.enums;

namespace MCloudStorage.Data.Dtos

{
    public class Document
    {
        public int Id { get; set; }
        public string? Filename { get; set; }
        public string? FileReference { get; set; }
        public string? Filelink { get; set; }
        public string? FileUploadType { get; set; }
        public string? ParentLocation { get; set; }
        public string UserId { get; set; }
        public string? FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string Avatar { get; set; } = " ";
        public FileStatus FileStatus { get; set; }
    }
}
