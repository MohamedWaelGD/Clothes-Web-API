namespace Clothes_Web_API_App.Dtos.UserDto
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? ProfilePicturePath { get; set; }
        public DateTime dateTime { get; set; }
    }
}
