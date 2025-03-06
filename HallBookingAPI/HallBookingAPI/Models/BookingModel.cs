using FluentValidation;

namespace HallBookingAPI.Models
{
    public class BookingModel
    {

        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int ResourceID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserBookingModel
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string Location { get; set; }
        public int PinCode { get; set; }
        public int PricePerDay { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class BookedDateRangeDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class HallBookingRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string HallName { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string BookingStatus { get; set; }
    }


    public class BookingModelValidator : AbstractValidator<BookingModel>
    {
        public BookingModelValidator()
        {
            RuleFor(booking => booking.UserID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(booking => booking.ResourceID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("Resource ID must be greater than 0.");

            //RuleFor(booking => booking.BookingDate)
            //    .NotEmpty().WithMessage("Booking Date is required.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Booking Date cannot be in the future.");

            RuleFor(booking => booking.FromDate)
                .NotEmpty().NotNull().WithMessage("From Date is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("From Date cannot be in the past.");

            RuleFor(booking => booking.ToDate)
                .NotEmpty().NotNull().WithMessage("To Date is required.")
                .GreaterThanOrEqualTo(booking => booking.FromDate).WithMessage("To Date must be after From Date.");

            RuleFor(booking => booking.TotalPrice)
                .NotEmpty().NotNull()
                .GreaterThanOrEqualTo(0).WithMessage("Total Price must be 0 or a positive value.");

            RuleFor(booking => booking.Status)
                .NotEmpty().NotNull().WithMessage("Status is required.")
                .Must(status => new[] { "Pending", "Confirmed", "Cancelled", "Completed" }.Contains(status))
                .WithMessage("Status must be one of the following: Pending, Confirmed, Cancelled, Completed.");
        }
    }
}
