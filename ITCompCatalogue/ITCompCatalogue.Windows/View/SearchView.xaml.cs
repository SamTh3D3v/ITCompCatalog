﻿using System;
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
using ITCompCatalogue.Helper;


namespace ITCompCatalogue.View
{
    public sealed partial class SearchView : BindablePage
    {
        public SearchView()
        {
            this.InitializeComponent();
        }


        private void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_OnItemClick(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var a = 3;
        }
    }
}
