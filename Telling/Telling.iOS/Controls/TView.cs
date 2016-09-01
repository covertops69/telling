using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TView : UIView
    {
        public TView() : base()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public static TView MakeSeperator(UIColor color = null)
        {
            if (color == null)
            {
                color = ColorPalette.DarkPumpkin;
            }

            var seperator = new TView
            {
                BackgroundColor = color
            };

            return seperator;
        }
    }
}