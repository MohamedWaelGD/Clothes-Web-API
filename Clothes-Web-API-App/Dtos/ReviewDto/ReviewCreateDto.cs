using System.ComponentModel.DataAnnotations;

namespace Clothes_Web_API_App.Dtos.ReviewDto
{
    public class ReviewCreateDto
    {
        public int ClothId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; } = 3;
    }
}
