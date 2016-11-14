using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace Telling.iOS.Controls
{
    class TFloatingPicker : TFloatingTextField
    {
        public TFloatingPicker(CGRect frame) : base(frame)
        {
            ShouldChangeCharacters += TFloatingPicker_ShouldChangeCharacters;
            this.TintColor = UIColor.Clear; // hide cursor
        }

        private bool TFloatingPicker_ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            return false; // prevent manual editing
        }
    }
}
