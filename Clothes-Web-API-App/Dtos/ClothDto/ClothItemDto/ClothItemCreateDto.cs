using Clothes_Web_API_App.Dtos.ClothImageDto;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Dtos.ClothItemDto
{
    public class ClothItemCreateDto
    {
        public string Color { get; set; }
        public SizeType Size { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ClothId { get; set; }
    }
}
