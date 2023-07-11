using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Models.Request;


namespace MCloudStorage.API.Services.Implementation
{
    public class LocalFileUploadService : IFileUploadService
    {
        private const string FileUploadBasePath = "C:\\Files";

        /// <inheritdoc />
        public (string FileRetrievalLink, string FileRetrievalReference) UploadDocument(FileUploadData fileUploadData)
        {
            string uploadDirectory = TryCreateUploadFolder(fileUploadData.UserId, fileUploadData.ParentLocation);
            string uniqueFilename = GetUniqueFileNameWithExtension(fileUploadData.File);

            string absoluteFilePath = Path.Combine(uploadDirectory, uniqueFilename);

            SaveFileToSpecifiedLocation(absoluteFilePath, fileUploadData.File);

            return (absoluteFilePath, uniqueFilename);
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
    }
}

