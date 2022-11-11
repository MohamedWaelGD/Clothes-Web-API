namespace Clothes_Web_API_App.Dtos.ClothImageDto
{
    public class ClothImageCreateDto
    {
        public int ClothItemId { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
