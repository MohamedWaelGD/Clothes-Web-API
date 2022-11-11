using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.ClothItemRepository;
using Clothes_Web_API_App.Repository.ClothRepository;
using Clothes_Web_API_App.Repository.UserRepository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Clothes_Web_API_App.Repository.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;
        private readonly IUserRepository _userRepository;
        private readonly IClothItemRepository _clothItemRepository;

        public CartRepository(
            AppDbContext dbContext,
            IDistributedCache distributedCache,
            IUserRepository userRepository,
            IClothItemRepository clothItemRepository)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
            _userRepository = userRepository;
            _clothItemRepository = clothItemRepository;
        }

        public async Task<ResponseApi<Cart>> AddClothToUserCart(int clothId)
        {
            var response = await GetUserCart();
            if (!response.Success)
                return response;

            var cloth = await _clothItemRepository.GetClothItemById(clothId);
            if (cloth == null)
                return ResponseApi<Cart>.GenerateErrorMessage("Cloth Item not found.");

            var userResponse = await _userRepository.GetAuthorizedUser();
            var cart = response.Data;
            if (cart == null)
                cart = new Cart();

            var cartItem = new CartItem 
            { 
                ClothId = clothId,
                Quantity = 1 
            };

            cart.UserId = userResponse.Data.Id;
            cart.Cloths.Add(cartItem);

            await UpdateCartCashing(cart, userResponse.Data.Id);

            return ResponseApi<Cart>.GenerateSuccessMessage("User cart updated", cart);
        }

        public async Task<ResponseApi<bool>> ClearUserCart()
        {
            var userResponse = await _userRepository.GetAuthorizedUser();
            int userId = userResponse.Data.Id;
            await _distributedCache.RemoveAsync($"User_Cart_{userId}");
            return ResponseApi<bool>.GenerateSuccessMessage("Cart is cleared", true);
        }

        public async Task<ResponseApi<Cart>> GetUserCart()
        {
            var response = await _userRepository.GetAuthorizedUser();

            if (!response.Success)
                return ResponseApi<Cart>.GenerateErrorMessage("User not authorized");

            var jsonData = await _distributedCache.GetStringAsync($"User_Cart_{response.Data.Id}");

            if (string.IsNullOrEmpty(jsonData))
                return ResponseApi<Cart>.GenerateSuccessMessage("Cart is empty");

            var entity = JsonConvert.DeserializeObject<Cart>(jsonData);

            return ResponseApi<Cart>.GenerateSuccessMessage("User cart loaded", entity);
        }

        public async Task<ResponseApi<Cart>> RemoveClothToUserCart(int clothId)
        {
            var response = await GetUserCart();
            if (!response.Success)
                return response;

            var cloth = await _clothItemRepository.GetClothItemById(clothId);
            if (cloth == null)
                return ResponseApi<Cart>.GenerateErrorMessage("Cloth not found.");

            var userResponse = await _userRepository.GetAuthorizedUser();
            
            var cart = response.Data;

            if (cart == null)
                return ResponseApi<Cart>.GenerateErrorMessage("The cart is empty");

            cart.Cloths.RemoveWhere(e => e.ClothId == clothId);

            await UpdateCartCashing(cart, userResponse.Data.Id);

            return ResponseApi<Cart>.GenerateSuccessMessage("User cart updated", cart);
        }

        public async Task<ResponseApi<Cart>> UpdateQuantityToItemInCart(int clothId, int quantity)
        {
            var response = await GetUserCart();
            if (!response.Success)
                return response;

            var cloth = await _clothItemRepository.GetClothItemById(clothId);
            if (cloth == null)
                return ResponseApi<Cart>.GenerateErrorMessage("Cloth not found.");

            var userResponse = await _userRepository.GetAuthorizedUser();

            var cart = response.Data;

            if (cart == null)
                return ResponseApi<Cart>.GenerateErrorMessage("The cart is empty");

            var item = cart.Cloths.FirstOrDefault(e => e.ClothId == clothId);

            if (item == null)
                return ResponseApi<Cart>.GenerateErrorMessage("The cloth item not found in the cart");

            item.Quantity = Math.Clamp(item.Quantity, 1, int.MaxValue);

            await UpdateCartCashing(cart, userResponse.Data.Id);

            return ResponseApi<Cart>.GenerateSuccessMessage("User cart updated", cart);
        }

        private async Task UpdateCartCashing(Cart cart, int userId)
        {
            var distributeCashOptions = new DistributedCacheEntryOptions();
            distributeCashOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            distributeCashOptions.SlidingExpiration = null;

            var jsonData = JsonConvert.SerializeObject(cart);
            await _distributedCache.SetStringAsync($"User_Cart_{userId}", jsonData, distributeCashOptions);
        }
    }
}
