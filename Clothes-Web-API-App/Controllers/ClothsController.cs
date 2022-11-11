using AutoMapper;
using Clothes_Web_API_App.Dtos.ClothDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Paging;
using Clothes_Web_API_App.Repository.ClothRepository;
using Clothes_Web_API_App.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothsController : ControllerBase
    {
        private readonly IClothRepository _clothRepository;
        private readonly IMapper _mapper;

        public ClothsController(IClothRepository clothRepository, IMapper mapper)
        {
            _clothRepository = clothRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi<PagedList<ClothGetDto>>>> GetCloths([FromQuery]ClothSpec spec, [FromQuery]PagingParameters pagingParameters)
        {
            var cloths = await _clothRepository.GetCloths(spec, pagingParameters);
            var clothsGetDto = PagedList<ClothGetDto>.CopyPagedList(cloths, _mapper);
            var result = ResponseApi<PagedList<ClothGetDto>>.GenerateSuccessMessage(string.Empty, clothsGetDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<ClothGetDto>>> GetCloth(int id)
        {
            var cloth = await _clothRepository.GetClothById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<ClothGetDto>.GenerateErrorMessage("Cloth not found."));
            }

            var clothGetDto = _mapper.Map<ClothGetDto>(cloth);
            var result = ResponseApi<ClothGetDto>.GenerateSuccessMessage(string.Empty, clothGetDto);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ClothGetDto>>> CreateCloth(ClothCreateDto entity)
        {
            var cloth = _mapper.Map<Cloth>(entity);
            await _clothRepository.Create(cloth);
            var clothGetDto = _mapper.Map<ClothGetDto>(cloth);
            var result = ResponseApi<ClothGetDto>.GenerateSuccessMessage("Cloth successfully created", clothGetDto);
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ClothGetDto>>> UpdateCloth(ClothUpdateDto entity)
        {
            var cloth = _mapper.Map<Cloth>(entity);
            await _clothRepository.Update(cloth);
            var clothGetDto = _mapper.Map<ClothGetDto>(cloth);
            var result = ResponseApi<ClothGetDto>.GenerateSuccessMessage("Cloth successfully updated", clothGetDto);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<PagedList<ClothGetDto>>>> DeleteCloth(int id)
        {
            var cloth = await _clothRepository.GetClothById(id);

            if (cloth == null)
            {
                return BadRequest(ResponseApi<PagedList<ClothGetDto>>.GenerateErrorMessage("Cloth not found"));
            }

            await _clothRepository.Delete(cloth);

            var cloths = await _clothRepository.GetCloths(new ClothSpec(), new PagingParameters());
            var clothsGetDto = PagedList<ClothGetDto>.CopyPagedList(cloths, _mapper);
            var result = ResponseApi<PagedList<ClothGetDto>>.GenerateSuccessMessage("Cloth successfully deleted", clothsGetDto);
            return Ok(result);
        }
    }
}
