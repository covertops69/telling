using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.Constants;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TButton : UIButton
    {
        //UIImage _arrowImage;

        public nfloat Height { get; set; } = Constants.ButtonHeight;

        public TButton(string title)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            //BackgroundColor = ColorPalette.Green;

            SetTitle(title, UIControlState.Normal);

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            ContentEdgeInsets = new UIEdgeInsets(0, 10, -1, 0);

            //Font = UIFont.FromName("Telkom123-Bold", 15.0f);
            //_arrowImage = UIImage.FromBundle("images/white_arrow_right.png");

            //SetImage(_arrowImage, UIControlState.Normal);
        }

        public override void Draw(CGRect frame)
        {
            ImageEdgeInsets = new UIEdgeInsets(0, Frame.Size.Width/* - (_arrowImage.Size.Width + 20)*/, 0, 0);
        }

        public override bool Highlighted
        {
            get
            {
                return base.Highlighted;
            }

            set
            {
                base.Highlighted = value;

                //if (value)
                //{
                //    BackgroundColor = ColorPalette.Grey;
                //}
                //else
                //{
                //    BackgroundColor = ColorPalette.Green;
                //}
            }
        }
    }
}
