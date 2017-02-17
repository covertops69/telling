using UIKit;

namespace Telling.iOS.Controls
{
    class TScrollView : UIScrollView
    {
        public TScrollView()
        {
            DelaysContentTouches = false;
            ShowsHorizontalScrollIndicator = false;
            ShowsVerticalScrollIndicator = false;
            TranslatesAutoresizingMaskIntoConstraints = false;
        }
    }
}