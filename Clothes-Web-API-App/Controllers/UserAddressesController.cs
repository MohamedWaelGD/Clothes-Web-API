using AutoMapper;
using Clothes_Web_API_App.Dtos.UserDto.UserAddressDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.UserRepository.UserAddressRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IMapper _mapper;

        public UserAddressesController(IUserAddressRepository userAddressRepository, IMapper mapper)
        {
            _userAddressRepository = userAddressRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<UserAddressGetDto>>> GetUserAddress(int id)
        {
            var data = _userAddressRepository.FindFirstByCondition(e => e.Id == id);

            if (data == null)
                return BadRequest(ResponseApi<UserAddressGetDto>.GenerateErrorMessage("Address not found"));

            var dataDto = _mapper.Map<UserAddressGetDto>(data);
            return Ok(ResponseApi<UserAddressGetDto>.GenerateSuccessMessage("Address found", dataDto));
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseApi<ICollection<UserAddressGetDto>>>> GetAllUserAddresses(int id)
        {
            var data = _userAddressRepository.FindByCondition(e => e.Id == id);

            if (data == null)
                return BadRequest(ResponseApi<ICollection<UserAddressGetDto>>.GenerateErrorMessage("Address not found"));

            var dataDto = _mapper.Map<ICollection<UserAddressGetDto>>(data);
            return Ok(ResponseApi<ICollection<UserAddressGetDto>>.GenerateSuccessMessage("Address found", dataDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserAddressGetDto>>> CreateUserAddress(UserAddressCreateDto entity)
        {
            var userAddress = _mapper.Map<UserAddress>(entity);

            await _userAddressRepository.Create(userAddress);

            var userAddressDto = _mapper.Map<UserAddressGetDto>(userAddress);
            return Ok(ResponseApi<UserAddressGetDto>.GenerateSuccessMessage("Address created.", userAddressDto));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserAddressGetDto>>> EditUserAddress(UserAddressUpdateDto entity)
        {
            var userAddress = _mapper.Map<UserAddress>(entity);

            await _userAddressRepository.Update(userAddress);

            var userAddressDto = _mapper.Map<UserAddressGetDto>(userAddress);
            return Ok(ResponseApi<UserAddressGetDto>.GenerateSuccessMessage("Address created.", userAddressDto));
        }
    }
}
