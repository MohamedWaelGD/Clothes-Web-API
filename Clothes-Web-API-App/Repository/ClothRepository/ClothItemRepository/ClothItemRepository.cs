using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Web_API_App.Repository.ClothItemRepository
{
    public class ClothItemRepository : RepositoryBase<ClothItem>, IClothItemRepository
    {
        public ClothItemRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<ClothItem> GetClothItemById(int id)
        {
            return await FindByCondition(e => e.Id == id).Include(e => e.ClothImages).FirstOrDefaultAsync();
        }

        public async Task<ICollection<ClothItem>> GetClothItems(int clothId)
        {
            return await FindByCondition(e => e.ClothId == clothId).Include(e => e.ClothImages).ToListAsync();
        }
    }
}
