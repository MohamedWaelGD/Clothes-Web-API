using BloggerAPIApp.Services.ImagesServices;
using Clothes_Web_API_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseApi<string>> UploadImage([FromForm]IFormFile imageFile)
        {
            var path = await _imageService.UploadImage(imageFile);

            if (string.IsNullOrEmpty(path))
                return ResponseApi<string>.GenerateErrorMessage("Error happened");

            return ResponseApi<string>.GenerateSuccessMessage("Image has been uploaded.", path);
        }
    }
}
