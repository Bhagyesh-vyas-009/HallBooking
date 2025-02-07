using FluentValidation;
using WebAPiDemo.Models;

namespace WebAPiDemo.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            //RuleFor(ci => ci.CityID).NotEmpty().NotNull().GreaterThan(0).WithMessage("City ID must be a positive number");
            RuleFor(ci => ci.CityName).NotNull().NotEmpty().WithMessage("City Name must not be blank");
            RuleFor(ci => ci.CityCode).NotEmpty().Length(2).WithMessage("City Code must contain 2 character");
            RuleFor(ci => ci.StateID).NotEmpty().NotNull().GreaterThan(0).WithMessage("State Name is required");
            RuleFor(ci => ci.CountryID).NotEmpty().NotNull().GreaterThan(0).WithMessage("Country Name is required");
        }
    }
}
