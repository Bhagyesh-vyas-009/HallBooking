using FluentValidation;

namespace HallBookingAPI.Models
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public int ResourceID { get; set; }
        public string? ResourceName { get; set; }
        public Decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ReviewModelValidator : AbstractValidator<ReviewModel>
    {
        public ReviewModelValidator()
        {
            RuleFor(review => review.UserID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(review => review.ResourceID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("Resource ID must be greater than 0.");

            RuleFor(review => review.Rating)
                .NotEmpty().NotNull()
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(review => review.ReviewText)
                .NotEmpty().NotNull()
                .WithMessage("Review Text is required.")
                .MaximumLength(500).WithMessage("Review Text must not exceed 500 characters.");

        }
    }
}
