using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using MCloudStorage.API.Data;
using MCloudStorage.API.Entities.Enums;
using MCloudStorage.Data.Entities;
using MCloudStorage.Data.Models.Response;

namespace MCloudStorage.API.Services.Implementation
{
    public class FileUploadService : IFileUploadService
    {
        private const string FileUploadBasePath = "C:\\Files";
        private readonly DocumentStoreContext _dbContext;
        private readonly Cloudinary _cloudinary;
        
        public FileUploadService(DocumentStoreContext dbContext, Cloudinary cloudinary)
        {
            _dbContext = dbContext;
            _cloudinary = cloudinary;
        }

        /// <inheritdoc />
        public (string FileRetrievalLink, string FileRetrievalReference) UploadDocument(FileUploadData fileUploadData)
        {
            string uploadDirectory = TryCreateUploadFolder(fileUploadData.UserId, fileUploadData.ParentLocation);
            string uniqueFilename = GetUniqueFileNameWithExtension(fileUploadData.File);

            string absoluteFilePath = Path.Combine(uploadDirectory, uniqueFilename);

            SaveFileToSpecifiedLocation(absoluteFilePath, fileUploadData.File);
            


            var document = new Document
            {
                FileName = fileUploadData.FileName,
                FileReference = uniqueFilename,
                FileLink = absoluteFilePath,
                ParentLocation = fileUploadData.ParentLocation,
                UserId = fileUploadData.UserId,
                FileSize = fileUploadData.File.Length,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt= DateTime.UtcNow,
                FileType = fileUploadData.FileType,
                FileStatus = FileStatus.Created // Set the appropriate file status here
                
            };

            _dbContext.Documents.Add(document);
            _dbContext.SaveChanges();

            var sharedFile = new SharedFile
            {
                //FileId = document.Id,
                //Document = document,
                Id = document.Id, // Set the Id property explicitly
                DocumentId = document.Id, // Set the DocumentId foreign key property
                UserId = fileUploadData.UserId,
                SharedAt = DateTime.UtcNow
            };

            _dbContext.SharedFiles.Add(sharedFile);
            _dbContext.SaveChanges();

            return (absoluteFilePath, uniqueFilename);
            //_fileRepository.Add(document);
        }

        private static void SaveFileToSpecifiedLocation(string filePath, IFormFile file)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                // Use the appropriate method to save the uploaded file
                // Here, we assume that the file data is passed as a byte array
                // Adjust this code based on your file upload mechanism
                // For example, if you are using ASP.NET Core, you can use IFormFile and CopyToAsync method to save the file stream
                byte[] fileData = ConvertFormFileToByteArray(file);
                fileStream.Write(fileData, 0, fileData.Length);
            }
        }


        /// <summary>
        /// Convert a form file to a byte array.
        /// </summary>
        /// <param name="file">The form file to convert.</param>
        /// <returns>A byte array representing the form file.</returns>
        private static byte[] ConvertFormFileToByteArray(IFormFile file)
        {
            byte[] fileData;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileData = memoryStream.ToArray();
            }

            return fileData;
        }

        /// <summary>
        /// Try to create an upload folder for a particular user from the specified storage parent.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="parentLocation">The storage parent to associate the user's upload folder with.</param>
        private static string TryCreateUploadFolder(string userId, string parentLocation)
        {
            string uploadDirectory = Path.Combine(FileUploadBasePath, userId, parentLocation);
            // Create the upload directory if it doesn't exist
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            return uploadDirectory;
        }

        /// <summary>
        /// Generates a unique filename using a combination of the original filename and a unique identifier.
        /// </summary>
        /// <param name="fileName">The original file name</param>
        /// <returns>The unique file name.</returns>
        private static string GetUniqueFileNameWithExtension(IFormFile file)
        {
            string originalFileName = file.FileName;
            string originalFileExtension = Path.GetExtension(originalFileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            string uniqueIdentifier = Guid.NewGuid().ToString("N");
            return $"{Path.GetFileNameWithoutExtension(fileNameWithoutExtension)}_{uniqueIdentifier}{originalFileExtension}";
        }


        /// <summary>
        /// Uploading to the Cloudinary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mediaFiles"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> UpdateUserMediaAsync(string userId, List<IFormFile> mediaFiles)
        {
            if (mediaFiles == null || mediaFiles.Count == 0)
            {
                return "No media file";
            }

            var document = await _dbContext.Documents.FirstOrDefaultAsync(c => c.UserId == userId);
            if (document == null)
            {
                return "Couldn't find user";
            }

            foreach (var mediaFile in mediaFiles)
            {
                var uploadParams = new ImageUploadParams();

                if (IsImageFile(mediaFile))
                {
                    uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(mediaFile.FileName, mediaFile.OpenReadStream())
                    };
                }
                else if (IsVideoFile(mediaFile))
                {
                    uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(mediaFile.FileName, mediaFile.OpenReadStream())
                    };
                }
                else
                {
                    return "Invalid media file type";
                }

                var result = await _cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);
                document.Avatar += result.Url;
            }

            if (string.IsNullOrEmpty(document.Avatar))
            {
                return "Failed to upload";
            }

            _dbContext.Documents.Update(document);
            await _dbContext.SaveChangesAsync();

            return "Upload successful";
        }

        private bool IsImageFile(IFormFile file)
        {
            var imageExtensions = new[] { ".jpeg", ".jpg", ".png", ".gif" };//More extensions can be added as needed
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return imageExtensions.Contains(extension);
        }

        private bool IsVideoFile(IFormFile file)
        {
            var videoExtensions = new[] { ".mp4", ".avi", ".mov" };//More extensions can be added as needed
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return videoExtensions.Contains(extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public UserUploadsResponseDto GetUserUploads(string userId, int page, int pageSize)
        {
            // Calculate the skip count based on the page number and page size
            int skipCount = (page - 1) * pageSize;

            // Query the documents for the given user, ordered by CreatedAt in descending order
            var query = _dbContext.Documents
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.CreatedAt);

            // Get the total count of documents for the user
            int totalCount = query.Count();

            // Apply pagination to the query
            var userUploads = query.Skip(skipCount).Take(pageSize).ToList();

            // Map the Document entities to DocumentDto objects (assuming you have a DocumentDto class)
            var userUploadsDto = userUploads.Select(document => new DocumentDto
            {
                // Map the properties of the Document entity to the corresponding properties of the DocumentDto
                // Modify the mapping according to your requirements
                FileName = document.FileName,
                ParentLocation = document.ParentLocation,
                UserId = document.UserId,
                FileType = document.FileType,
                
            }).ToList();

            // Create the paginated response DTO
            var responseDto = new UserUploadsResponseDto
            {
                UserUploads = userUploadsDto,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return responseDto;
        }

        public UserSummaryDto GetUserSummary(string userId)
        {
            int totalUploads = _dbContext.Documents.Count(d => d.UserId == userId);
            long totalFileSize = _dbContext.Documents.Where(d => d.UserId == userId).Sum(d => d.FileSize);
            DateTime lastUploadDate = _dbContext.Documents.Where(d => d.UserId == userId)
                                                .OrderByDescending(d => d.CreatedAt)
                                                .Select(d => d.CreatedAt)
                                                .FirstOrDefault();

            var userSummary = new UserSummaryDto
            {
                TotalUploads = totalUploads,
                TotalFileSize = totalFileSize,
                LastUploadDate = lastUploadDate
                // Set other properties of the user summary DTO as needed
            };

            return userSummary;
        }

        public UserUploadsResponseDto GetSharedFiles(string userId)
        {
            var sharedFiles = _dbContext.SharedFiles
                .Include(sf => sf.Document)
                .Where(sf => sf.UserId == userId)
                .OrderByDescending(sf => sf.SharedAt)
                .ToList();

            var sharedFilesDto = sharedFiles.Select(sharedFile => new DocumentDto
            {
                FileName = sharedFile.Document.FileName,
                ParentLocation = sharedFile.Document.ParentLocation,
                UserId = sharedFile.Document.UserId,
                FileType = sharedFile.Document.FileType
            }).ToList();

            var responseDto = new UserUploadsResponseDto
            {
                UserUploads = sharedFilesDto,
                TotalCount = sharedFilesDto.Count
            };

            return responseDto;
        }

    }
}

