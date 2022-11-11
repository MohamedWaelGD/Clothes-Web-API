using Microsoft.Net.Http.Headers;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace BloggerAPIApp.Services.ImagesServices
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImage(IFormFile imageFile)
        {
            try
            {
                var folderName = Path.Combine("wwwroot", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (imageFile.Length > 0)
                {
                    Guid guid = Guid.NewGuid();
                    var fileName = guid.ToString() + "-" + ContentDispositionHeaderValue.Parse(imageFile.ContentDisposition).FileName.Trim();
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    var dbContext = _configuration.GetSection("MainURL").Value + "/images/" + fileName;

                    return dbContext;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
