using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Validation
{
    public interface IValidateRequest
    {
        /// <summary>
        /// Validates the specified request model.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request model.</typeparam>
        /// <typeparam name="TValidator">>The type of the validator to use.</typeparam>
        /// <param name="request">The request model.</param>
        /// <returns cref="ValidateResult">ValidateResult</returns>
        ValidateResult Validate<TRequest, TValidator>(TRequest request, bool forceValidate = false)
            where TRequest : class
            where TValidator : class, IValidator, new();

        /// <summary>
        /// Validates the specified request model and map the result.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request model.</typeparam>
        /// <typeparam name="TValidator">The type of the validator to use.</typeparam>
        /// <param name="request">The request model.</param>
        /// <param name="mappings">Mapping criteria for property names.</param>
        /// <returns cref="ValidateResult">ValidateResult</returns>
        ValidateResult Validate<TRequest, TValidator>(TRequest request, Dictionary<string, string> mappings, bool forceValidate = false)
             where TRequest : class
            where TValidator : class, IValidator, new();

        /// <summary>
        /// Validates the specified request models individual property.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request model.</typeparam>
        /// <typeparam name="TValidator">The type of the validator to use.</typeparam>
        /// <param name="request">The request model.</param>
        /// <param name="properties">The property names to run the rules against.</param>
        /// <returns cref="ValidateResult">ValidateResult</returns>
        ValidateResult ValidateProperty<TRequest, TValidator>(TRequest request, bool forceValidate = false, params string[] properties)
            where TRequest : class
            where TValidator : class, IValidator<TRequest>, new();
    }
}
