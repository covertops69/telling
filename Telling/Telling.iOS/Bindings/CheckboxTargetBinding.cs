using MvvmCross.Binding.Bindings.Target;
using System;
using Telling.iOS.Controls;
using MvvmCross.Binding;

namespace Telling.iOS.Bindings
{
    class CheckboxTargetBinding : MvxTargetBinding
    {
        public CheckboxTargetBinding(TCheckbox target) : base(target)
        {
            target.OnChecked += CheckedChanged;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            var target = Target as TCheckbox;

            if (target == null)
                return;

            FireValueChanged(target.Checked);
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public override Type TargetType => typeof(bool);

        public override void SetValue(object value)
        {
            var target = Target as TCheckbox;

            if (target == null)
                return;

            target.Checked = (bool)value;
        }
    }
}