using MCloudStorage.Data.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace MCloudStorage.Data.Models.Request
{
    /// <summary>
    /// Class for receiving file upload data from the client.
    /// </summary>
    public class FileUploadData
    {
        [Required(ErrorMessage = "File is required.")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "File name is required.")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "File type is required.")]
        public FileType FileType { get; set; }

        [Required(ErrorMessage = "Parent location is required.")]
        public string ParentLocation { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
    }
}
