using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ITCompCatalogue.Helper
{
    public class ItemTemplateSelector:DataTemplateSelector
    {
        public DataTemplate SimpleListTemplate { get; set; }
        public DataTemplate MultipleTilesTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {                 
            return App.IsListViewSelected ? SimpleListTemplate : MultipleTilesTemplate;
        }
    }
}
