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
                if (score > 80)
                    return "img_80.png";
                if (score > 60)
                    return "img_60.png";
                if (score > 40)
                    return "img_40.png";
                if (score > 20)
                    return "img_20.png";
                else return "img_0.png";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
