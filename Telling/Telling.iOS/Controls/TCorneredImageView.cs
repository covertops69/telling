using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TCorneredImageView : UIImageView
    {
        public TCorneredImageView(UIImage image) : base(image)
        {
            ContentMode = UIViewContentMode.Redraw;
            TranslatesAutoresizingMaskIntoConstraints = false;

            Layer.CornerRadius = 5.0f;
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.BorderWidth = 1.0f;
            Layer.MasksToBounds = true;
        }

        public static TImageView FromBundle(string imageName)
        {
            var image = UIImage.FromBundle(imageName);
            var imageView = new TImageView(image);
            return imageView;
        }
    }
}