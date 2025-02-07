using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly ReviewRepository _reviewRepository;
        public ReviewController(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _reviewRepository.GetAllReviews();
            return Ok(reviews);
        }

<<<<<<< HEAD

=======
        [HttpGet("Resource/{ResourceID}")]
        public IActionResult GetAllReviewByResource(int ResourceID)
        {
            var reviews = _reviewRepository.GetAllReviewByResourceID(ResourceID);
            return Ok(reviews);
        }

        [HttpGet("User/{UserID}")]
        public IActionResult GetAllReviewByUser(int UserID)
        {
            var reviews = _reviewRepository.GetAllReviewByUserID(UserID);
            return Ok(reviews);
        }
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)

        [HttpGet("{ReviewID}")]
        public IActionResult GetReviewByID(int ReviewID)
        {
            ReviewModel review = _reviewRepository.SelectReviewByPK(ReviewID);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

<<<<<<< HEAD
        [HttpDelete("{ReviewID}")]
=======
        [HttpDelete("Delete/{ReviewID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult DeleteReview(int ReviewID)
        {
            var isDeleted = _reviewRepository.DeleteReview(ReviewID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

<<<<<<< HEAD
        [HttpPost]
=======
        [HttpPost("Add")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult ReviewInsert([FromBody] ReviewModel review)
        {
            var isInserted = _reviewRepository.InsertReview(review);
            if (review == null)
                return BadRequest();
            if (isInserted)
<<<<<<< HEAD
                return Ok(new { Message = "Review Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Review");
        }

        [HttpPut("{ReviewID}")]
=======
                return Ok(new { Message = "Review Added  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Review");
        }

        [HttpPut("Update/{ReviewID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
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

    }
}
