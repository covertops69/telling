using Foundation;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Controls;
using Telling.iOS.Views.Cells;
using UIKit;

namespace Telling.iOS.TableSources
{
    class SessionTableSource : MvxTableViewSource
    {
        public SessionTableSource(TTableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(SessionListingItemCell), SessionListingItemCell.CellIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (SessionListingItemCell)tableView.DequeueReusableCell(SessionListingItemCell.CellIdentifier, indexPath);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            tableView.DeselectRow(indexPath, true);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 70.0f;
        }
    }
}
