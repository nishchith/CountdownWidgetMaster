//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text;
//using System.Windows;
//using System.Windows.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GenericCountdown.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility vis = Visibility.Collapsed;

            if (value != null && value is bool?)
            {
                bool? val = (bool?)value;
                if (val.Value)
                {
                    vis = Visibility.Visible;
                }
            }

            return vis;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
