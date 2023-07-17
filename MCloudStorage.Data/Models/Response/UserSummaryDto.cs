namespace MCloudStorage.Data.Models.Response
{
    public class UserSummaryDto
    {
        public int TotalUploads { get; set; }
        public long TotalFileSize { get; set; }
        public DateTime LastUploadDate { get; set; }
        // Add other properties as needed
    }
}
