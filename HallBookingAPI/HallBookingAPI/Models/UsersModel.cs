using FluentValidation;

namespace HallBookingAPI.Models
{
    public class UsersModel
    {

        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        //public string? Address { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserUpdateModel
    {

        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
        //public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class ChangePasswordModel
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; }
    }
    public class UserModelValidator : AbstractValidator<UsersModel>
    {
        public UserModelValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().NotNull().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotEqual("example@gmai.com").WithMessage("Email can not be example@gmail.com")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().NotNull().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().NotNull().WithMessage("Phone Number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            //RuleFor(user => user.Address)
            //    .NotNull().NotEmpty()
            //    .MaximumLength(255).WithMessage("Address must not exceed 255 characters.");

            RuleFor(user => user.Role)
                .NotEmpty().NotNull().WithMessage("Role is required.")
                 .Must(role => new[] { "Owner", "Admin", "User" }.Contains(role))
                .WithMessage("Invalid Role value.");
        }
    }

    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotEqual("example@gmai.com").WithMessage("Email can not be example@gmail.com")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().NotNull().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");
            RuleFor(user => user.Role)
                .NotEmpty().NotNull().WithMessage("Role is required.");
        }
    }

}
