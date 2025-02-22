using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        #region Configuration
        private readonly ReviewRepository _reviewRepository;
        public ReviewController(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        #endregion

        #region GetAllReviews
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _reviewRepository.GetAllReviews();
            return Ok(reviews);
        }
        #endregion

        #region GetAllReviewByResource
        [HttpGet("Resource/{ResourceID}")]
        public IActionResult GetAllReviewByResource(int ResourceID)
        {
            var reviews = _reviewRepository.GetAllReviewByResourceID(ResourceID);
            return Ok(reviews);
        }
        #endregion

        #region GetAllReviewByUser
        [HttpGet("User/{UserID}")]
        public IActionResult GetAllReviewByUser(int UserID)
        {
            var reviews = _reviewRepository.GetAllReviewByUserID(UserID);
            return Ok(reviews);
        }
        #endregion

        #region GetAllReviewByOwnerID
        [HttpGet("Owner/{OwnerID}")]
        public IActionResult GetAllReviewByOwnerID(int OwnerID)
        {
            var reviews = _reviewRepository.GetAllReviewByOwnerID(OwnerID);
            return Ok(reviews);
        }
        #endregion

        #region GetReviewByID
        [HttpGet("{ReviewID}")]
        public IActionResult GetReviewByID(int ReviewID)
        {
            ReviewModel review = _reviewRepository.SelectReviewByPK(ReviewID);
            if (review == null)
                return NotFound();
            return Ok(review);
        }
        #endregion

        #region DeleteReview
        [HttpDelete("Delete/{ReviewID}")]
        public IActionResult DeleteReview(int ReviewID)
        {
            var isDeleted = _reviewRepository.DeleteReview(ReviewID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
        #endregion

        #region ReviewInsert
        [HttpPost("Add")]
        public IActionResult ReviewInsert([FromBody] ReviewModel review)
        {
            var isInserted = _reviewRepository.InsertReview(review);
            if (review == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Review Added  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Review");
        }
        #endregion

        #region ReviewUpdate
        [HttpPut("Update/{ReviewID}")]
        public IActionResult ReviewUpdate(int ReviewID, [FromBody] ReviewModel review)
        {
            var isUpdated = _reviewRepository.UpdateReview(review);
            if (review == null || ReviewID!= review.ReviewID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Review Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Review");
        }
        #endregion

    }
}
