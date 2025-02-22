using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HallBookingAPI.Models
{
    public class ResourceImageModel
    {
        public int ImageID { get; set; }
        public int ResourceID { get; set; }
        public string? ResourceName { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ResourceImageModelValidator : AbstractValidator<ResourceImageModel>
    {
        public ResourceImageModelValidator()
        {
            RuleFor(image => image.ResourceID)
                .NotEmpty().NotNull()
                .GreaterThan(0).WithMessage("Resource ID must be greater than 0.");

            RuleFor(image => image.ImageURL)
                .NotEmpty().NotNull().WithMessage("Image URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Image URL must be a valid URL.");

        }
    }
}
