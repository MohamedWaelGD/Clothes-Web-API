using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Specification
{
    public class ClothSpec
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public int? CategoryId { get; set; }
        public SizeType? SizeType { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
