using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.Helper
{
    public class NavigableViewModelBase : ViewModelBase, INavigable
    {
        #region Fields
        protected INavigationService NavigationService;
        protected ICatalogueService CatalogueService;        
        #endregion
        #region Properties
       
        #endregion

        #region Ctors and Methods
        public NavigableViewModelBase(ICatalogueService catalogueService, INavigationService navigationService)
        {
            CatalogueService = catalogueService;
            NavigationService = navigationService;
                   
        }
        public virtual void Activate(object parameter)
        {
            //if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ThemeBrush"))
            //{
            //    ThemeBrush = (bool)(ApplicationData.Current.LocalSettings.Values["ThemeBrush"]) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.DeepSkyBlue);
            //}
            //else
            //{
            //    ThemeBrush = new SolidColorBrush(Colors.DeepSkyBlue);
            //}
        }
        public virtual void Deactivate(object parameter)
        {
        }
        public virtual void GoBack()
        {
        }
        #endregion
    }
}
