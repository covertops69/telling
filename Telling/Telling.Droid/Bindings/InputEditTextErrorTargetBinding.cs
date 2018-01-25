using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using Telling.Core.Validation;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class InputEditTextErrorTargetBinding : MvxAndroidTargetBinding<SGInputValidation, ValidationError>
    {
        string _previousValidationError;

        public InputEditTextErrorTargetBinding(SGInputValidation textField)
            : base(textField)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void SetValueImpl(SGInputValidation editTextControl, ValidationError errorValue)
        {
            if (errorValue == null)
            {
                editTextControl.ErrorText = string.Empty;
                editTextControl.Validate();
                return;
            }

            editTextControl.ErrorText = errorValue.Message;

            if (errorValue.ForceValidate || errorValue.DoLiveValidation)
            {
                editTextControl.Validate();
                return;
            }

            if (_previousValidationError != null
                && (!string.IsNullOrEmpty(_previousValidationError))
                && string.IsNullOrEmpty(errorValue.Message))
            {
                editTextControl.Validate();
            }

            _previousValidationError = errorValue.Message;
        }
    }
}