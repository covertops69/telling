using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.ViewModels;

namespace Telling.iOS.Views
{
    public class EventListingView : MvxViewController<EventListingViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<EventListingView, EventListingViewModel>();
            set.Apply();

        }
    }
}