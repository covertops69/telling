using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using System.Drawing;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Controls;
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
            bindingSet = BindLoader(bindingSet);

            var refreshButton = new UIButton(UIButtonType.Custom);
            refreshButton.SetImage(UIImage.FromBundle("Images/refresh.png"), UIControlState.Normal);
            refreshButton.Frame = new RectangleF(0, 0, 30, 30);

            bindingSet.Bind(refreshButton)
                .For("TouchUpInside")
                .To(vm => vm.RefreshCommand).Apply();

            var addButton = new UIButton(UIButtonType.Custom);
            addButton.SetImage(UIImage.FromBundle("Images/add.png"), UIControlState.Normal);
            addButton.Frame = new RectangleF(0, 0, 30, 30);

            bindingSet.Bind(addButton)
                .For("TouchUpInside")
                .To(vm => vm.NavigateToAddCommand).Apply();

            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null, null)
                {
                    Width = -10f
                },
                new UIBarButtonItem(refreshButton),
                new UIBarButtonItem(addButton),
            }, false);

            _table = new TTableView();
            Add(_table);

            var tableSource = new SessionTableSource(_table);
            bindingSet.Bind(tableSource).To(vm => vm.SessionsCollection).Apply();
            bindingSet.Bind(tableSource).For(vm => vm.SelectionChangedCommand).To(vm => vm.NavigateCommand).Apply();

            View.AddConstraints(new FluentLayout[]
            {
                _table.AtTopOf(View),
                _table.AtLeftOf(View),
                _table.WithSameWidth(View),
                _table.AtBottomOf(View)
            });

            _table.Source = tableSource;
            _table.ReloadData();
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            await ViewModel.LoadDataAsync();
        }
    }
}