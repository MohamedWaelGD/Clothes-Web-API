namespace Clothes_Web_API_App.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Orider { get; set; }
        public int ClothItemId { get; set; }
        public ClothItem ClothItem { get; set; }
        public int Quantity { get; set; }
    }
}
