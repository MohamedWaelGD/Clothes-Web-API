namespace Clothes_Web_API_App.Models
{
    public class UserNumber
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string number { get; set; }
    }
}
