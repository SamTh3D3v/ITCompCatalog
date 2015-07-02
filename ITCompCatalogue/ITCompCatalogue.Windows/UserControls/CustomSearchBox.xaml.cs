using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ITCompCatalogue.UserControls
{
    public sealed partial class CustomSearchBox : UserControl
    {
        public CustomSearchBox()
        {
            this.InitializeComponent();
        }

        private void SearchBox_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ExpandAnimation.Begin();
        }

        private void SearchBox_OnPointerExited(object sender, RoutedEventArgs routedEventArgs)
        {
            ShrinkAnimation.Begin();            
        }

        
    }
}
