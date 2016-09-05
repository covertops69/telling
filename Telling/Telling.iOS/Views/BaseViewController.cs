using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.ViewModels;
using Telling.iOS.Controls;
using Telling.iOS.Converters;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public TLoadingOverlayView Loader { get; private set; }

        public BaseViewController()
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AddLoader();
        }

        private void AddLoader()
        {
            Loader = new TLoadingOverlayView();
            NavigationController.View.Add(Loader);

            NavigationController.View.AddConstraints(new FluentLayout[] {
                Loader.AtTopOf(NavigationController.View),
                Loader.AtLeftOf(NavigationController.View),
                Loader.WithSameWidth(NavigationController.View),
                Loader.WithSameHeight(NavigationController.View),
            });

            var activityIndicator = new UIActivityIndicatorView
            {
                Hidden = true
            };
            activityIndicator.StartAnimating();

            activityIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            Loader.AddSubview(activityIndicator);

            Loader.AddConstraints(new FluentLayout[]
            {
                activityIndicator.WithSameCenterX(Loader),
                activityIndicator.WithSameCenterY(Loader),
                activityIndicator.Width().EqualTo(50f),
                activityIndicator.Height().EqualTo(50f),
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.BackgroundColor = ColorPalette.Carnelian;

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

        public MvxFluentBindingDescriptionSet<ViewController, ViewModel> BindLoader<ViewController, ViewModel>(MvxFluentBindingDescriptionSet<ViewController, ViewModel> bindingSet)
            where ViewController : class, IMvxBindingContextOwner
            where ViewModel : BaseViewModel
        {
            bindingSet.Bind(Loader).For(b => b.Hidden).To(vm => vm.IsBusy).WithConversion(new LoaderVisibilityConverter()).Apply();
            bindingSet.Bind(this).For(c => c.Title).To(vm => vm.Title).Apply();

            return bindingSet;
        }
    }
}