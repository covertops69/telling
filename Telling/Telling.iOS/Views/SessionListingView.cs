using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;

namespace Telling.iOS.Views
{
    public class SessionListingView : MvxViewController<SessionListingViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<SessionListingView, SessionListingViewModel>();
            set.Apply();

        }
    }
}