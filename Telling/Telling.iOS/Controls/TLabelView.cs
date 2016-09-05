using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TLabelView : UILabel
    {
        public TLabelView()
            : base()
        {
            Lines = 0;
            LineBreakMode = UILineBreakMode.WordWrap;
            TextColor = UIColor.White;
            //Font = UIFont.FromName("BrookeShappell8", 28f);
            TranslatesAutoresizingMaskIntoConstraints = false;
        }
    }
}
