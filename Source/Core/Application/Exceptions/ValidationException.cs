using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();
            foreach (var validation in validationResult.Errors) ValidationErrors.Add(validation.ErrorMessage);
        }
    }
}