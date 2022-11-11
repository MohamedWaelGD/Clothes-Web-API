using AutoMapper;
using Clothes_Web_API_App.Dtos.CategoryDto;
using Clothes_Web_API_App.Dtos.ClothDto;
using Clothes_Web_API_App.Dtos.ClothImageDto;
using Clothes_Web_API_App.Dtos.ClothItemDto;
using Clothes_Web_API_App.Dtos.OrderDto;
using Clothes_Web_API_App.Dtos.ReviewDto;
using Clothes_Web_API_App.Dtos.UserDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;

namespace Clothes_Web_API_App
{
    public class CustomMapper: Profile
    {
        public CustomMapper()
        {
            CreateMap<Cloth, ClothCreateDto>().ReverseMap();
            CreateMap<Cloth, ClothGetDto>().ReverseMap();
            CreateMap<Cloth, ClothUpdateDto>().ReverseMap();

            CreateMap<ClothItem, ClothItemCreateDto>().ReverseMap();
            CreateMap<ClothItem, ClothItemUpdateDto>().ReverseMap();
            CreateMap<ClothItem, ClothItemGetDto>().ReverseMap();

            CreateMap<ClothImage, ClothImageCreateDto>().ReverseMap();
            CreateMap<ClothImage, ClothImageGetDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();

            CreateMap<Review, ReviewCreateDto>().ReverseMap();
            CreateMap<Review, ReviewUpdateDto>().ReverseMap();
            CreateMap<Review, ReviewGetDto>().ReverseMap();

            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderGetDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();
        }
    }
}
