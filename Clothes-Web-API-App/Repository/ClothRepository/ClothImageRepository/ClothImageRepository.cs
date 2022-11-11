using BloggerAPIApp.Services.ImagesServices;
using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Dtos.ClothImageDto;
using Clothes_Web_API_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Web_API_App.Repository.ClothImageRepository
{
    public class ClothImageRepository : RepositoryBase<ClothImage>, IClothImageRepository
    {
        private readonly IImageService _imageService;

        public ClothImageRepository(AppDbContext appDbContext, IImageService imageService) : base(appDbContext)
        {
            _imageService = imageService;
        }

        public async Task CreateClothImage(ClothImage entity, IFormFile imageFile)
        {
            entity.ImagePath = await _imageService.UploadImage(imageFile);
            await Create(entity);
        }

        public async Task<ClothImage> GetClothImageById(int id)
        {
            return await FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<ClothImage>> GetClothImages(int clothItemId)
        {
            return await FindByCondition(e => e.ClothItemId == clothItemId).ToListAsync();
        }
    }
}
