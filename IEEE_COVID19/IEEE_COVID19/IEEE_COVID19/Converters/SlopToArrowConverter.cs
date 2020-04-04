using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IEEE_COVID19.Converters
{
    public class SlopToArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string slope)) return string.Empty;
            if (slope.Equals("U"))
                return FontAwesome.FontAwesomeIcons.ArrowUp;
            return slope.Equals("D") ? FontAwesome.FontAwesomeIcons.ArrowDown : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
