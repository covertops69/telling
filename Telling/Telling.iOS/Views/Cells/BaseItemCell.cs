using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Views.Cells
{
    class BaseItemCell : MvxTableViewCell
    {
        public BaseItemCell(IntPtr handle)
            : base(handle)
        {
            BackgroundColor = UIColor.Clear;
        }
    }
}
