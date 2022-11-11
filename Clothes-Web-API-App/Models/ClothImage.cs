namespace Clothes_Web_API_App.Models
{
    public class ClothImage
    {
        public int Id { get; set; }
        public int ClothItemId { get; set; }
        public ClothItem ClothItem { get; set; }
        public string ImagePath { get; set; }
    }
}
