namespace Clothes_Web_API_App.Models
{
    public class Cloth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Brand { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ClothItem>? ClothItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
