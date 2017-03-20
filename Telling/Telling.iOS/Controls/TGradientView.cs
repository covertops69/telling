using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using Telling.Core;
using Telling.iOS.Helpers;

namespace Telling.iOS.Controls
{
    public class TGradientView : UIView
    {
        private CAGradientLayer _gradientLayer;

        public TGradientView(UIColor startColor, UIColor endColor) : base()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            _gradientLayer = new CAGradientLayer();
            _gradientLayer.Colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
            this.Layer.InsertSublayer(_gradientLayer, 0);
        }

        public override void LayoutSublayersOfLayer(CALayer layer)
        {
            base.LayoutSublayersOfLayer(layer);
            _gradientLayer.Frame = this.Bounds;
        }
    }
}