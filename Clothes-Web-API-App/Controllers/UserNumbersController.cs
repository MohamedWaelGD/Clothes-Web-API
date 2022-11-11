using AutoMapper;
using Clothes_Web_API_App.Dtos.UserDto.UserNumberDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.UserRepository.UserNumberRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNumbersController : ControllerBase
    {
        private readonly IUserNumberRepository _userNumberRepository;
        private readonly IMapper _mapper;

        public UserNumbersController(IUserNumberRepository userNumberRepository, IMapper mapper)
        {
            _userNumberRepository = userNumberRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<UserNumberGetDto>>> GetUserNumber(int id)
        {
            var data = _userNumberRepository.FindFirstByCondition(e => e.Id == id);

            if (data == null)
                return BadRequest(ResponseApi<UserNumberGetDto>.GenerateErrorMessage("Number not found"));

            var dataDto = _mapper.Map<UserNumberGetDto>(data);
            return Ok(ResponseApi<UserNumberGetDto>.GenerateSuccessMessage("Number found", dataDto));
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseApi<ICollection<UserNumberGetDto>>>> GetAllUserNumbers(int id)
        {
            var data = _userNumberRepository.FindByCondition(e => e.Id == id);

            if (data == null)
                return BadRequest(ResponseApi<ICollection<UserNumberGetDto>>.GenerateErrorMessage("Number not found"));

            var dataDto = _mapper.Map<ICollection<UserNumberGetDto>>(data);
            return Ok(ResponseApi<ICollection<UserNumberGetDto>>.GenerateSuccessMessage("Number found", dataDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserNumberGetDto>>> CreateUserNumber(UserNumberCreateDto entity)
        {
            var userNumber = _mapper.Map<UserNumber>(entity);

            await _userNumberRepository.Create(userNumber);

            var userAddressDto = _mapper.Map<UserNumberGetDto>(userNumber);
            return Ok(ResponseApi<UserNumberGetDto>.GenerateSuccessMessage("Number created.", userAddressDto));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserNumberGetDto>>> EditUserNumber(UserNumberUpdateDto entity)
        {
            var userNumber = _mapper.Map<UserNumber>(entity);

            await _userNumberRepository.Update(userNumber);

            var userAddressDto = _mapper.Map<UserNumberGetDto>(userNumber);
            return Ok(ResponseApi<UserNumberGetDto>.GenerateSuccessMessage("Number created.", userAddressDto));
        }
    }
}
