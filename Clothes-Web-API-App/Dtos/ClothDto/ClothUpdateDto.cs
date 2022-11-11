namespace Clothes_Web_API_App.Dtos.ClothDto
{
    public class ClothUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Brand { get; set; }
        public int CategoryId { get; set; }
    }
}
