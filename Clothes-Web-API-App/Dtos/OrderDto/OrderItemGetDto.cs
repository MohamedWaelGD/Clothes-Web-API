using Clothes_Web_API_App.Dtos.ClothItemDto;

namespace Clothes_Web_API_App.Dtos.OrderDto
{
    public class OrderItemGetDto
    {
        public int OrderId { get; set; }
        public OrderGetDto Orider { get; set; }
        public int ClothItemId { get; set; }
        public ClothItemGetDto ClothItem { get; set; }
        public int Quantity { get; set; }
    }
}
