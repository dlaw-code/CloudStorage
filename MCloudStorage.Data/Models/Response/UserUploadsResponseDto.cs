namespace MCloudStorage.Data.Models.Response
{
    public class UserUploadsResponseDto
    {
        /// <summary>
        /// Allows to  retrieve the list of documents uploaded by a user and also set a new list to the user
        /// </summary>
        public List<DocumentDto> UserUploads { get; set; }

        /// <summary>
        /// Get or set the Total count of the File
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Get or set the Page of the file uploaded
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Get or set the Page Size of the file uploaded
        /// </summary>
        public int PageSize { get; set; }
    }
}
