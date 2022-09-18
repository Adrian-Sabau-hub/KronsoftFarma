using FluentValidation;
using System.Linq;

namespace KF.WPF.Client.Core.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }

        public PropertyValidationResult ValidateProperty(T instanceToValidate, string propertyName)
        {
            var validationResult = Validate(instanceToValidate);

            return new PropertyValidationResult()
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Where(error => error.PropertyName == propertyName).ToList()
            };
        }
    }
}
