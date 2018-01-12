using FluentValidation.Results;
using Telling.Core.Extensions;
using Telling.Core.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Telling.Core.Validation
{
    public static class ValidationResultExtension
    {
        public static ObservableDictionary<string, ValidationError> AsObservableDictionary(this ValidationResult result, bool forceValidate)
        {
            return result.Errors
                .GroupBy(e => e.PropertyName)
                .ToObservableDictionary(
                e => e.Key,
                e => new ValidationError
                {
                    Message = e.FirstOrDefault()?.ErrorMessage,
                    ForceValidate = forceValidate
                });
        }

        public static ObservableDictionary<string, ValidationError> AsObservableDictionary(this ValidationResult result, bool forceValidate, Dictionary<string, string> mappings)
        {
            return result.Errors
                .GroupBy(e => e.PropertyName)
                .ToObservableDictionary(
                e => mappings[e.Key],
                e => new ValidationError
                {
                    Message = e.FirstOrDefault()?.ErrorMessage,
                    ForceValidate = forceValidate
                });
        }
    }
}
