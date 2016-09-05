using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TTextFieldLabelView : TLabelView
    {
        public TTextFieldLabelView()
        {
            TextColor = ColorPalette.DarkPumpkin;
            Font = UIFont.SystemFontOfSize(10f);
        }
    }
}
