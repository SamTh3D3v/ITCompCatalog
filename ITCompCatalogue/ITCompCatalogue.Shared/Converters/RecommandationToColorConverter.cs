using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ITCompCatalogue.Converters
{
    public class RecommandationToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value.ToString();
            switch (val)
            {
                case "Nécéssaire":
                    return new SolidColorBrush(Colors.Orange);                    
                case "Recommandé":
                    return new SolidColorBrush(Colors.Yellow);                    
                case "Facultatif":
                    return new SolidColorBrush(Colors.LimeGreen);                    
                case "Avancé":
                    return new SolidColorBrush(Colors.Red);                    
                default:
                    return new SolidColorBrush(Colors.Gray);                    

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
