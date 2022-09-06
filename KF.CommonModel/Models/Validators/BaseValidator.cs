using FluentValidation;
using KF.Common.Model.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.CommonModel.Models.Validators
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
