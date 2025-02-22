using FluentValidation;

namespace HallBookingAPI.Models
{
    public class PaymentModel
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? BookingDate { get; set; }
    }

    public class PaymentModelValidator : AbstractValidator<PaymentModel>
    {
        public PaymentModelValidator()
        {
            RuleFor(payment => payment.BookingID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("Booking ID must be greater than 0.");

            RuleFor(payment => payment.UserID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            //RuleFor(payment => payment.PaymentDate)
            //    .NotEmpty().WithMessage("Payment Date is required.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Payment Date cannot be in the future.");

            RuleFor(payment => payment.PaymentMethod)
                .NotEmpty().NotNull()
                .WithMessage("Payment Method is required.")
                .Must(method => new[] { "CreditCard", "DebitCard", "NetBanking", "UPI", }.Contains(method))
                .WithMessage("Payment Method must be one of the following: CreditCard, DebitCard, NetBanking, UPI");

            RuleFor(payment => payment.Amount)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(payment => payment.Status)
               .NotEmpty().NotNull().WithMessage("Status is required.")
               .Must(status => new[] { "Pending", "Confirmed", "Cancelled", "Success" }.Contains(status))
               .WithMessage("Status must be one of the following: Pending, Confirmed, Cancelled, Success.");
        }
    }

    public class UserPaymentModel
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FullName { get; set; }

        public string? ResourceName { get; set; }

    }
}
