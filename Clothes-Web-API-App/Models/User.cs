using System.ComponentModel.DataAnnotations;

namespace Clothes_Web_API_App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ProfilePicturePath { get; set; }
        public UserRole UserRole { get; set; } = UserRole.Client;
        public DateTime dateTime { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserAddress>? UserAddresses { get; set; }
        public ICollection<UserNumber>? UserNumbers { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
