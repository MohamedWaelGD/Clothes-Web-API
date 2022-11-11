namespace Clothes_Web_API_App.Models
{
    public class ClothItem
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public SizeType Size { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ClothId { get; set; }
        public Cloth Cloth { get; set; }
        public ICollection<ClothImage>? ClothImages { get; set; }
    }
}
