using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Helper
{
    public class ListViewEx : ListView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ListViewItem listItem = element as ListViewItem;
            Binding binding = new Binding();
            binding.Mode = BindingMode.TwoWay;
            binding.Source = item;
            binding.Path = new PropertyPath("IsTechSelected");
            listItem.SetBinding(ListViewItem.IsSelectedProperty, binding);
        }
    }
}
