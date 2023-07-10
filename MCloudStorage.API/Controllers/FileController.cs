using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace MCloudStorage.API.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] FileUploadData uploadData)
        {
            var (FileRetrievalLink, FileRetrievalReference) = _fileUploadService.UploadDocument(uploadData);

            return Ok(new
            {
                FileRetrievalLink,
                FileRetrievalReference
            });
        }
    }
}
