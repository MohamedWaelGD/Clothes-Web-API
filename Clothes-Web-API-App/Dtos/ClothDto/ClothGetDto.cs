using Clothes_Web_API_App.Dtos.ClothItemDto;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Dtos.ClothDto
{
    public class ClothGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ClothItemGetDto>? ClothItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
