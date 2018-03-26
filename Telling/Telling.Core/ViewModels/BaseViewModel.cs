using FluentValidation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
//using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Telling.Core.Helpers;
using Telling.Core.Models;
using Telling.Core.StateMachine;
using Telling.Core.Validation;
using Telling.Core.Validators;
using Telling.Core.ViewModels.Modals;

namespace Telling.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public BaseViewModel()
        {
            ValidationErrors = new ObservableDictionary<string, ValidationError>();
        }

        #region Validation

        public ValidateResult ValidateProperty<TRequest, TValidator>(IValidateRequest validateRequest, TRequest validationInput, bool forceValidate = false, params string[] properties)
            where TRequest : class
            where TValidator : class, IValidator<TRequest>, new()
        {
            return validateRequest.ValidateProperty<TRequest, TValidator>(validationInput, forceValidate, properties);
        }

        public void ValidateChange<TRequest, TValidator>(IValidateRequest validateRequest, TRequest request, string validationPropertyName = null, bool forceValidate = false, [CallerMemberName] string propertyName = null)
            where TRequest : class
            where TValidator : class, IValidator<TRequest>, new()
        {
            string mappingProperty = validationPropertyName ?? propertyName;

            ValidateResult validationResults = ValidateProperty<TRequest, TValidator>(validateRequest, request, forceValidate, mappingProperty);

            if (validationResults.IsValid)
            {
                ValidationErrors.Remove(mappingProperty);
            }
            else
            {
                KeyValuePair<string, ValidationError> errorMessage = validationResults.Errors.FirstOrDefault();
                ValidationErrors[mappingProperty] = errorMessage.Value;
            }

            RaisePropertyChanged(nameof(ValidationErrors));
        }

        public void ValidateChange<TPropertyType, TRequest, TValidator>(TPropertyType value, IValidateRequest validateRequest, string validationPropertyName = null, bool forceValidate = false)
            where TRequest : class, new()
            where TValidator : class, IValidator<TRequest>, new()
        {
            var request = new TRequest();
            PropertyInfo requestProperty = request.GetType().GetProperty(validationPropertyName);

            if (requestProperty == null)
            {
                Mvx.Error("No property named {0} found on {1}", validationPropertyName, typeof(TRequest));
                return;
            }

            requestProperty.SetValue(request, value);

            ValidateChange<TRequest, TValidator>(validateRequest, request, validationPropertyName, forceValidate);
        }

        ObservableDictionary<string, ValidationError> _validationErrors;
        public ObservableDictionary<string, ValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors ?? (_validationErrors = new ObservableDictionary<string, ValidationError>());
            }

            set
            {
                SetProperty(ref _validationErrors, value);
            }
        }

        #endregion

        public bool ProcessResponse<TResp>(BaseResponse<TResp> response)
            where TResp : class
        {
            var isSuccess = response?.ReponseType == ServiceReponseType.Successful;

            if (!isSuccess)
            {
                ShowViewModel<ModalViewModel>(new { exceptionMessage = response?.Message ?? Constants.API_ERROR_NO_MESSAGE });
            }

            return isSuccess;
        }

        protected void ShowException(Exception ex)
        {
            ShowViewModel<ModalViewModel>(new { exceptionMessage = ex.Message });
        }
    }
}
