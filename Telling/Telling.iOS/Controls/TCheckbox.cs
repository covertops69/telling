using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Telling.iOS.Controls
{
    class TCheckbox : UIButton
    {
        string _selectedImage;
        string _unselectedImage;

        public event EventHandler OnChecked;

        bool _checked;
        public bool Checked
        {
            get
            {
                return _checked;
            }

            set
            {
                if (value)
                {
                    SetImage(UIImage.FromBundle(_selectedImage), UIControlState.Normal);
                }
                else
                {
                    SetImage(UIImage.FromBundle(_unselectedImage), UIControlState.Normal);
                }

                _checked = value;
            }
        }

        public TCheckbox()
        {
            _selectedImage = "images/checked.png";
            _unselectedImage = "images/unchecked.png";

            Initialize();
        }

        void Initialize()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            SetImage(UIImage.FromBundle(_unselectedImage), UIControlState.Normal);

            TouchUpInside += (s, e) =>
            {
                Checked = !Checked;

                if (OnChecked != null)
                {
                    OnChecked(this, EventArgs.Empty);
                }
            };
        }
    }
}
