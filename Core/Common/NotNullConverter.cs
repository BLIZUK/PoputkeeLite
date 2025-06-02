// Common/NotNullConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace PoputkeeLite.Core.Common
{
    public class NotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}