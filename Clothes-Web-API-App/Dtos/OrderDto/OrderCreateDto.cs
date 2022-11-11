using System.ComponentModel.DataAnnotations;

namespace Clothes_Web_API_App.Dtos.OrderDto
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public int UserAddressId { get; set; }
        public int UserNumberId { get; set; }
        public DateTime OrderAt { get; set; }
        [Range(0, 100)]
        public double Discount { get; set; }
    }
}
