using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

namespace ITCompCatalogue.Converters
{
    public class RecommandationToGlyphConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {  
            
            var val = value.ToString();
            switch (val)
            {
                case "Nécéssaire":
                    return "&#xE1E3;";                    
                case "Recommandé":
                    return "&#xE203;";                     
                case "Facultatif":
                    return "&#xE234;";                     
                case "Avancé":
                    return "&#xE1DE;";                     
                default:
                    return "&#xE20D;";                     
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
