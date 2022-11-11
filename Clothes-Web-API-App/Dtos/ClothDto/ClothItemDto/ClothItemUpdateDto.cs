using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Dtos.ClothItemDto
{
    public class ClothItemUpdateDto
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public SizeType Size { get; set; }
        public double Pricee { get; set; }
        public int Quantity { get; set; }
        public int ClothId { get; set; }
    }
}
