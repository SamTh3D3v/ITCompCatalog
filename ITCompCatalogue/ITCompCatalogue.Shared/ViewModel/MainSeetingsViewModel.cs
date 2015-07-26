using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;
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
                ApplicationData.Current.RoamingSettings.Values["ThemeBrush"] = _redTheameBrushIsSelected;               
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
                        //Get the Data From The Roaming Folder
                        
                        if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("RoamingFavorite"))
                        {
                            RoamingFavorite = (bool)(ApplicationData.Current.RoamingSettings.Values["RoamingFavorite"]);
                        }
                        else
                        {
                            RoamingFavorite = true;

                        }
                        //Application.Current.RequestedTheme = _redTheameBrushIsSelected ? ApplicationTheme.Light : ApplicationTheme.Dark;

                        //if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("TheeBrush"))
                        //{
                        //    RedThemeBrushIsSelected = (bool)(ApplicationData.Current.RoamingSettings.Values["TheeBrush"]);
                        //}
                        //else
                        //{
                        //    RedThemeBrushIsSelected = true;
                        //}
                        
                    }));
            }
        }

        #endregion
        #region Ctor and Methods
        public MainSeetingsViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {
           
        }
        #endregion
    }



}
