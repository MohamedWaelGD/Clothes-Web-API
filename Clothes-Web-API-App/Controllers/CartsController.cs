using AutoMapper;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.CartRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpPost("{clothId}")]
        [Authorize]
        public async Task<ActionResult<ResponseApi<Cart>>> AddClothToCart(int clothId)
        {
            var response = await _cartRepository.AddClothToUserCart(clothId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{clothId}")]
        [Authorize]
        public async Task<ActionResult<ResponseApi<Cart>>> RemoveClothFromCart(int clothId)
        {
            var response = await _cartRepository.RemoveClothToUserCart(clothId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseApi<Cart>>> ClearUserCart()
        {
            var response = await _cartRepository.ClearUserCart();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseApi<Cart>>> GetUserCart()
        {
            var response = await _cartRepository.GetUserCart();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
