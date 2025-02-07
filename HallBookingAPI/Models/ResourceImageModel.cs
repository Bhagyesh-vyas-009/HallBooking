<<<<<<< HEAD
﻿namespace HallBookingAPI.Models
=======
﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HallBookingAPI.Models
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
{
    public class ResourceImageModel
    {
        public int ImageID { get; set; }
        public int ResourceID { get; set; }
<<<<<<< HEAD
=======
        public string? ResourceName { get; set; }
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
<<<<<<< HEAD
=======

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
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
}
