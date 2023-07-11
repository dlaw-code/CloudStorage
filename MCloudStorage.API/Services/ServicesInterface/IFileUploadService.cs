using MCloudStorage.Data.Models.Request;

namespace MCloudStorage.API.Services.ServicesInterface
{
    public interface IFileUploadService
    {
        /// <summary>
        /// Uploads a document to the file storage provider.
        /// </summary>
        /// <param name="fileUploadData">The file upload details.</param>
        /// <returns>A tuple containing the file retrieval link and reference.</returns>
        (string FileRetrievalLink, string FileRetrievalReference) UploadDocument(FileUploadData fileUploadData);
    }
}
