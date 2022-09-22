using FluentValidation;

namespace KF.WPF.Client.Core.Models.Validators
{
    public class ProductModelValidator : BaseValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(s => s.ProductId).NotNull()
                .WithMessage("ProductId must not be empty!");

            RuleFor(s => s.Name).NotEmpty()
                .WithMessage("Name must not be empty!")
                .MinimumLength(3)
                .WithMessage("Name minimum length of 3!")
                .MaximumLength(50)
                .WithMessage("Name maximum length of 50!");

            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("Description must not be empty!")
                .MinimumLength(3)
                .WithMessage("Description MinimumLength = 3!");

            RuleFor(s => s.Price).NotEmpty()
                .WithMessage("Price must not be empty!")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0!");

            RuleFor(s => s.Producer).NotEmpty()
                .WithMessage("Producer must not be empty!")
                .MinimumLength(3)
                .WithMessage("Producer minimum length of 3!")
                .MaximumLength(50)
                .WithMessage("Producer maximum length of 50!");

        }
    }
}
