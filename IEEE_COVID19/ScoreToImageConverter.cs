using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IEEE_COVID19.Converters
{
    class ScoreToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var score = value as int?;
            if (score != null)
            {
                if (score > 85)
                    return "";
                if (score > 65)
                    return "";
                if (score > 45)
                    return "";
                else return "";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
