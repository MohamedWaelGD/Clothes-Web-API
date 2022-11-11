using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.UserRepository.UserAddressRepository
{
    public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
