using Clothes_Web_API_App.Dtos.ClothDto;
using Clothes_Web_API_App.Dtos.UserDto;

namespace Clothes_Web_API_App.Dtos.ReviewDto
{
    public class ReviewGetDto
    {
        public int ClothId { get; set; }
        public ClothGetDto Cloth { get; set; }
        public int UserId { get; set; }
        public UserGetDto User { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
    }
}
