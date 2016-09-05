﻿using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.Constants;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TButtonView : UIButton
    {
        //UIImage _arrowImage;

        public nfloat TopPadding { get; set; } = 0f;
        //public nfloat Height { get; set; } = BaseViewHelper.ButtonHeight;

        public TButtonView(string title)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            BackgroundColor = ColorPalette.Pumpkin;

            SetTitle(title, UIControlState.Normal);

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            ContentEdgeInsets = new UIEdgeInsets(0, 10, -1, 0);

            Layer.CornerRadius = 2.5f;
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.BorderWidth = 1.0f;
            Layer.MasksToBounds = true;
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

                if (value)
                {
                    BackgroundColor = ColorPalette.DarkRed;
                }
                else
                {
                    BackgroundColor = ColorPalette.Pumpkin;
                }
            }
        }
    }
}
