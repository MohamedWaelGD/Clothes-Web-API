using Clothes_Web_API_App.Dtos.ClothDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Clothes_Web_API_App.Specification;

namespace Clothes_Web_API_App.Repository.ClothRepository
{
    public interface IClothRepository: IRepositoryBase<Cloth>
    {
        Task<PagedList<Cloth>> GetCloths(ClothSpec spec, PagingParameters pagingParameters);
        Task<Cloth> GetClothById(int id);
    }
}
