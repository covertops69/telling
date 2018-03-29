using Android.Views;
using Android.Webkit;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Telling.Core.Validation;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class MenuItemClickEventTargetBinding : MvxAndroidTargetBinding<IMenu, ICommand>
    {
        public MenuItemClickEventTargetBinding(IMenu menu)
            : base(menu)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(IMenu menu, ICommand value)
        {
            //menu.M
        }
    }
}