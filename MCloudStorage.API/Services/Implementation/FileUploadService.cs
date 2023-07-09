using MCloudStorage.API.Entities.enums;
using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Dtos;
using MCloudStorage.Data.Repository.RepositoryInterface;


namespace MCloudStorage.API.Services.Implementation
{
    public class FileUploadService : IFileUploadService

    {
        private readonly ICloudinaryUpload _cloudinaryService;
        private readonly IFileRepository _fileRepository;//Dependency injection

        public FileUploadService(ICloudinaryUpload cloudinaryService, IFileRepository fileRepository)
        {
            _cloudinaryService = cloudinaryService;
            _fileRepository = fileRepository;

        }
        public async Task<DocumentDto> UploadDocument(string filename, string fileUploadType, string parentLocation, string userId, string fileType, IFormFile file)
        {
            // Specify the local directory where you want to store the uploaded document
            string uploadDirectory = "C:\\Files"; // Replace with your desired directory path

            // Create the upload directory if it doesn't exist
            if (!Directory.Exists(uploadDirectory))         
            {
                Directory.CreateDirectory(uploadDirectory);
            }


            

            // Generate a unique filename to avoid overwriting existing files
            string uniqueFilename = GetUniqueFileNameWithExtension(filename);
            //string filePath = Path.Combine(uploadDirectory, uniqueFilename);
            string filePath = Path.Combine(uploadDirectory, $"{uniqueFilename}.{fileType}");


            // Save the file to the specified location
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                // Use the appropriate method to save the uploaded file
                // Here, we assume that the file data is passed as a byte array
                // Adjust this code based on your file upload mechanism
                // For example, if you are using ASP.NET Core, you can use IFormFile and CopyToAsync method to save the file stream
                byte[] fileData = GetFileDataFromUploadService(file); // Replace this with the code to retrieve the file data
                fileStream.Write(fileData, 0, fileData.Length);
            }

            // Get the filesize in bytes using the FileInfo class
            FileInfo fileInfo = new FileInfo(filePath);
            long filesize = fileInfo.Length;

            //DocumentDto cloudinaryUploadedDocument = await _cloudinaryService.UploadDocument(file, uniqueFilename, fileUploadType, parentLocation, userId, fileType);

            // Create a new Document object with the necessary information
            Document uploadedDocument = new Document
            {

                Filename = uniqueFilename,
                FileUploadType = fileUploadType,
                ParentLocation = parentLocation,
                UserId = userId,
                FileType = fileType,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Filelink = filePath,
                FileSize = filesize,
                FileReference = uniqueFilename,
                FileStatus = FileStatus.Created

        };

            // Set the FileStatus to Deleted if necessary
            if (fileUploadType == "delete")
            {
                uploadedDocument.FileStatus = FileStatus.Deleted;
            }



            _fileRepository.Add(uploadedDocument);
            

            var doc = new DocumentDto();
            doc.Filename = uploadedDocument.Filename;
            doc.UserId = uploadedDocument.UserId;
            doc.FileUploadType = uploadedDocument.FileUploadType;
            doc.FileType = uploadedDocument.FileType;
            doc.ParentLocation = uploadedDocument.ParentLocation;

            
            return doc;

        }

        

        

        

       










        private string GetUniqueFileNameWithExtension(string filename)
        {
            // Generate a unique filename using a combination of the original filename and a unique identifier
            string uniqueIdentifier = Guid.NewGuid().ToString("N");
            //string extension = Path.GetExtension(filename);
            string uniqueFilename = $"{Path.GetFileNameWithoutExtension(filename)}_{uniqueIdentifier}";
            return uniqueFilename;
        }

        private byte[] GetFileDataFromUploadService(IFormFile file) //Might not need it
        {
            // Replace this method with the code to retrieve the file data from your file upload service
            // This method will depend on the specific file upload mechanism you are using
            // For example, if you are using ASP.NET Core, you can access the uploaded file data from the IFormFile object
            // You may need to adjust this code based on your specific requirements and file upload implementation
            byte[] fileData = null;
            // Retrieve the file data from your file upload service
            // ...
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileData = memoryStream.ToArray();
            }


            return fileData;
        }
    }
}

