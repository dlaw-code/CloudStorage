using MCloudStorage.Data.Models.Request;
using MCloudStorage.Data.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace MCloudStorage.API.Services.ServicesInterface
{
    public interface IFileUploadService
    {
        /// <summary>
        /// Uploads a document to the file storage provider.
        /// </summary>
        /// <param name="fileUploadData">The file upload details.</param>
        /// <returns>A tuple containing the file retrieval link and reference.</returns>
        (string FileRetrievalLink, string FileRetrievalReference) UploadLocalDocument(FileUploadData fileUploadData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mediaFiles"></param>
        /// <returns>This upload files to the cloudinary for retrieval purposes</returns>
        Task<(string FileRetrievalLink, string FileRetrievalReference)> UploadCloudinaryDocument(FileUploadData fileUploadData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>This returns the user paginated response</returns>
        UserUploadsResponseDto GetUserUploads(string userId, int page, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>This returns the user summary for dashboard purposes</returns>
        UserSummaryDto GetUserSummary(string userId);

        /// <summary>
        /// This return the file shared by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ShareFileWithUser(int fileId, string senderUserId, string receiverUserId);
        
    }
}