using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System.Drawing;
using Telling.Core;
using Telling.Core.ViewModels.Modals;
using Telling.iOS.Controls;
using UIKit;

namespace Telling.iOS.Views.Modals
{
    class ExceptionModalView : BaseViewController<ModalViewModel>, IMvxModalIosView
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<ExceptionModalView, ModalViewModel>();
            bindingSet = BindLoader(bindingSet);

            var exceptionDetails = new TLabelView();
            Add(exceptionDetails);

            bindingSet.Bind(exceptionDetails).To(vm => vm.Exception).Apply();

            var closeButton = new UIButton(UIButtonType.Custom);
            closeButton.SetImage(UIImage.FromBundle("Images/close.png"), UIControlState.Normal);
            closeButton.Frame = new RectangleF(0, 0, 30, 30);
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
                exceptionDetails.AtTopOf(View, Constants.MARGIN),
                exceptionDetails.AtLeftOf(View, Constants.MARGIN),
                exceptionDetails.WithSameWidth(View).Minus(Constants.MARGIN * 2)
            });
        }
    }
}