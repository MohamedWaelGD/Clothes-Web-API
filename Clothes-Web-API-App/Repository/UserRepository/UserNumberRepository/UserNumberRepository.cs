using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.UserRepository.UserNumberRepository
{
    public class UserNumberRepository : RepositoryBase<UserNumber>, IUserNumberRepository
    {
        public UserNumberRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
