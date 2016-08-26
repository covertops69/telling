using MvvmCross.iOS.Views.Presenters;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS
{
    public class TellingPresenter : MvxIosViewPresenter
    {
        public TellingPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
    }
}
