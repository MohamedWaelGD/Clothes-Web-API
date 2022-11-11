namespace Clothes_Web_API_App.Dtos.UserDto
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; }
        public DateTime dateTime { get; set; }
        /*
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserAddressRepository>? UserAddresses { get; set; }
        public ICollection<UserNumber>? UserNumbers { get; set; }
        public ICollection<Order>? Orders { get; set; }
        */
    }
}
