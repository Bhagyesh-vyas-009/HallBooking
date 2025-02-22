using FluentValidation;

namespace HallBookingAPI.Models
{
    public class ResourceDetailModel
    {
        public int ResourceID { get; set; }
        public string ResourceType { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public string? CityName { get; set; }
        public string? StateName { get; set; }

        public string? CountryName { get; set; }
        public int PinCode { get; set; }
        public decimal Capacity { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public string OpenHours { get; set; }
        public string CloseHours { get; set; }
        public bool IsAvailable { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UserName { get; set; }

    }
    public class ResourceUploadModel
    {
        public int ResourceID { get; set; }
        public string ResourceType { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public int PinCode { get; set; }
        public decimal Capacity { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public string OpenHours { get; set; }
        public string CloseHours { get; set; }
        public bool IsAvailable { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserID { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class ResourceModelValidator : AbstractValidator<ResourceUploadModel>
    {
        public ResourceModelValidator()
        {
            RuleFor(resource => resource.ResourceType)
                .NotNull().NotEmpty().WithMessage("Resource Type is required.");

            RuleFor(resource => resource.Name)
                .NotNull().NotEmpty().WithMessage("Name is required.")
                .NotEqual("User").WithMessage("Name not be 'User'")
                .Length(3, 100).WithMessage("Name length must be 3 or more than 3 characters");

            RuleFor(resource => resource.Location)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");

            RuleFor(resource => resource.CountryID)
                .NotNull().NotEmpty().WithMessage("Country can not be empty.");
            //.GreaterThan(0).WithMessage("Country  can not be empty.");

            RuleFor(resource => resource.StateID)
                .NotNull().NotEmpty().WithMessage("State can not be empty.");
                //.GreaterThan(0).WithMessage("State can not be empty.");

            RuleFor(resource => resource.CityID)
                .NotNull().NotEmpty().WithMessage("City can not be empty.");
            //.GreaterThan(0).WithMessage("City Name can not be empty.");

            RuleFor(resource => resource.PinCode)
                .NotNull().NotEmpty().WithMessage("Pincode can not be empty.")
                .GreaterThan(0).WithMessage("PinCode must be a positive number.");

            RuleFor(resource => resource.Capacity)
                .NotNull().NotEmpty().WithMessage("Can not be empty")
                .GreaterThan(0).WithMessage("Must have a positive value.");

            RuleFor(resource => resource.Description)
                .NotNull().NotEmpty().WithMessage("Description can not be empty")
                .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

            RuleFor(resource => resource.PricePerDay)
                .NotNull().NotEmpty().WithMessage("Rent Per Day can not be empty")
                .GreaterThan(0).WithMessage("Rent Per Day must be a positive value.");

            RuleFor(resource => resource.OpenHours)
                .NotNull()
                .NotEmpty().WithMessage("Open Hours are required.");
            //.Matches(@"^\d{2}:\d{2}$").WithMessage("Open Hours must be in HH:mm format.");

            RuleFor(resource => resource.CloseHours)
                .NotEmpty().WithMessage("Close Hours are required.");
            //.Matches(@"^\d{2}:\d{2}$").WithMessage("Close Hours must be in HH:mm format.");

            RuleFor(resource => resource.Latitude)
                .NotEmpty().NotNull();

            RuleFor(resource => resource.Longitude)
                .NotEmpty().NotNull();
            //.InclusiveBetween(-180, 180).When(resource => resource.Longitude.HasValue).WithMessage("Longitude must be between -180 and 180.");

            RuleFor(resource => resource.UserID)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        }
    }
}
