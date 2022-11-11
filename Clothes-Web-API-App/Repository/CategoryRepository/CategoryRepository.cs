using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Web_API_App.Repository.CategoryRepository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
