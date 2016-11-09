using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Controls;
using Telling.iOS.Interfaces;

namespace Telling.iOS.Bindings
{
    public class TFloatingTextFieldValidationTargetBinding : MvxConvertingTargetBinding
    {
        public TFloatingTextFieldValidationTargetBinding(IValidatable inputField)
            : base(inputField)
        {
        }

        public override Type TargetType => typeof(IValidatable);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(object target, object value)
        {
            var inputField = target as TFloatingTextField;
            var errorMessage = value as string;

            if (inputField == null)
                return;

            inputField.ValidationErrorMessage = errorMessage;
        }
    }
}
