using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    public class TTextFieldWithLabel : UIView
    {
        bool _initialized = false;

        TTextField _textField;
        public TTextField TextField
        {
            get { return _textField; }
            set { _textField = value; }
        }

        TLabelView _labelView;
        public TLabelView LabelView
        {
            get { return _labelView; }
            set { _labelView = value; }
        }

        public TTextFieldWithLabel()
        {
            ContentMode = UIViewContentMode.Redraw;

            _labelView = new TLabelView();
            _textField = new TTextField();

            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public override void Draw(CGRect rect)
        {
            Initialize();
            base.Draw(rect);
        }

        void Initialize()
        {
            if (!_initialized)
            {
                Add(_labelView);
                Add(_textField);

                this.AddConstraints(new FluentLayout[] {
                    _labelView.AtTopOf(this),
                    _labelView.AtLeftOf(this),

                    _textField.Below(_labelView),
                    _labelView.ToLeftOf(this)
                });

                _initialized = true;
            }
        }
    }
}
