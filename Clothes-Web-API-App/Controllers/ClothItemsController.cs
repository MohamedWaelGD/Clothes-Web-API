using AutoMapper;
using Clothes_Web_API_App.Dtos.ClothItemDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Clothes_Web_API_App.Repository.ClothItemRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothItemsController : ControllerBase
    {
        private readonly IClothItemRepository _clothItemRepository;
        private readonly IMapper _mapper;

        public ClothItemsController(IClothItemRepository clothItemRepository, IMapper mapper)
        {
            _clothItemRepository = clothItemRepository;
            _mapper = mapper;
        }

        [HttpGet("cloth/{clothId}")]
        public async Task<ActionResult<ResponseApi<ICollection<ClothItemGetDto>>>> GetClothItems(int clothId)
        {
            var cloths = await _clothItemRepository.GetClothItems(clothId);
            var clothsGetDto = _mapper.Map<ICollection<ClothItemGetDto>>(cloths);
            var result = ResponseApi<ICollection<ClothItemGetDto>>.GenerateSuccessMessage(string.Empty, clothsGetDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<ClothItemGetDto>>> GetCloth(int id)
        {
            var cloth = await _clothItemRepository.GetClothItemById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<ClothItemGetDto>.GenerateErrorMessage("Cloth not found."));
            }

            var clothGetDto = _mapper.Map<ClothItemGetDto>(cloth);
            var result = ResponseApi<ClothItemGetDto>.GenerateSuccessMessage(string.Empty, clothGetDto);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ClothItemGetDto>>> CreateCloth(ClothItemCreateDto entity)
        {
            var cloth = _mapper.Map<ClothItem>(entity);
            await _clothItemRepository.Create(cloth);
            var clothGetDto = _mapper.Map<ClothItemGetDto>(cloth);
            var result = ResponseApi<ClothItemGetDto>.GenerateSuccessMessage("Cloth successfully created", clothGetDto);
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ClothItemGetDto>>> UpdateCloth(ClothItemUpdateDto entity)
        {
            var cloth = _mapper.Map<ClothItem>(entity);
            await _clothItemRepository.Update(cloth);
            var clothGetDto = _mapper.Map<ClothItemGetDto>(cloth);
            var result = ResponseApi<ClothItemGetDto>.GenerateSuccessMessage("Cloth successfully updated", clothGetDto);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ICollection<ClothItemGetDto>>>> DeleteCloth(int id)
        {
            var cloth = await _clothItemRepository.GetClothItemById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<ICollection<ClothItemGetDto>>.GenerateErrorMessage("Cloth not found"));
            }

            int clothId = cloth.ClothId;

            await _clothItemRepository.Delete(cloth);

            var cloths = await _clothItemRepository.GetClothItems(clothId);
            var clothsGetDto = _mapper.Map<ICollection<ClothItemGetDto>>(cloths);
            var result = ResponseApi<ICollection<ClothItemGetDto>>.GenerateSuccessMessage("Cloth successfully deleted", clothsGetDto);
            return Ok(result);
        }
    }
}
