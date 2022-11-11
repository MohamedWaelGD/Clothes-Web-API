using AutoMapper;
using Clothes_Web_API_App.Dtos.CategoryDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.CategoryRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi<ICollection<CategoryGetDto>>>> GetCategories()
        {
            var categories = _categoryRepository.FindAll();
            var categoriesGetDto = _mapper.Map<ICollection<CategoryGetDto>>(categories);
            var result = ResponseApi<ICollection<CategoryGetDto>>.GenerateSuccessMessage(string.Empty, categoriesGetDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<CategoryGetDto>>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return BadRequest(ResponseApi<CategoryGetDto>.GenerateErrorMessage("Category not found."));
            }

            var categoryGetDto = _mapper.Map<CategoryGetDto>(category);
            var result = ResponseApi<CategoryGetDto>.GenerateSuccessMessage(string.Empty, categoryGetDto);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<CategoryGetDto>>> CreateCategory(CategoryCreateDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            await _categoryRepository.Create(category);
            var clothGetDto = _mapper.Map<CategoryGetDto>(category);
            var result = ResponseApi<CategoryGetDto>.GenerateSuccessMessage("Category successfully created", clothGetDto);
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<CategoryGetDto>>> UpdateCategory(CategoryUpdateDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            await _categoryRepository.Update(category);
            var categoryGetDto = _mapper.Map<CategoryGetDto>(category);
            var result = ResponseApi<CategoryGetDto>.GenerateSuccessMessage("Category successfully updated", categoryGetDto);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<ICollection<CategoryGetDto>>>> DeleteCloth(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return BadRequest(ResponseApi<ICollection<CategoryGetDto>>.GenerateErrorMessage("Category not found"));
            }

            await _categoryRepository.Delete(category);

            var categories = _categoryRepository.FindAll();
            var categoriesGetDto = _mapper.Map<ICollection<CategoryGetDto>>(categories);
            var result = ResponseApi<ICollection<CategoryGetDto>>.GenerateSuccessMessage("Category successfully deleted", categoriesGetDto);
            return Ok(result);
        }
    }
}
