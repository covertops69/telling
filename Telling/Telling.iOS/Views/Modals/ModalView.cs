using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System.Drawing;
using Telling.Core.Constants;
using Telling.Core.ViewModels.Modals;
using Telling.iOS.Controls;
using Telling.iOS.Converters;
using UIKit;

namespace Telling.iOS.Views.Modals
{
    class ModalView : BaseViewController<ModalViewModel>, IMvxModalIosView
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<ModalView, ModalViewModel>();
            bindingSet.Bind(Loader).For(b => b.Hidden).To(vm => vm.IsBusy).WithConversion(new LoaderVisibilityConverter()).Apply();
            bindingSet.Bind(this).For(c => c.Title).To(vm => vm.Title).Apply();

            var exceptionDetails = new TLabel();
            Add(exceptionDetails);

            bindingSet.Bind(exceptionDetails).To(vm => vm.Exception).Apply();

            var closeButton = new UIButton(UIButtonType.Custom);
            closeButton.SetImage(UIImage.FromBundle("images/close.png"), UIControlState.Normal);
            closeButton.Frame = new RectangleF(0, 0, 35, 35);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null, null)
                {
                    Width = -5f
                },
                new UIBarButtonItem(closeButton)
            }, false);

            bindingSet.Bind(closeButton)
                .For("TouchUpInside")
                .To(vm => vm.CloseCommand).Apply();

            View.AddConstraints(new FluentLayout[] {
                exceptionDetails.AtTopOf(View, Constants.Margin),
                exceptionDetails.AtLeftOf(View, Constants.Margin),
                exceptionDetails.WithSameWidth(View).Minus(Constants.Margin * 2)
            });
        }
    }
}