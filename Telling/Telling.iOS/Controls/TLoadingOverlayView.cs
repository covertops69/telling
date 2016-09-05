using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TLoadingOverlayView : UIView
    {
        public TLoadingOverlayView() : base()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            BackgroundColor = UIColor.FromRGBA(0f, 0f, 0f, 0.5f);
        }
    }
}
