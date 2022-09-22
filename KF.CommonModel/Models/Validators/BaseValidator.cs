using FluentValidation;
using KF.Common.Model.Models.Validators;

namespace KF.CommonModel.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        #region ctor

        public BaseValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }

        #endregion

        #region Methods

        public PropertyValidationResult ValidateProperty(T instanceToValidate, string propertyName)
        {
            try
            {
                var validationResult = Validate(instanceToValidate);

                return new PropertyValidationResult()
                {
                    IsValid = validationResult.IsValid,
                    Errors = validationResult.Errors.Where(error => error.PropertyName == propertyName).ToList()
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
