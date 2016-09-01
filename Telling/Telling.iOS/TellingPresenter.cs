using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using Telling.Core.ViewModels.Modals;
using UIKit;

namespace Telling.iOS
{
    public class TellingPresenter : MvxIosViewPresenter
    {
        UINavigationController _modalNavigationController;

        public TellingPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public override void Show(IMvxIosView view)
        {
            var viewControllerToShow = (UIViewController)view;

            if (view is IMvxModalIosView)
            {
                _modalNavigationController = new UINavigationController(viewControllerToShow);
                PresentModalViewController(_modalNavigationController, true);

                return;
            }

            base.Show((IMvxIosView)viewControllerToShow);
        }

        public override void Close(IMvxViewModel toClose)
        {
            if (toClose is ModalViewModel)
            {
                _modalNavigationController?.DismissViewController(true, null);
                return;
            }

            base.Close(toClose);
        }
    }
}
