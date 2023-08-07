using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Models.Request;
using MCloudStorage.Data.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var (FileRetrievalLink, FileRetrievalReference) = _fileUploadService.UploadLocalDocument(uploadData);

            return StatusCode((int)HttpStatusCode.Created, new
            {
                FileRetrievalLink,
                FileRetrievalReference
            });
        }

        [HttpPatch("UploadCloudinary")]
        public async Task<IActionResult> UploadCloudinaryDocument([FromForm] FileUploadData uploadData)
        {
            var (FileRetrievalLink, FileRetrievalReference) = await _fileUploadService.UploadCloudinaryDocument(uploadData);

            return StatusCode((int)HttpStatusCode.Created, new
            {
                FileRetrievalLink,
                FileRetrievalReference
            });
        }

        [HttpGet("{userId}")]
        public ActionResult<UserUploadsResponseDto> GetUserUploads(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userUploads = _fileUploadService.GetUserUploads(userId, page, pageSize);
            return Ok(userUploads);
        }


        [HttpGet("summary/{userId}")]
        public ActionResult<UserSummaryDto> GetUserSummary(string userId)
        {
            var userSummary = _fileUploadService.GetUserSummary(userId);
            return Ok(userSummary);
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareFileWithUser(int fileId, string senderUserId, string receiverUserId)
        {
            try
            {
                await _fileUploadService.ShareFileWithUser(fileId, senderUserId, receiverUserId);
                return Ok("File shared successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return an appropriate error message to the client
            }
        }



    }
}
