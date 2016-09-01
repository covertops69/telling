using Cirrious.FluentLayouts.Touch;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public UIActivityIndicatorView Loader { get; private set; }

        public BaseViewController()
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Loader = new UIActivityIndicatorView
            {
                Hidden = true
            };
            Loader.StartAnimating();

            Loader.TranslatesAutoresizingMaskIntoConstraints = false;
            Add(Loader);

            View.BringSubviewToFront(Loader);

            View.AddConstraints(new FluentLayout[]
            {
                Loader.WithSameCenterX(View),
                Loader.WithSameCenterY(View),
                Loader.Width().EqualTo(50f),
                Loader.Height().EqualTo(50f),
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = UIColor.White
            };

            NavigationController.NavigationBar.Translucent = false;

            NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationController.NavigationBar.ShadowImage = new UIImage();

            NavigationController.NavigationBar.BackgroundColor = ColorPalette.DarkRed;
            NavigationController.NavigationBar.BarTintColor = ColorPalette.DarkRed;
            NavigationController.NavigationBar.TintColor = UIColor.White;

            NavigationController.View.BackgroundColor = ColorPalette.Carnelian;
        }
    }
}