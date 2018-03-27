using System.Globalization;
using Android.Graphics;
using MvvmCross.Platform.Droid;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;

namespace Telling.Droid.Converters
{
    public class BackgroundValueConverter : MvxColorValueConverter<bool>
    {
        protected override MvxColor Convert(bool value, object parameter, CultureInfo culture)
        {
            string selectedColorString = MvvmCross.Platform.Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.GetString(Resource.Color.dark_red);
            Color selectedColor = Android.Graphics.Color.ParseColor(selectedColorString);

            string unSelectedColorString = MvvmCross.Platform.Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.GetString(Resource.Color.carnelian);
            Color unSelectedColor = Android.Graphics.Color.ParseColor(unSelectedColorString);

            return value
                ? new MvxColor(selectedColor.R, selectedColor.G, selectedColor.B, 127)
                : new MvxColor(unSelectedColor.R, unSelectedColor.G, unSelectedColor.B);
        }
    }
}