using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class MainSeetingsViewModel : NavigableViewModelBase
    {

        #region Fields   
        private bool _romingFavorire  ;
        private bool _redTheameBrushIsSelected; 
        #endregion
        #region Properties
       
        public bool RedThemeBrushIsSelected
        {
            get
            {
                return _redTheameBrushIsSelected;
            }

            set
            {
                if (_redTheameBrushIsSelected == value)
                {
                    return;
                }

                _redTheameBrushIsSelected = value;
                ApplicationData.Current.LocalSettings.Values["ThemeBrush"] = _redTheameBrushIsSelected;
                RaisePropertyChanged();
            }
        }
        public bool RoamingFavorite
        {
            get
            {
                return _romingFavorire;
            }

            set
            {
                if (_romingFavorire == value)
                {
                    return;
                }

                _romingFavorire = value;
                ApplicationData.Current.LocalSettings.Values["RoamingFavorite"] = _romingFavorire;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands

        #endregion
        #region Ctor and Methods
        public MainSeetingsViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("RoamingFavorite"))
            {
                RoamingFavorite = (bool)(ApplicationData.Current.LocalSettings.Values["RoamingFavorite"]);
            }
            else
            {
                RoamingFavorite = true;
            }

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ThemeBrush"))
            {
                RedThemeBrushIsSelected = (bool) (ApplicationData.Current.LocalSettings.Values["ThemeBrush"]);
            }
            else
            {
                RedThemeBrushIsSelected = true;
            }
        }
        #endregion
    }



}
