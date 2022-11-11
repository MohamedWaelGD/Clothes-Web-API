namespace Clothes_Web_API_App.Dtos.UserDto.UserNumberDto
{
    public class UserNumberGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserGetDto User { get; set; }
        public string number { get; set; }
    }
}
