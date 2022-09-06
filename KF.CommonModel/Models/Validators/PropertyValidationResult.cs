using FluentValidation.Results;

namespace KF.Common.Model.Models.Validators
{
    public class PropertyValidationResult
    {
        public PropertyValidationResult()
        {
            this.Errors = new List<ValidationFailure>();
        }

        public bool IsValid { get; set; }

        public List<ValidationFailure> Errors { get; set; }
    }
}
