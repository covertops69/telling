using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Controls;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Views.Cells
{
    class BaseItemCell : MvxTableViewCell
    {
        public BaseItemCell(IntPtr handle)
            : base(handle)
        {
            BackgroundColor = UIColor.Clear;

            SelectedBackgroundView = new TView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = ColorPalette.DarkRed
            };
        }
    }
}
