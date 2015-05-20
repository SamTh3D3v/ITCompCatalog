using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public class TechIdToImageTileConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((long)value)
            {
                case 1:
                    return "../Images/Tiles/Android.jpg";
                    break;
                case 2:
                    return "../Images/Tiles/Microsoft.jpg";
                    break;
                case 7:
                    return "../Images/Tiles/Orcale.gif";
                    break;
                default:
                    return "../Images/Tiles/General.png";
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
