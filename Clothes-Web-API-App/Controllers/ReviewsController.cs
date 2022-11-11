using AutoMapper;
using Clothes_Web_API_App.Dtos.ReviewDto;
using Clothes_Web_API_App.Models;
using Clothes_Web_API_App.Repository.ReviewRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_Web_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet("cloth/{clothId}")]
        public async Task<ActionResult<ResponseApi<ReviewGetDto>>> GetClothReviews(int clothId)
        {
            var response = await _reviewRepository.GetClothReviews(clothId);

            if (!response.Success)
                return BadRequest(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));

            return Ok(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi<ReviewGetDto>>> GetActiveUserReviews()
        {
            var response = await _reviewRepository.GetActiveUserReviews();

            if (!response.Success)
                return BadRequest(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));

            return Ok(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ResponseApi<ReviewGetDto>>> GetActiveUserReviews(int userId)
        {
            var response = await _reviewRepository.GetUserReviewsById(userId);

            if (!response.Success)
                return BadRequest(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));

            return Ok(ResponseApi<ReviewGetDto>.CopyResponse(response, _mapper));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseApi<ReviewGetDto>>> CreateReview(ReviewCreateDto entity)
        {
            var review = _mapper.Map<Review>(entity);

            await _reviewRepository.Create(review);

            var reviewDto = _mapper.Map<ReviewGetDto>(entity);

            return Ok(ResponseApi<ReviewGetDto>.GenerateSuccessMessage("Review created", reviewDto));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseApi<ReviewGetDto>>> UpdateReview(ReviewUpdateDto entity)
        {
            var review = _mapper.Map<Review>(entity);

            await _reviewRepository.Update(review);

            var reviewDto = _mapper.Map<ReviewGetDto>(entity);

            return Ok(ResponseApi<ReviewGetDto>.GenerateSuccessMessage("Review created", reviewDto));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseApi<bool>>> DeleteReview([FromQuery]int userId,[FromQuery] int clothId)
        {
            var response = await _reviewRepository.DeleteReview(userId, clothId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("user")]
        [Authorize]
        public async Task<ActionResult<ResponseApi<bool>>> DeleteReviewByActiveUser([FromQuery] int clothId)
        {
            var response = await _reviewRepository.DeleteReviewByActiveUser(clothId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
