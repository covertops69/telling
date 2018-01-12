using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using Telling.Core.Validation;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class InputEditTextErrorTargetBinding : MvxAndroidTargetBinding
    {
        string _previousValidationError;

        public InputEditTextErrorTargetBinding(TInputValidation textField)
            : base(textField)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void SetValueImpl(object editTextControl, object errorValue)
        {
            var control = (editTextControl as TInputValidation);
            var error = (errorValue as ValidationError);

            if (error == null)
            {
                control.ErrorText = string.Empty;
                control.Validate();
                return;
            }

            control.ErrorText = error.Message;

            if (error.ForceValidate || error.DoLiveValidation)
            {
                control.Validate();
                return;
            }

            if (_previousValidationError != null
                && (!string.IsNullOrEmpty(_previousValidationError))
                && string.IsNullOrEmpty(error.Message))
            {
                control.Validate();
            }

            _previousValidationError = error.Message;
        }

        public override Type TargetType
        {
            get { return typeof(TInputValidation); }
        }
    }
}