using FluentValidation.Results;
using System.Collections.Generic;

namespace WPF.Client.Models.Validators
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
