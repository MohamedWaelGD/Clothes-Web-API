using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.CartRepository
{
    public interface ICartRepository
    {
        Task<ResponseApi<Cart>> AddClothToUserCart(int clothId);
        Task<ResponseApi<Cart>> RemoveClothToUserCart(int clothId);
        Task<ResponseApi<Cart>> GetUserCart();
        Task<ResponseApi<bool>> ClearUserCart();
        Task<ResponseApi<Cart>> UpdateQuantityToItemInCart(int clothId, int quantity);
    }
}
