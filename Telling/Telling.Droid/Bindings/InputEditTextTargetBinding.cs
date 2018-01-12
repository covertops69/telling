using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Platform.Droid.WeakSubscription;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class InputEditTextTargetBinding : MvxAndroidTargetBinding
    {
        IDisposable _subscriptionTextChanged;
        IDisposable _subscriptionFocusChanged;

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public InputEditTextTargetBinding(TInputValidation inputField)
            : base(inputField)
        {
        }

        public override void SubscribeToEvents()
        {
            var target = (Target as TInputValidation);

            if (target?.EditTextControl != null)
            {
                _subscriptionTextChanged = target.WeakSubscribe<TInputValidation, TextChangedEventArgs>(
                   nameof(TInputValidation.TextChanged),
                   OnTextChanged);

                _subscriptionFocusChanged = target.EditTextControl.WeakSubscribe<View, View.FocusChangeEventArgs>(
                   nameof(TInputValidation.FocusChange),
                   OnFocusChanged);
            }
        }

        private void OnFocusChanged(object sender, View.FocusChangeEventArgs e)
        {
            var target = (Target as TInputValidation);

            if (!e.HasFocus)
            {
                FireValueChanged(target.EditTextControl.Text);

                if (!string.IsNullOrEmpty(target.ErrorText))
                    target.Validate();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var target = (Target as TInputValidation);

            if (target == null)
                return;

            FireValueChanged(target.Text);
        }

        protected override void SetValueImpl(object target, object value)
        {
            (target as TInputValidation).Text = value.ToString();
        }

        public override Type TargetType
        {
            get { return typeof(TInputValidation); }
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _subscriptionTextChanged?.Dispose();
                _subscriptionTextChanged = null;
                _subscriptionFocusChanged?.Dispose();
                _subscriptionFocusChanged = null;
            }
            base.Dispose(isDisposing);
        }
    }
}