using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;

namespace Clothes_Web_API_App.Repository.ClothImageRepository
{
    public interface IClothImageRepository : IRepositoryBase<ClothImage>
    {
        Task<ICollection<ClothImage>> GetClothImages(int clothItemId);
        Task<ClothImage> GetClothImageById(int id);
        Task CreateClothImage(ClothImage entity, IFormFile imageFile);
    }
}
