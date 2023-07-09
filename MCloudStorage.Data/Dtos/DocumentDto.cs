namespace MCloudStorage.Data.Dtos
{

    public class DocumentDto
    {
        
        public string? Filename { get; set; }
        
        public string? FileUploadType { get; set; }
        
        public string? ParentLocation { get; set; }
        
        public string UserId { get; set; }
        
        public string? FileType { get; set; }
    }
}
