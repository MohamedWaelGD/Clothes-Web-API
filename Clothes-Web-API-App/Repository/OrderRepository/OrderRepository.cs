using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.CartRepository;
using Clothes_Web_API_App.Repository.ClothItemRepository;
using Clothes_Web_API_App.Repository.ClothRepository;

namespace Clothes_Web_API_App.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICartRepository _cartRepository;
        private readonly IClothItemRepository _clothItemRepository;

        public OrderRepository(
            AppDbContext appDbContext,
            ICartRepository cartRepository,
            IClothItemRepository clothItemRepository) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _cartRepository = cartRepository;
            _clothItemRepository = clothItemRepository;
        }

        public async Task<ResponseApi<Order>> CreateOrder(Order order)
        {
            var responseCart = await _cartRepository.GetUserCart();

            if (!responseCart.Success)
                return ResponseApi<Order>.GenerateErrorMessage(responseCart.Message);

            var carts = responseCart.Data;

            if (carts.Cloths.Count == 0)
                return ResponseApi<Order>.GenerateErrorMessage("There are no items in the cart");

            await _appDbContext.Orders.AddAsync(order);

            foreach (var item in carts.Cloths)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ClothItemId = item.ClothId,
                    Quantity = item.Quantity
                };

                var clothItem = await _clothItemRepository.GetClothItemById(item.ClothId);

                if (clothItem == null)
                    return ResponseApi<Order>.GenerateErrorMessage("Cloth Item not found");

                if (clothItem.Quantity - item.Quantity < 0)
                    return ResponseApi<Order>.GenerateErrorMessage("There are no enough resources");

                clothItem.Quantity -= item.Quantity;

                await _appDbContext.OrderItems.AddAsync(orderItem);
                _appDbContext.ClothItems.Update(clothItem);
            }

            await _appDbContext.SaveChangesAsync();
            await _cartRepository.ClearUserCart();

            return ResponseApi<Order>.GenerateSuccessMessage("Order has been successfuly assigned", order);
        }
    }
}
