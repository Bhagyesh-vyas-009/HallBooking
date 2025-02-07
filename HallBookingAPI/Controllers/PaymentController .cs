using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            return Ok(payments);
        }



<<<<<<< HEAD
        [HttpGet("{PaymentID}")]
=======
        [HttpGet("GetBy/{PaymentID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult GetPaymentByID(int PaymentID)
        {
            PaymentModel payment = _paymentRepository.SelectPaymentByPK(PaymentID);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

<<<<<<< HEAD
        [HttpDelete("{PaymentID}")]
=======
        [HttpGet("Booking/{BookingID}")]
        public IActionResult GetPaymentByBookingID(int BookingID)
        {
            var payments = _paymentRepository.GetAllPaymentByBookingID(BookingID);
            if (payments == null)
                return NotFound();
            return Ok(payments);
        }

        [HttpGet("User/{UserID}")]
        public IActionResult GetPaymentByUserID(int UserID)
        {
            var payments = _paymentRepository.GetAllPaymentByUserID(UserID);
            if (payments == null)
                return NotFound();
            return Ok(payments);
        }

        [HttpDelete("Delete/{PaymentID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult DeletePayment(int PaymentID)
        {
            var isDeleted = _paymentRepository.DeletePayment(PaymentID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

<<<<<<< HEAD
        [HttpPost]
=======
        [HttpPost("Add")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult PaymentInsert([FromBody] PaymentModel payment)
        {
            var isInserted = _paymentRepository.InsertPayment(payment);
            if (payment == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Payment Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Payment");
        }

<<<<<<< HEAD
        [HttpPut("{PaymentID}")]
=======
        [HttpPut("Update/{PaymentID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult PaymentUpdate(int PaymentID, [FromBody] PaymentModel payment)
        {
            var isUpdated = _paymentRepository.UpdatePayment(payment);
            if (payment == null || PaymentID != payment.PaymentID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Payment Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Payment");
        }
    }
}
