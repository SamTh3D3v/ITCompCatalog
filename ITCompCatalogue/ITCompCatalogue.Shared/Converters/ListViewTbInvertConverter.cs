﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public class ListViewTbInvertConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !((bool) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !((bool)value);
        }
    }
}
