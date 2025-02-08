using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookingController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;
        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            return Ok(bookings);
        }

        [HttpGet("BookingByUser/{UserID}")]
        public IActionResult GetAllBookingByUserID(int UserID)
        {
            var bookings = _bookingRepository.GetAllBookingByUserID(UserID);
            return Ok(bookings);
        }

        [HttpGet("BookingDetail/{BookingID}")]
        public IActionResult GetBookingDetail(int BookingID)
        {
            UserBookingModel booking = _bookingRepository.GetBookingDetail(BookingID);
            return Ok(booking);
        }

        [HttpGet("BookingByResource/{ResourceID}")]
        public IActionResult GetAllBookingByResourceID(int ResourceID)
        {
            var bookings = _bookingRepository.GetAllBookingByResourceID(ResourceID);
            return Ok(bookings);
        }

        [HttpGet("GetBy/{BookingID}")]
        public IActionResult GetBookingByID(int BookingID)
        {
            BookingModel booking = _bookingRepository.SelectBookingByPK(BookingID);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        //[HttpDelete("Cancel/{BookingID}")]
        //public IActionResult DeleteBooking(int BookingID)
        //{
        //    var isDeleted = _bookingRepository.DeleteBooking(BookingID);
        //    if (!isDeleted)
        //        return NotFound();
        //    return NoContent();
        //}

        [HttpPost("Add")]
        public IActionResult BookingInsert([FromBody] BookingModel booking)
        {
            var isInserted = _bookingRepository.InsertBooking(booking);
            if (booking == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Booking Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Booking");
        }

        [HttpPut("Cancel/{BookingID}")]
        public IActionResult CancelBooking(int BookingID,[FromBody]string status)
        {
            var booking = _bookingRepository.SelectBookingByPK(BookingID);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingRepository.CancelBooking(BookingID, status);
            return Ok(booking);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingModel booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingId = await _bookingRepository.CreateBookingAsync(booking);

            return Ok(new { BookingId = bookingId });
        }

        [HttpPut("Update/{BookingID}")]
        public IActionResult BookingUpdate(int BookingID, [FromBody] BookingModel booking)
        {
            var isUpdated = _bookingRepository.UpdateBooking(booking);
            if (booking == null || BookingID != booking.BookingID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Booking Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Booking");
        }

        [HttpGet("bookedDateRanges/{ResourceID}")]
        public async Task<IActionResult> GetBookedDateRanges(int ResourceID)
        {
            try
            {
                var bookedDateRanges = await _bookingRepository.GetBookedDateRangesAsync(ResourceID);

                if (bookedDateRanges == null || bookedDateRanges.Count == 0)
                {
                    return NotFound("No booked date ranges found.");
                }

                Console.WriteLine(bookedDateRanges);
                return Ok(bookedDateRanges);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
