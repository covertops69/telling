using MvvmValidation;
using System;
using System.Linq;
using System.Linq.Expressions;
using Telling.Core.Extensions;
using Telling.Core.Helpers;
using Telling.Core.Models;

namespace Telling.Core.ViewModels
{
    public abstract class BaseValidationViewModel : BaseViewModel
    {
        protected ValidationHelper Validator { get; set; }

        private ObservableDictionary<string, TValidationError> _errors;
        public ObservableDictionary<string, TValidationError> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                SetProperty(ref _errors, value);
            }
        }

        protected BaseValidationViewModel()
        {
            Errors = new ObservableDictionary<string, TValidationError>();
            Validator = new ValidationHelper();

            ConfigureValidation();
        }

        protected virtual void ConfigureValidation()
        {

        }

        protected virtual bool Validate()
        {
            if (Validator.IsValidationSuspended)
            {
                return true;
            }

            var result = Validator.ValidateAll();
            var errors = result.AsObservableDictionary();

            Errors = new ObservableDictionary<string, TValidationError>(errors.ToDictionary(x => x.Key, x => new TValidationError
            {
                ForceValidate = true,
                Message = x.Value
            }));

            return result.IsValid;
        }

        protected virtual bool Validate(Expression<Func<object>> propertyExpression)
        {
            if (Validator.IsValidationSuspended)
            {
                return true;
            }

            var result = Validator.Validate(propertyExpression);
            var errors = result.AsObservableDictionary();

            Errors = new ObservableDictionary<string, TValidationError>(errors.ToDictionary(x => x.Key, x => new TValidationError
            {
                ForceValidate = true,
                Message = x.Value
            }));

            return result.IsValid;
        }

        protected void SetError<T>(Expression<Func<T>> propertyExpression, bool doLiveValidation = false)
        {
            var propName = GetPropertyName(propertyExpression);
            var validationResult = Validator.Validate(propName);
            var errors = validationResult.AsObservableDictionary();

            Errors[propName] = new TValidationError { ForceValidate = false, Message = errors[propName], DoLiveValidation = doLiveValidation };
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
    }
}