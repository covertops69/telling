using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Interfaces;
using UIKit;

namespace Telling.iOS
{
    public class TellingPresenter : MvxIosViewPresenter
    {
        public TellingPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public override void Show(IMvxIosView view)
        {
            var viewControllerToShow = (UIViewController)view;

            if (view is IMvxModalIosView)
            {
                var newNav = new UINavigationController(viewControllerToShow);
                PresentModalViewController(newNav, true);

                return;
            }

            if (MasterNavigationController == null)
                ShowFirstView(viewControllerToShow);
            else
                ((UINavigationController)CurrentTopViewController).PushViewController(viewControllerToShow, true);
        }
    }
}
