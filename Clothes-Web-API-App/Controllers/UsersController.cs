using AutoMapper;
using Clothes_Web_API_App.Dtos.UserDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseApi<UserGetDto>>> Register([FromForm]UserCreateDto entity)
        {
            var user = _mapper.Map<User>(entity);
            var response = await _userRepository.Create(user, entity.Password, entity.ProfilePicturePath);

            if (!response.Success)
                return BadRequest(response);

            return Ok(ResponseApi<UserGetDto>.CopyResponse(response, _mapper));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseApi<string>>> Login(UserLoginDto entity)
        {
            var response = await _userRepository.Login(entity.Email, entity.Passord);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseApi<string>>> RefreshToken()
        {
            var response = await _userRepository.RefreshToken();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserGetDto>>> EditUser([FromForm]UserUpdateDto entity)
        {
            var user = _mapper.Map<User>(entity);
            var response = await _userRepository.Update(user, entity.Password, entity.ProfilePicturePath);

            if (!response.Success)
                return BadRequest(response);

            return Ok(ResponseApi<UserGetDto>.CopyResponse(response, _mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseApi<UserGetDto>>> GetAuthorizedUser()
        {
            var response = await _userRepository.GetAuthorizedUser();

            if (!response.Success)
                return BadRequest(response);

            return Ok(ResponseApi<UserGetDto>.CopyResponse(response, _mapper));
        }
    }
}
