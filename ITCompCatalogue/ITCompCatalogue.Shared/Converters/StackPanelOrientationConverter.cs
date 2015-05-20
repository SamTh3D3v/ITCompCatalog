using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public class StackPanelOrientationConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool) value) ? Orientation.Horizontal : Orientation.Vertical;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((Orientation)value) == Orientation.Horizontal;
        }
    }
}
