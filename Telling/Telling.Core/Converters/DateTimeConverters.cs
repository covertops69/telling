using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Converters
{
    public class DateToShortDateNullValueConverter : MvxValueConverter<DateTime?, string>
    {
        protected override string Convert(DateTime? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == new DateTime(1, 1, 1, 0, 0, 0))
                return string.Empty;

            var stringParameter = parameter as string;

            if (string.IsNullOrWhiteSpace(stringParameter))
                return ((DateTime)value).ToString("d MMM yyyy");

            return ((DateTime)value).ToString(stringParameter);
        }

        protected override DateTime? ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var returnType = DateTime.ParseExact(value, parameter as string, culture);
            return returnType;
        }
    }
}
