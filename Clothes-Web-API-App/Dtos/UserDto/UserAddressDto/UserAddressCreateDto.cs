namespace Clothes_Web_API_App.Dtos.UserDto.UserAddressDto
{
    public class UserAddressCreateDto
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
