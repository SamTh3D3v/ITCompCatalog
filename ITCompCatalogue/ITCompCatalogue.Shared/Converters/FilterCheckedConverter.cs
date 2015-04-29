using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public class FilterCheckedConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var va = value;
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
