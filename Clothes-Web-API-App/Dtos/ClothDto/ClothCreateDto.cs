using Clothes_Web_API_App.Dtos.ClothItemDto;
using Clothes_Web_API_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Dtos.ClothDto
{
    public class ClothCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Brand { get; set; }
        public int CategoryId { get; set; }
    }
}
