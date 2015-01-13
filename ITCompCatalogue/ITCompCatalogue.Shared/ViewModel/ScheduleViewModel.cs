﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class ScheduleViewModel : ViewModelBase,INavigable
    {
        #region Fields

        private ICatalogueService _catalogueService;
        private readonly INavigationService _navigationService;
        #endregion
        #region Properties
        
        #endregion
        #region Commands
        private RelayCommand _navigateToIndexCommand;
        public RelayCommand NavigateToIndexCommand
        {
            get
            {
                return _navigateToIndexCommand
                    ?? (_navigateToIndexCommand = new RelayCommand(
                    () => _navigationService.NavigateTo("MainPage")));
            }
        }
        #endregion
        #region Ctor & Mothods

        public ScheduleViewModel(ICatalogueService catalogueService,INavigationService navigationService)
        {
            _catalogueService = catalogueService;
            _navigationService = navigationService;
        }
        
        #endregion

        public void Activate(object parameter)
        {
            
        }

        public void Deactivate(object parameter)
        {
            
        }

        public void GoBack()
        {
            
        }
    }
}
