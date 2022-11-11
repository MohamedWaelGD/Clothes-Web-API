using Clothes_Web_API_App.Dtos.ClothImageDto;
using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Dtos.ClothItemDto
{
    public class ClothItemGetDto
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public SizeType Size { get; set; }
        public double Pricee { get; set; }
        public int Quantity { get; set; }
        public int ClothId { get; set; }
        public Cloth Cloth { get; set; }
        public ICollection<ClothImageGetDto>? ClothImages { get; set; }
    }
}
