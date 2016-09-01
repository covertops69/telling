using MvvmCross.Binding.BindingContext;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Converters;

namespace Telling.iOS.Views.Sessions
{
    class AddSessionView : BaseViewController<AddSessionViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<AddSessionView, AddSessionViewModel>();
            bindingSet.Bind(Loader).For(b => b.Hidden).To(vm => vm.IsBusy).WithConversion(new LoaderVisibilityConverter()).Apply();
            bindingSet.Bind(this).For(c => c.Title).To(vm => vm.Title).Apply();
        }
    }
}
