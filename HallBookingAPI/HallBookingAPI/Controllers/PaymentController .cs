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
        #region Configuration
        private readonly PaymentRepository _paymentRepository;
        public PaymentController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        #endregion

        #region GetAllPayments
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            return Ok(payments);
        }
        #endregion

        #region GetPaymentByID
        [HttpGet("GetBy/{PaymentID}")]
        public IActionResult GetPaymentByID(int PaymentID)
        {
            PaymentModel payment = _paymentRepository.SelectPaymentByPK(PaymentID);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }
        #endregion

        #region GetPaymentByBookingID
        [HttpGet("Booking/{BookingID}")]
        public IActionResult GetPaymentByBookingID(int BookingID)
        {
            var payments = _paymentRepository.GetAllPaymentByBookingID(BookingID);
            if (payments == null)
                return NotFound();
            return Ok(payments);
        }
        #endregion

        #region GetPaymentByUserID
        [HttpGet("User/{UserID}")]
        public IActionResult GetPaymentByUserID(int UserID)
        {
            var payments = _paymentRepository.GetAllPaymentByUserID(UserID);
            if (payments == null)
                return NotFound();
            return Ok(payments);
        }
        #endregion


        #region  DeletePayment
        [HttpDelete("Delete/{PaymentID}")]
        public IActionResult DeletePayment(int PaymentID)
        {
            var isDeleted = _paymentRepository.DeletePayment(PaymentID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
        #endregion


        #region PaymentInsert
        [HttpPost("Add")]
        public IActionResult PaymentInsert([FromBody] PaymentModel payment)
        {
            var isInserted = _paymentRepository.InsertPayment(payment);
            if (payment == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Payment Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Payment");
        }
        #endregion


        #region PaymentUpdate
        [HttpPut("Update/{PaymentID}")]
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
        #endregion

    }
}
