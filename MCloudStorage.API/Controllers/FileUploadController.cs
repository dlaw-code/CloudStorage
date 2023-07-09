using MCloudStorage.API.Data;
using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Entities;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCloudStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase

    {
        private readonly DocumentStoreContext dbContext;
        private readonly IFileUploadService _imageuploadService;//Dependency injection
        private readonly ICloudinaryUpload _cloudinaryUpload;
        

        public FileUploadController(DocumentStoreContext dbContext,IFileUploadService imageuploadService, ICloudinaryUpload cloudinaryUpload)
        {
            _imageuploadService = imageuploadService;
            _cloudinaryUpload = cloudinaryUpload;
            this.dbContext = dbContext;





        }


        [HttpPost("upload")]
        
        public IActionResult Upload([FromForm] FileUploadData uploadData)
        { 
            if (uploadData.File == null || uploadData.File.Length == 0)
                return BadRequest("File is missing.");

            try
            {
                var document = _imageuploadService.UploadDocument(
                    uploadData.Filename!,
                    uploadData.FileUploadType!,
                    uploadData.ParentLocation!,
                    uploadData.UserId,
                    uploadData.FileType!,
                    uploadData.File
             );
                string uploadDirectory = "C:\\Files";
                string filePath = Path.Combine(uploadDirectory, uploadData.Filename!);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadData.File.CopyTo(fileStream);
                }

                // Return the uploaded document details
                return Ok(document);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the file.");
            }

            
        }

        [HttpPatch("cloudinaryUpload")]
        

        public async Task <IActionResult> CloudinaryUpload(string userId, IFormFile[] images, IFormFile[] videos)
        {
            return Ok (await _cloudinaryUpload.UpdateUserPhotosAsync(userId, images, videos));

        }


        [HttpGet("userUploads")]
        public async Task<IActionResult> GetUserUploads(string userId, int page = 1, int pageSize = 10)
        {
            var userUploads = await dbContext.Documents
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(userUploads);
        }

        

        [HttpGet("userSummary")]
        public async Task<IActionResult> GetUserSummary(string userId)
        {
            try
            {
                var totalUploads = await dbContext.Documents.CountAsync(d => d.UserId == userId);
                var latestUploads = await dbContext.Documents
                    .Where(d => d.UserId == userId)
                    .OrderByDescending(d => d.CreatedAt)
                    .Take(5) // Change the number as needed
                    .Select(d => new
                    {
                        FileName = d.Filename,
                        FileSize = d.FileSize,
                        CreatedAt = d.CreatedAt
                    })
                    .ToListAsync();

                var storageUsage = await dbContext.Documents
                    .Where(d => d.UserId == userId)
                    .SumAsync(d => d.FileSize);

                var userSummary = new
                {
                    TotalUploads = totalUploads,
                    LatestUploads = latestUploads,
                    StorageUsage = storageUsage
                };

                return Ok(userSummary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the user summary.");
            }
        }



        [HttpDelete]
        [Route("{id}")]
        
        public async Task<IActionResult> DeleteUpload([FromRoute] int id)
        {
            var document = await dbContext.Documents.FindAsync(id);

            if (document != null)
            {
                dbContext.Remove(document);
                await dbContext.SaveChangesAsync();
                return Ok(document);
            }

            return NotFound();
        }

    }
}
