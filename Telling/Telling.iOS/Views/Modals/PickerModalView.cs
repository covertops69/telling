using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Telling.iOS.Views.Modals
{
    //class PickerModalView : BaseViewController<PickerModalViewModel>, IMvxModalIosView
    //{
    //    public override void ViewDidLoad()
    //    {
    //        base.ViewDidLoad();

    //        var bindingSet = this.CreateBindingSet<PickerModalView, PickerModalViewModel>();
    //        bindingSet = BindLoader(bindingSet);

    //        var exceptionDetails = new TLabelView();
    //        Add(exceptionDetails);

    //        bindingSet.Bind(exceptionDetails).To(vm => vm.Exception).Apply();

    //        var closeButton = new UIButton(UIButtonType.Custom);
    //        closeButton.SetImage(UIImage.FromBundle("images/close.png"), UIControlState.Normal);
    //        closeButton.Frame = new RectangleF(0, 0, 30, 30);
    //        NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] {
    //            new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null, null)
    //            {
    //                Width = -5f
    //            },
    //            new UIBarButtonItem(closeButton)
    //        }, false);

    //        bindingSet.Bind(closeButton)
    //            .For("TouchUpInside")
    //            .To(vm => vm.CloseCommand).Apply();

    //        View.AddConstraints(new FluentLayout[] {
    //            exceptionDetails.AtTopOf(View, Constants.Margin),
    //            exceptionDetails.AtLeftOf(View, Constants.Margin),
    //            exceptionDetails.WithSameWidth(View).Minus(Constants.Margin * 2)
    //        });
    //    }
    //}
}
