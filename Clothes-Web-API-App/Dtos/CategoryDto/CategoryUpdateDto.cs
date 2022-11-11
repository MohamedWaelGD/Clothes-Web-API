namespace Clothes_Web_API_App.Dtos.CategoryDto
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; } = "#000000";
        public string TextColor { get; set; } = "#ffffff";
    }
}
