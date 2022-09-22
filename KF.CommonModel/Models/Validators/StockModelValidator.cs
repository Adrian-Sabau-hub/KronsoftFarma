using FluentValidation;

namespace KF.CommonModel.Models.Validators
{
    public class StockModelValidator : BaseValidator<StockModel>
    {
        public StockModelValidator()
        {
            RuleFor(s => s.StockId).NotNull()
                .WithMessage("StockId must not be empty!");

            RuleFor(s => s.Quantity).NotEmpty()
                .WithMessage("Quantity must not be empty!")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be greater than or equal to 0!");

        }
    }
}
