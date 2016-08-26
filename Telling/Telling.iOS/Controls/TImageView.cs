using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TImageView : UIImageView
    {
        public TImageView(UIImage image) : base(image)
        {
            ContentMode = UIViewContentMode.Redraw;
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public static TImageView FromBundle(string imageName)
        {
            var image = UIImage.FromBundle(imageName);
            var imageView = new TImageView(image);
            return imageView;
        }
    }
}