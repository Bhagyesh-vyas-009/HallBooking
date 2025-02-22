﻿using FluentValidation;
using WebAPiDemo.Models;

namespace WebAPiDemo.Validators
{
    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            //RuleFor(c => c.CountryID).NotEmpty().NotNull().GreaterThan(0).WithMessage("Country ID must be a positive number");
            RuleFor(c => c.CountryName).NotEmpty().NotNull().WithMessage("Country Name must not be blank");
            RuleFor(c => c.CountryCode).NotEmpty().NotNull().Length(2).WithMessage("Country Code must be contain 2 character");
        }
    }
}
