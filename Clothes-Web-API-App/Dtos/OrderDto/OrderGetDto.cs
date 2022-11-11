using Clothes_Web_API_App.Dtos.UserDto;
using Clothes_Web_API_App.Dtos.UserDto.UserAddressDto;
using Clothes_Web_API_App.Dtos.UserDto.UserNumberDto;

namespace Clothes_Web_API_App.Dtos.OrderDto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserGetDto User { get; set; }
        public int UserAddressId { get; set; }
        public UserAddressGetDto UserAddress { get; set; }
        public int UserNumberId { get; set; }
        public UserNumberGetDto UserNumber { get; set; }
        public DateTime OrderAt { get; set; }
        public double Discount { get; set; }
        public ICollection<OrderItemGetDto> OrderItems { get; set; }
    }
}
