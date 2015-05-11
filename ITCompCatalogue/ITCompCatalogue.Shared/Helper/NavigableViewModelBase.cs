using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.Helper
{
    public class NavigableViewModelBase:ViewModelBase,INavigable
    {
        #region Fields
        protected INavigationService NavigationService;
        protected ICatalogueService CatalogueService;
        #endregion     
        #region Ctors and Methods

        public NavigableViewModelBase(ICatalogueService catalogueService, INavigationService navigationService)
        {
            CatalogueService = catalogueService;
            NavigationService = navigationService;
        }
        public virtual void Activate(object parameter)
        {
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
