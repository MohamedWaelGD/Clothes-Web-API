using Clothes_Web_API_App.Models;

namespace Clothes_Web_API_App.Repository.OrderRepository
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<ResponseApi<Order>> CreateOrder(Order order);
    }
}
