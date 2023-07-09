using MCloudStorage.Data.Dtos;

namespace MCloudStorage.API.Services.ServicesInterface
{
    public interface ICloudinaryUpload
    {
        public Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images, IFormFile[] videos);
        //Task<DocumentDto> UploadDocument(IFormFile file, string filename, string fileUploadType, string parentLocation, int userId, string fileType);
    }
}
