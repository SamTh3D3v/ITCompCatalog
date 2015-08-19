using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class MainSeetingsViewModel : NavigableViewModelBase
    {
        #region Fields
        private bool _romingFavorire;
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
                ApplicationData.Current.RoamingSettings.Values["ThemeBrush"] = _redTheameBrushIsSelected;
                Messenger.Default.Send<bool>(_redTheameBrushIsSelected, "ThemeUpdate");
                
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
                ApplicationData.Current.RoamingSettings.Values["RoamingFavorite"] = _romingFavorire;
                NavigationService.NavigateTo("MainPage");
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _falyoutoadedCommand;
        public RelayCommand FalyoutoadedCommand
        {
            get
            {
                return _falyoutoadedCommand
                    ?? (_falyoutoadedCommand = new RelayCommand(
                    () =>
                    {

                        //RoamingFavorite = (bool)(ApplicationData.Current.RoamingSettings.Values["RoamingFavorite"]);
                        RedThemeBrushIsSelected = (bool)(ApplicationData.Current.RoamingSettings.Values["ThemeBrush"]);
                    }));
            }
        }

        #endregion
        #region Ctor and Methods
        public MainSeetingsViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {
            _romingFavorire = (bool)(ApplicationData.Current.RoamingSettings.Values["RoamingFavorite"]);
        }
        #endregion
    }



}
