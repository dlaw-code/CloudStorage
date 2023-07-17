namespace MCloudStorage.Data.Models.Response
{
    public class UserUploadsResponseDto
    {
        public List<DocumentDto> UserUploads { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
