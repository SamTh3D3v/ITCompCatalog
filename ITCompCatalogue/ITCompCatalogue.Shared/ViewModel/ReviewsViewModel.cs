using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class ReviewsViewModel : NavigableViewModelBase
    {
        #region Fields
        
        #endregion
        #region Properties
        
        #endregion
        #region Commands
        
        #endregion
        #region Ctors and Methods
        public ReviewsViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {
        }
        #endregion        
    }
}
