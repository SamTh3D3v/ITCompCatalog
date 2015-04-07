using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public class TechIdToImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((long)value)
            {
                case 1:
                    return "../Images/Android.png";
                    break; 
                case 2:
                    return "../Images/Microsoft.png";
                    break;
                case 7:
                    return "../Images/Oracle.png";
                    break;
                default:
                    return "../Images/General.png";
                    break;                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
