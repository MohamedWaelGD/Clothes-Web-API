using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;

namespace Clothes_Web_API_App.Repository.ClothItemRepository
{
    public interface IClothItemRepository : IRepositoryBase<ClothItem>
    {
        Task<ICollection<ClothItem>> GetClothItems(int clothId);
        Task<ClothItem> GetClothItemById(int id);
    }
}
