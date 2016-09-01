using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UIKit;

namespace Telling.iOS.Converters
{
    public class LoaderVisibilityConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return !value;
        }
    }

    public class DateTimeToStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value.ToString("dd MMM yyyy");
        }
    }

    public class StringToImageConverter : MvxValueConverter<string, UIImage>
    {
        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    return UIImage.FromBundle("images/games/" + value);
                }
            }
            catch(Exception ex)
            {
                // don't care
            }

            return UIImage.FromBundle("images/missing.png");
        }
    }
}
