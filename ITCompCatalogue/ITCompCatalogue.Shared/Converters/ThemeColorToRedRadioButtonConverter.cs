using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ITCompCatalogue.Converters
{
    public class ThemeColorToRedRadioButtonConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value==null)            
                return null;
            return ((SolidColorBrush) value).Color == Colors.Red;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            return ((bool) value) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.DeepSkyBlue);
        }
    }
}
