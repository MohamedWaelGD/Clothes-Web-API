using System.ComponentModel.DataAnnotations;

namespace Clothes_Web_API_App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserAddressId { get; set; }
        public UserAddress UserAddress { get; set; }
        public int UserNumberId { get; set; }
        public UserNumber UserNumber { get; set; }
        public DateTime OrderAt { get; set; }
        [Range(0, 100)]
        public double Discount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
