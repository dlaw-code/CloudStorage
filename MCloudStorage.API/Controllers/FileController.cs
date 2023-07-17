﻿using MCloudStorage.API.Services.ServicesInterface;
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
            string userId = uploadData.UserId;
            var (FileRetrievalLink, FileRetrievalReference) = _fileUploadService.UploadDocument(uploadData);

            return StatusCode((int)HttpStatusCode.Created, new
            {
                FileRetrievalLink,
                FileRetrievalReference
            });
        }

        [HttpPatch("cloudinaryUpload")]
        public async Task<IActionResult> UpdateUserMediaAsync(string userId, List<IFormFile> mediaFiles)
        {
            var result = await _fileUploadService.UpdateUserMediaAsync(userId, mediaFiles);

            return Ok(result);
            //if (result.StartsWith("Failed"))
            //{
            //    return BadRequest(result);
            //}
        }

        [HttpGet("{userId}")]
        public ActionResult<UserUploadsResponseDto> GetUserUploads(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userUploads = _fileUploadService.GetUserUploads(userId, page, pageSize);
            return Ok(userUploads);
        }

        
        [HttpGet("user-summary/{userId}")]
        public ActionResult<UserSummaryDto> GetUserSummary(string userId)
        {
            var userSummary = _fileUploadService.GetUserSummary(userId);
            return Ok(userSummary);
        }


        [HttpGet("shared/{userId}")]
        public ActionResult<UserUploadsResponseDto> GetSharedFiles(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var sharedFiles = _fileUploadService.GetSharedFiles(userId);
            return Ok(sharedFiles);
        }


    }
}
