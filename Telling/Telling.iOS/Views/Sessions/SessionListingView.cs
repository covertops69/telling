using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Controls;
using Telling.iOS.Converters;
using Telling.iOS.TableSources;
using UIKit;

namespace Telling.iOS.Views.Sessions
{
    public class SessionListingView : BaseViewController<SessionListingViewModel>
    {
        TTableView _table;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<SessionListingView, SessionListingViewModel>();
            bindingSet.Bind(Loader).For(b => b.Hidden).To(vm => vm.IsBusy).WithConversion(new LoaderVisibilityConverter()).Apply();
            bindingSet.Bind(this).For(c => c.Title).To(vm => vm.Title).Apply();

            var refreshButton = new UIButton(UIButtonType.Custom);
            refreshButton.SetImage(UIImage.FromBundle("images/refresh.png"), UIControlState.Normal);
            refreshButton.Frame = new RectangleF(0, 0, 30, 30);

            var addButton = new UIButton(UIButtonType.Custom);
            addButton.SetImage(UIImage.FromBundle("images/add.png"), UIControlState.Normal);
            addButton.Frame = new RectangleF(0, 0, 30, 30);

            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null, null)
                {
                    Width = -10f
                },
                new UIBarButtonItem(refreshButton),
                new UIBarButtonItem(addButton),
            }, false);

            bindingSet.Bind(refreshButton)
                .For("TouchUpInside")
                .To(vm => vm.RefreshCommand).Apply();

            _table = new TTableView();
            Add(_table);

            var tableSource = new SessionTableSource(_table);
            bindingSet.Bind(tableSource).To(vm => vm.SessionsCollection);
            bindingSet.Bind(tableSource).For(vm => vm.SelectionChangedCommand).To(vm => vm.NavigateCommand);

            View.AddConstraints(new FluentLayout[]
            {
                _table.AtTopOf(View),
                _table.AtLeftOf(View),
                _table.WithSameWidth(View),
                _table.AtBottomOf(View)
            });

            bindingSet.Apply();

            _table.Source = tableSource;
            _table.ReloadData();
        }
    }
}