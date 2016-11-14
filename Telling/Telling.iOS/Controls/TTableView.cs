using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TTableView : UITableView
    {
        public TTableView() : base()
        {
            ShowsHorizontalScrollIndicator = false;
            ShowsVerticalScrollIndicator = false;
            TranslatesAutoresizingMaskIntoConstraints = false;

            BackgroundColor = UIColor.Clear;

            SeparatorStyle = UITableViewCellSeparatorStyle.None;

            DelaysContentTouches = false;
        }
    }
}
