using AutoMapper;
using Clothes_Web_API_App.Dtos.OrderDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseApi<OrderGetDto>>> CreateOrder(OrderCreateDto entity)
        {
            var order = _mapper.Map<Order>(entity);

            var response = await _orderRepository.CreateOrder(order);

            if (!response.Success)
                return BadRequest(ResponseApi<OrderGetDto>.CopyResponse(response, _mapper));

            return Ok(ResponseApi<OrderGetDto>.CopyResponse(response, _mapper));
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<bool>>> DeleteOrder(int orderId)
        {
            var entity = await _orderRepository.FindFirstByCondition(e => e.Id == orderId);

            if (entity == null)
                return BadRequest(ResponseApi<bool>.GenerateErrorMessage("Order is not found"));

            await _orderRepository.Delete(entity);

            return Ok(ResponseApi<bool>.GenerateSuccessMessage("Order is deleted", true));
        }
    }
}
