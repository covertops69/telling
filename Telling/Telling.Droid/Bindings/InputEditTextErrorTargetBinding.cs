using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using System.Diagnostics;
using Telling.Core.Validation;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class InputEditTextErrorTargetBinding : MvxAndroidTargetBinding<TInputValidation, ValidationError>
    {
        //string _previousValidationError;

        public InputEditTextErrorTargetBinding(TInputValidation textField)
            : base(textField)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void SetValueImpl(TInputValidation editTextControl, ValidationError errorValue)
        {
            var controlNameId = editTextControl.Context.Resources.GetResourceEntryName(editTextControl.Id);
            Debug.WriteLine(string.Format("{0} >> {1}", controlNameId, errorValue?.Message ?? "NULL"));

            ;

            if (!string.IsNullOrEmpty(errorValue?.Message))
            {
                editTextControl.ErrorText = errorValue?.Message;
            }
            else
            {
                editTextControl.ErrorText = null;
            }

            editTextControl.Validate();

            //if (errorValue == null)
            //{
            //    editTextControl.ErrorText = string.Empty;
            //    //editTextControl.Validate();
            //    return;
            //}

            //editTextControl.ErrorText = errorValue.Message;

            //if (errorValue.ForceValidate || errorValue.DoLiveValidation)
            //{
            //    editTextControl.Validate();
            //    return;
            //}

            //if (_previousValidationError != null
            //    && (!string.IsNullOrEmpty(_previousValidationError))
            //    && string.IsNullOrEmpty(errorValue.Message))
            //{
            //    editTextControl.Validate();
            //}

            //_previousValidationError = errorValue.Message;
        }
    }
}