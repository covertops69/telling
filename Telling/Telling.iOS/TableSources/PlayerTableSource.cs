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
    class PlayerTableSource : MvxTableViewSource
    {
        public PlayerTableSource(TTableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(PlayerListingItemCell), PlayerListingItemCell.CellIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (PlayerListingItemCell)tableView.DequeueReusableCell(PlayerListingItemCell.CellIdentifier, indexPath);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            tableView.DeselectRow(indexPath, true);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 35.0f;
        }
    }
}
