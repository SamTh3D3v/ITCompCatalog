using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.Converters
{
    public class ListViewItemClickedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as ItemClickEventArgs;
            if (args != null)
                return(args.ClickedItem as CursusCour).Cour;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
