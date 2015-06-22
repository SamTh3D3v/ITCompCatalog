using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using ITCompCatalogue.Helper;
using ITCompCatalogue.View;

namespace ITCompCatalogue
{
   
    public sealed partial class MainPage : BindablePage
    {
        public MainPage()
        {                       
            this.InitializeComponent();
            SizeChanged += MainPage_SizeChanged;           
        }

        
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VisualStateManager.GoToState(this, GetState(e.NewSize.Width), true);            
        }
        private string GetState(double width)
        {              
        
            if (width <= 755)
                return "Snapped";           
            return "Default";
        }

        
    }
}
