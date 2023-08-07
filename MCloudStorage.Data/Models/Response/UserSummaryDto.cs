namespace MCloudStorage.Data.Models.Response
{
    public class UserSummaryDto
    {
        /// <summary>
        /// Get or set the total uploads of file by the user
        /// </summary>
        public int TotalUploads { get; set; }

        /// <summary>
        /// Get or set the the total file size in Bytes 
        /// </summary>
        public long TotalFileSize { get; set; }

        /// <summary>
        /// Get or set the the date that file was last uploaded
        /// </summary>
        public DateTime LastUploadDate { get; set; }
        // Add other properties as needed
    }
}
