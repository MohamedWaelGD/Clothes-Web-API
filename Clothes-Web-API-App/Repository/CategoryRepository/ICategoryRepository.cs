using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.CategoryRepository
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<Category> GetCategoryById(int id);
    }
}
