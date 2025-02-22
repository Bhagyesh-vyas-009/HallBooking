using FluentValidation;
using WebAPiDemo.Models;

namespace WebAPiDemo.Validators
{
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            //RuleFor(st => st.StateID).NotEmpty().NotNull().GreaterThan(0).WithMessage("State ID must be a positive number");
            RuleFor(x => x.StateName).NotNull().NotEmpty().WithMessage("State Name must not be blank");
            RuleFor(st => st.StateCode).NotEmpty().Length(2).WithMessage("State Code must contain  2 character");
            RuleFor(st => st.CountryID).NotEmpty().NotNull().GreaterThan(0).WithMessage("Country Name is required");
        }
    }
}
