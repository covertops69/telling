using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;

namespace Telling.Droid.Converters
{
    public class ImageNameValueConverter : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] splitString = value.Split('.');

            if (splitString.Length < 1)
            {
                return String.Empty;
            }
            else
            {
                return splitString[0];
            }
        }
    }
}