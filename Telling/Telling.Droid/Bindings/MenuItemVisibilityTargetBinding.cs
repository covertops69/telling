using Android.Views;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using System.Diagnostics;
using Telling.Core.Validation;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class MenuItemVisibilityTargetBinding : MvxAndroidTargetBinding<IMenuItem, bool>
    {
        public MenuItemVisibilityTargetBinding(IMenuItem menuItem)
            : base(menuItem)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void SetValueImpl(IMenuItem menuItem, bool isVisible)
        {
            menuItem.SetVisible(isVisible);
        }
    }
}