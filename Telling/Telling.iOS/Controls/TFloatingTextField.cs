using CoreAnimation;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Helpers;
using Telling.iOS.Interfaces;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TFloatingTextField : UITextField, IValidatable
    {
        private readonly UILabel _floatingLabel;
        private readonly UILabel _validationLabel;

        string _validationErrorMessage;
        public string ValidationErrorMessage
        {
            get
            {
                return _validationErrorMessage;
            }

            set
            {
                _validationErrorMessage = value;
                _validationLabel.Text = _validationErrorMessage;
            }
        }

        public UIColor FloatingLabelTextColor { get; set; }
        public UIColor FloatingLabelActiveTextColor { get; set; }
        public UIFont FloatingLabelFont
        {
            get { return _floatingLabel.Font; }
            set { _floatingLabel.Font = value; }
        }
        private bool _editable = true;
        public bool Editable
        {
            get { return _editable; }
            set {

                _editable = value;

                if (_editable)
                {
                    ShouldChangeCharacters = (t, r, s) =>
                    {
                        return true;
                    };
                }
                else
                {
                    ShouldChangeCharacters = (t, r, s) =>
                    {
                        return false;
                    };
                }
            }
        }

        CALayer _bottomBorder;

        public TFloatingTextField(CGRect frame)
            : base(frame)
        {
            TextColor = UIColor.White;
            TranslatesAutoresizingMaskIntoConstraints = false;

            AddBottomLine();

            _floatingLabel = new UILabel()
            {
                Alpha = 0.0f
            };

            AddSubview(_floatingLabel);

            FloatingLabelTextColor = ColorPalette.DarkRed;
            FloatingLabelActiveTextColor = ColorPalette.DarkPumpkin;
            FloatingLabelFont = UIFont.BoldSystemFontOfSize(12);

            _validationLabel = new UILabel()
            {
                Text = String.Empty,
                Font = UIFont.BoldSystemFontOfSize(12),
                TextColor = ColorPalette.DeepLemon
            };

            AddSubview(_validationLabel);
        }

        private void AddBottomLine()
        {
            _bottomBorder = new CALayer
            {
                BackgroundColor = ColorPalette.Pumpkin.CGColor
            };

            Layer.AddSublayer(_bottomBorder);
        }

        public override string Placeholder
        {
            get { return base.Placeholder; }
            set
            {
                base.Placeholder = value;

                _floatingLabel.Text = value;
                _floatingLabel.SizeToFit();
                _floatingLabel.Frame =
                    new CGRect(
                        0, _floatingLabel.Font.LineHeight,
                        _floatingLabel.Frame.Size.Width, _floatingLabel.Frame.Size.Height);
            }
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            if (_floatingLabel == null)
                return base.TextRect(forBounds);

            return InsetRect(base.TextRect(forBounds), new UIEdgeInsets(_floatingLabel.Font.LineHeight, 0, 0, 0));
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            if (_floatingLabel == null)
                return base.EditingRect(forBounds);

            return InsetRect(base.EditingRect(forBounds), new UIEdgeInsets(_floatingLabel.Font.LineHeight, 0, 0, 0));
        }

        public override CGRect ClearButtonRect(CGRect forBounds)
        {
            var rect = base.ClearButtonRect(forBounds);

            if (_floatingLabel == null)
                return rect;

            return new CGRect(
                rect.X, rect.Y + _floatingLabel.Font.LineHeight / 2.0f,
                rect.Size.Width, rect.Size.Height);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            // update bottom border frame
            _bottomBorder.Frame = new CGRect(0.0f, Frame.Size.Height - 1.0f, Frame.Size.Width, 1.0f);

            _validationLabel.SizeToFit();
            _validationLabel.Frame = new CGRect(
                        0, Frame.Height,
                        Frame.Width, _validationLabel.Font.LineHeight);

            Action updateLabel = () =>
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    _floatingLabel.Alpha = 1.0f;
                    _floatingLabel.Frame =
                        new CGRect(
                            _floatingLabel.Frame.Location.X,
                            2.0f,
                            _floatingLabel.Frame.Size.Width,
                            _floatingLabel.Frame.Size.Height);
                }
                else
                {
                    _floatingLabel.Alpha = 0.0f;
                    _floatingLabel.Frame =
                        new CGRect(
                            _floatingLabel.Frame.Location.X,
                            _floatingLabel.Font.LineHeight,
                            _floatingLabel.Frame.Size.Width,
                            _floatingLabel.Frame.Size.Height);
                }
            };

            if (IsFirstResponder)
            {
                _floatingLabel.TextColor = FloatingLabelActiveTextColor;

                var shouldFloat = !string.IsNullOrEmpty(Text);
                var isFloating = _floatingLabel.Alpha == 1f;

                if (shouldFloat == isFloating)
                {
                    updateLabel();
                }
                else
                {
                    UIView.Animate(
                        0.3f, 0.0f,
                        UIViewAnimationOptions.BeginFromCurrentState
                        | UIViewAnimationOptions.CurveEaseOut,
                        () => updateLabel(),
                        () => { });
                }
            }
            else
            {
                _floatingLabel.TextColor = FloatingLabelTextColor;

                updateLabel();
            }
        }

        public override CGRect GetCaretRectForPosition(UITextPosition position)
        {
            if (!Editable)
            {
                return CGRect.Empty;
            }
            else
            {
                return base.GetCaretRectForPosition(position);
            }
        }

        private static CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
        {
            return new CGRect(
                rect.X + insets.Left,
                rect.Y + insets.Top,
                rect.Width - insets.Left - insets.Right,
                rect.Height - insets.Top - insets.Bottom);
        }
    }
}