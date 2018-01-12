using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Telling.Core.Validation
{
    public class ValidateRequest : IValidateRequest
    {
        public ValidateResult Validate<TRequest, TValidator>(TRequest request, bool forceValidate = false)
            where TRequest : class
            where TValidator : class, IValidator, new()
        {
            ValidationResult results = new TValidator().Validate(request);

            return new ValidateResult
            {
                IsValid = results.IsValid,
                Errors = results.AsObservableDictionary(forceValidate)
            };
        }

        public ValidateResult Validate<TRequest, TValidator>(TRequest request, Dictionary<string, string> mappings, bool forceValidate = false)
            where TRequest : class
            where TValidator : class, IValidator, new()
        {
            ValidationResult results = new TValidator().Validate(request);

            return new ValidateResult
            {
                IsValid = results.IsValid,
                Errors = results.AsObservableDictionary(forceValidate, mappings)
            };
        }

        public ValidateResult ValidateProperty<TRequest, TValidator>(TRequest request, bool forceValidate = false, params string[] properties)
            where TRequest : class
            where TValidator : class, IValidator<TRequest>, new()
        {
            ValidationResult results = new TValidator().Validate(request, properties);

            return new ValidateResult
            {
                IsValid = results.IsValid,
                Errors = results.AsObservableDictionary(forceValidate)
            };
        }
    }
}
