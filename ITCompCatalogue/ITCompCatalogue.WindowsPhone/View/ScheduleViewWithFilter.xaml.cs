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
using Telerik.UI.Xaml.Controls.Input.Calendar;

namespace ITCompCatalogue.View
{
    public sealed partial class ScheduleViewWithFilter : BindablePage
    {
        public ScheduleViewWithFilter()
        {
            this.InitializeComponent();
        }
        private void Calendar_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            e.Handled = false;
        }
    }
}
