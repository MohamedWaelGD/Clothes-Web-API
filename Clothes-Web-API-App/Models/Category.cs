namespace Clothes_Web_API_App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; } = "#000000";
        public string TextColor { get; set; } = "#ffffff";
        public ICollection<Cloth>? Cloths { get; set; }
    }
}
