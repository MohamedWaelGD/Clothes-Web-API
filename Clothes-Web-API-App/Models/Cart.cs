namespace Clothes_Web_API_App.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public HashSet<CartItem> Cloths { get; set; } = new HashSet<CartItem>();
    }
}
