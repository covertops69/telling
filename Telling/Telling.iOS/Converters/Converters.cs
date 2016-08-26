﻿using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Telling.iOS.Converters
{
    public class LoaderVisibilityConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return !value;
        }
    }
}