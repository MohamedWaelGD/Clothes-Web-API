using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Dtos.ClothDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Clothes_Web_API_App.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Clothes_Web_API_App.Repository.ClothRepository
{
    public class ClothRepository : RepositoryBase<Cloth>, IClothRepository
    {
        public ClothRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Cloth> GetClothById(int id)
        {
            return await FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Cloth>> GetCloths(ClothSpec spec, PagingParameters pagingParameters)
        {
            var dataQuery = FindAll().Include(e => e.ClothItems).ThenInclude(i => i.ClothImages);

            var result = GetFiltering(dataQuery, spec);

            return await Task.FromResult(PagedList<Cloth>.GetPagedList(
                result, 
                pagingParameters.PageNumber, 
                pagingParameters.PageSize));
        }

        private IQueryable<Cloth> GetFiltering(IQueryable<Cloth> data, ClothSpec spec)
        {
            if (spec.Color != null)
                data = data.
                    Where(e => e.ClothItems.
                        Any(e => e.Color.
                            Contains(spec.Color
                        )));

            if (spec.Name != null)
                data = data.Where(e => e.Name.Contains(spec.Name));
        
            if (spec.SizeType != null)
                data = data.
                    Where(e => e.ClothItems.
                        Any(e => e.Size == spec.SizeType));

            if (spec.Brand != null)
                data = data.Where(e => e.Brand.Contains(spec.Brand));

            if (spec.CategoryId != null)
                data = data.Where(e => e.CategoryId == spec.CategoryId);

            if (spec.MinPrice != null)
                data = data.Where(e => e.ClothItems.Any(e => e.Price >= spec.MinPrice));

            if (spec.MaxPrice != null)
                data = data.Where(e => e.ClothItems.Any(e => e.Price <= spec.MaxPrice));

            return data;
        }
    }
}
