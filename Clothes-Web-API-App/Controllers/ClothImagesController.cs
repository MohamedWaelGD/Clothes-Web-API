using AutoMapper;
using Clothes_Web_API_App.Dtos.ClothImageDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Clothes_Web_API_App.Repository.ClothImageRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothImagesController : ControllerBase
    {
        private readonly IClothImageRepository _clothImagesRepository;
        private readonly IMapper _mapper;

        public ClothImagesController(IClothImageRepository clothImageRepository, IMapper mapper)
        {
            _clothImagesRepository = clothImageRepository;
            _mapper = mapper;
        }

        [HttpGet("cloth Item/{clothItemId}")]
        public async Task<ActionResult<ResponseApi<ICollection<ClothImageGetDto>>>> GetClothItems(int clothItemId)
        {
            var cloths = await _clothImagesRepository.GetClothImages(clothItemId);
            var clothsGetDto = _mapper.Map<ICollection<ClothImageGetDto>>(cloths);
            var result = ResponseApi<ICollection<ClothImageGetDto>>.GenerateSuccessMessage(string.Empty, clothsGetDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<ClothImageGetDto>>> GetCloth(int id)
        {
            var cloth = await _clothImagesRepository.GetClothImageById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<ClothImageGetDto>.GenerateErrorMessage("Cloth not found."));
            }

            var clothGetDto = _mapper.Map<ClothImageGetDto>(cloth);
            var result = ResponseApi<ClothImageGetDto>.GenerateSuccessMessage(string.Empty, clothGetDto);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ClothImageGetDto>>> CreateCloth([FromForm] ClothImageCreateDto entity)
        {
            var cloth = _mapper.Map<ClothImage>(entity);
            await _clothImagesRepository.CreateClothImage(cloth, entity.ImagePath);
            var clothGetDto = _mapper.Map<ClothImageGetDto>(cloth);
            var result = ResponseApi<ClothImageGetDto>.GenerateSuccessMessage("Cloth successfully created", clothGetDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<PagedList<ClothImageGetDto>>>> DeleteCloth(int id)
        {
            var cloth = await _clothImagesRepository.GetClothImageById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<PagedList<ClothImageGetDto>>.GenerateErrorMessage("Cloth not found"));
            }

            int clothItemId = cloth.ClothItemId;

            await _clothImagesRepository.Delete(cloth);

            var cloths = await _clothImagesRepository.GetClothImageById(clothItemId);
            var clothsGetDto = _mapper.Map<PagedList<ClothImageGetDto>>(cloths);
            var result = ResponseApi<PagedList<ClothImageGetDto>>.GenerateSuccessMessage("Cloth successfully deleted", clothsGetDto);
            return Ok(result);
        }
    }
}
