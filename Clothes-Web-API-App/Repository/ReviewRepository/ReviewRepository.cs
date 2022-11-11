using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Dtos.ReviewDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Web_API_App.Repository.ReviewRepository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;

        public ReviewRepository(AppDbContext appDbContext, IUserRepository userRepository) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }

        public async Task<ResponseApi<ICollection<Review>>> GetClothReviews(int clothId)
        {
            return ResponseApi<ICollection<Review>>.GenerateSuccessMessage("", await FindByCondition(e => e.ClothId == clothId).ToListAsync());
        }

        public async Task<ResponseApi<ICollection<Review>>> GetActiveUserReviews()
        {
            var response = await _userRepository.GetAuthorizedUser();

            if (!response.Success)
                return ResponseApi<ICollection<Review>>.GenerateErrorMessage("User not authorized");

            var review = await FindByCondition(e => e.UserId == response.Data.Id).ToListAsync();

            return ResponseApi<ICollection<Review>>.GenerateSuccessMessage("", review);
        }

        public async Task<ResponseApi<ICollection<Review>>> GetUserReviewsById(int userId)
        {
            if (!await _userRepository.IsUserExists(userId))
            {
                return ResponseApi<ICollection<Review>>.GenerateErrorMessage("User not found");
            }

            return ResponseApi<ICollection<Review>>.GenerateSuccessMessage("", await FindByCondition(e => e.UserId == userId).ToListAsync());
        }

        public async Task<ResponseApi<bool>> DeleteReview(int userId, int clothId)
        {
            var review = await FindFirstByCondition(e => e.UserId == userId && e.ClothId == clothId);

            if (review == null)
                return ResponseApi<bool>.GenerateErrorMessage("Review not found.", false);

            _appDbContext.Reviews.Remove(review);
            await _appDbContext.SaveChangesAsync();

            return ResponseApi<bool>.GenerateSuccessMessage("Review deleted.", true);
        }

        public async Task<ResponseApi<bool>> DeleteReviewByActiveUser(int clothId)
        {
            var response = await _userRepository.GetAuthorizedUser();

            if (!response.Success)
                return ResponseApi<bool>.GenerateErrorMessage("User not authorized", false);

            var review = await FindFirstByCondition(e => e.UserId == response.Data.Id && e.ClothId == clothId);

            if (review == null)
                return ResponseApi<bool>.GenerateErrorMessage("Review not found.", false);

            _appDbContext.Reviews.Remove(review);
            await _appDbContext.SaveChangesAsync();

            return ResponseApi<bool>.GenerateSuccessMessage("Review deleted.", true);
        }
    }
}
