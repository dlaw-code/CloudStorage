using MCloudStorage.Data.Dtos;

namespace MCloudStorage.API.Services.ServicesInterface
{
    public interface IFileUploadService
    {
        Task<DocumentDto> UploadDocument(string filename, string fileUploadType, string parentLocation, string userId, string fileType, IFormFile file);
    }
}
