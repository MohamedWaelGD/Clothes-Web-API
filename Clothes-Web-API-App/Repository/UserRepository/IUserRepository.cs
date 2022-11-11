using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.UserRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<ResponseApi<User>> Create(User user, string password, IFormFile profilePicture);
        Task<ResponseApi<User>> Update(User user, string password, IFormFile profilePicture);
        Task<ResponseApi<string>> Login(string email, string password);
        Task<ResponseApi<User>> GetAuthorizedUser();
        Task<bool> IsUserExists(int id);
        int GetAuthorizedUserId();
        Task<ResponseApi<string>> RefreshToken();
    }
}
