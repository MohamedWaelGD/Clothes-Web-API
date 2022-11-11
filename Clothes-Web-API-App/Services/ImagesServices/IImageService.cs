namespace BloggerAPIApp.Services.ImagesServices
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile imageFile);
    }
}
