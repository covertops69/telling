using Cirrious.FluentLayouts.Touch;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.ViewModels.Modals;
using Telling.iOS.Controls;
using Telling.iOS.Helpers;
using UIKit;
using Foundation;
using Telling.iOS.Interfaces;

namespace Telling.iOS.Views.Modals
{
    class ModalView : BaseViewController<ModalViewModel>, IMvxModalIosView
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //View.BackgroundColor = UIColor.FromHSBA(0f, 0f, 0f, 0.5f);

            //TLabel myLabel = new TLabel();
            //myLabel.Text = "This is my label";

            //Add(myLabel);

            //View.AddConstraints(new FluentLayout[] {
            //    myLabel.AtTopOf(View),
            //    myLabel.AtLeftOf(View)
            //});

            //View.UserInteractionEnabled = true;
            //View.AddGestureRecognizer(new UITapGestureRecognizer(tap =>
            //{
            //    NavigationController.SetNavigationBarHidden(true, true);
            //})
            //{
            //    NumberOfTapsRequired = 1
            //});
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //this.ModalPresentationStyle = UIKit.UIModalPresentationStyle.CurrentContext;

            //NavigationController.NavigationBar.Translucent = false;

            //NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            //NavigationController.NavigationBar.ShadowImage = new UIImage();

            //NavigationController.NavigationBar.BackgroundColor = ColorPalette.DarkRed;
            //NavigationController.NavigationBar.BarTintColor = ColorPalette.DarkRed;

            //NavigationController.View.BackgroundColor = UIColor.Clear;
        }
    }
}