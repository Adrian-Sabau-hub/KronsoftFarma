using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.CommonModel.Models.Validators
{
    public class UserModelValidator : BaseValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(s => s.UserId).NotNull()
                .WithMessage("UserId must not be empty!");

            RuleFor(s => s.Username).NotEmpty()
                .WithMessage("Username must not be empty!")
                .MinimumLength(3)
                .WithMessage("Username minimum length of 3!")
                .MaximumLength(50)
                .WithMessage("Username maximum length of 50!");

            RuleFor(s => s.Password).NotEmpty()
                .WithMessage("Password must not be empty!")
                .MinimumLength(3)
                .WithMessage("Password minimum length of 3!")
                .MaximumLength(50)
                .WithMessage("Password maximum length of 50!");

            RuleFor(s => s.IsAdmin).NotEmpty()
                .WithMessage("IsAdmin must not be empty!");

        }
    }
}
