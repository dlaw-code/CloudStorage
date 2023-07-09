using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MCloudStorage.API.Data;
using MCloudStorage.API.Services.ServicesInterface;

namespace MCloudStorage.API.Services.Implementation
{
    public class CloudinaryUpload : ICloudinaryUpload

    {
        private readonly DocumentStoreContext dbContext;
        private readonly Cloudinary cloudinary;
        public CloudinaryUpload(DocumentStoreContext dbContext, Cloudinary cloudinary)
        {
            this.dbContext = dbContext;
            this.cloudinary = cloudinary;

        }

        public async Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images, IFormFile[] videos)
        {

            if ((images == null || images.Length == 0) && (videos == null || videos.Length == 0))
            {
                return "no images or videos to upload";
            }





            var document = await dbContext.Documents.FirstOrDefaultAsync(c => c.UserId == userId);
            string avatar = "";

            if (images != null && images.Length > 0)
            {
                foreach (var image in images)
                {
                    var result = await cloudinary.UploadAsync(new ImageUploadParams
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream())
                    }).ConfigureAwait(false);

                    avatar += result.Url;
                }
            }

            if (videos != null && videos.Length > 0)
            {
                foreach (var video in videos)
                {
                    var result = await cloudinary.UploadAsync(new VideoUploadParams
                    {
                        File = new FileDescription(video.FileName, video.OpenReadStream())
                    }).ConfigureAwait(false);

                    avatar += result.Url;
                }
            }

            //foreach (var image in images)
            //{
            //    var result = await cloudinary.UploadAsync(new ImageUploadParams
            //    {
            //        File = new FileDescription(image.FileName, image.OpenReadStream())
            //    }).ConfigureAwait(false);
            //    avatar += result.Url;
            //}


            if (avatar.Length == 0) return "Failed to upload";

            document.Avatar = avatar;
            dbContext.Documents.Update(document);
            await dbContext.SaveChangesAsync();


            return "Done";

        }
    }
}






//var user = dbContext.Documents.FirstOrDefault(c => c.Id == userId);

//if (user == null)
//{
//    return "Couldnt find user";
//}
//if (userId != user.Id)
//{
//    throw (new Exception("Cannot upload for another user"));
//}
//await cloudinary.DeleteResourcesAsync(user.Avatar);
