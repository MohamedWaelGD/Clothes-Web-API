using Clothes_Web_API_App.Dtos.ReviewDto;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.ReviewRepository
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<ResponseApi<ICollection<Review>>> GetClothReviews(int clothId);
        Task<ResponseApi<ICollection<Review>>> GetActiveUserReviews();
        Task<ResponseApi<ICollection<Review>>> GetUserReviewsById(int userId);
        Task<ResponseApi<bool>> DeleteReview(int userId, int clothId);
        Task<ResponseApi<bool>> DeleteReviewByActiveUser(int clothId);
    }
}
