using System.ComponentModel.DataAnnotations;

namespace Clothes_Web_API_App.Models
{
    public class Review
    {
        public int ClothId { get; set; }
        public Cloth Cloth { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
    }
}
