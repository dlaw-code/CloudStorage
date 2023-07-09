using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCloudStorage.Data.Entities
{
    public class FileUploadData
    {
        [Required(ErrorMessage = "File is required.")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Filename is required.")]
        public string? Filename { get; set; }

        [Required(ErrorMessage = "File upload type is required.")]
        public string? FileUploadType { get; set; }

        [Required(ErrorMessage = "Parent location is required.")]
        public string? ParentLocation { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "File type is required.")]
        public string? FileType { get; set; }
        


        
    }
}
