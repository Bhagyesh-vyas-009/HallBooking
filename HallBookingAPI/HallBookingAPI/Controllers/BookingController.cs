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
    public class BookingController : ControllerBase
    {
        #region Configuration
        private readonly BookingRepository _bookingRepository;
        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        #endregion

        #region GetAllBookings
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            return Ok(bookings);
        }
        #endregion

        #region GetAllBookingByUserID
        [HttpGet("BookingByUser/{UserID}")]
        public IActionResult GetAllBookingByUserID(int UserID)
        {
            var bookings = _bookingRepository.GetAllBookingByUserID(UserID);
            return Ok(bookings);
        }
        #endregion

        #region GetAllBookingByOwnerID
        [HttpGet("BookingByOwner/{OwnerID}")]
        [Authorize(Roles = "Owner")]
        public IActionResult GetAllBookingByOwnerID(int OwnerID)
        {
            var bookings = _bookingRepository.GetAllBookingByOwnerID(OwnerID);
            return Ok(bookings);
        }
        #endregion

        #region GetBookingDetail
        [HttpGet("BookingDetail/{BookingID}")]
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult GetBookingDetail(int BookingID)
        {
            UserBookingModel booking = _bookingRepository.GetBookingDetail(BookingID);
            return Ok(booking);
        }
        #endregion

        #region GetAllBookingByResourceID
        [HttpGet("BookingByResource/{ResourceID}")]
        [Authorize(Roles = "Owner")]
        public IActionResult GetAllBookingByResourceID(int ResourceID)
        {
            var bookings = _bookingRepository.GetAllBookingByResourceID(ResourceID);
            return Ok(bookings);
        }
        #endregion

        #region GetBookingByID
        [HttpGet("GetBy/{BookingID}")]
        public IActionResult GetBookingByID(int BookingID)
        {
            BookingModel booking = _bookingRepository.SelectBookingByPK(BookingID);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }
        #endregion

        #region CancelBooking
        [HttpPut("Cancel/{BookingID}")]
        [Authorize(Roles = "Admin,Owner")]
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
        #endregion

        #region CreateBooking
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingModel booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingId = await _bookingRepository.CreateBookingAsync(booking);

            return Ok(new { BookingId = bookingId });
        }
        #endregion

        #region BookingUpdate
        [HttpPut("Update/{BookingID}")]
        [Authorize(Roles = "Admin,Owner")]
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
        #endregion

        #region GetBookedDateRanges
        [HttpGet("bookedDateRanges/{ResourceID}")]
        public async Task<IActionResult> GetBookedDateRanges(int ResourceID)
        {
            try
            {
                var bookedDateRanges = await _bookingRepository.GetBookedDateRanges(ResourceID);

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
        #endregion


    }
}
