﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ITCompCatalogue.ViewModel
{
    public class ContactViewModel:ViewModelBase
    {
        #region Fields

        private INavigationService _navigationService;
        private String _phoneNumber = "+213 (0) 21 56 32 33";
            private String _faxNumber = "+213 (0) 21 56 18 26";
                private String _email = "Lazhar.Guendouz@ITComp-dz.com";
                    private String _altEmail = "Radia.Lamari@ITComp-dz.com";
                    private String _siteWeb = "www.itcomp-dz.com";
                        private String _adress = "10, rue Khoudjat Eldjeld, Bir Mourad Rais Alger. Algérie.";
                            
        #endregion 
        #region Properties
        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                if (_phoneNumber == value)
                {
                    return;
                }

                _phoneNumber = value;
                RaisePropertyChanged();
            }
        }
        public String FaxNumber
        {
            get
            {
                return _faxNumber;
            }

            set
            {
                if (_faxNumber == value)
                {
                    return;
                }

                _faxNumber = value;
                RaisePropertyChanged();
            }
        }
        public String Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email == value)
                {
                    return;
                }

                _email = value;
                RaisePropertyChanged();
            }
        }               
        public String SiteWeb
        {
            get
            {
                return _siteWeb;
            }

            set
            {
                if (_siteWeb == value)
                {
                    return;
                }

                _siteWeb = value;
                RaisePropertyChanged();
            }
        }          
        public String Adress
        {
            get
            {
                return _adress;
            }

            set
            {
                if (_adress == value)
                {
                    return;
                }

                _adress = value;
                RaisePropertyChanged();
            }
        }
        public String AltEmail
        {
            get
            {
                return _altEmail;
            }

            set
            {
                if (_altEmail == value)
                {
                    return;
                }

                _altEmail = value;
                RaisePropertyChanged();
            }
        }
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
        private RelayCommand<String> _callCommand;
        public RelayCommand<String> CallCommand
        {
            get
            {   
                return  _callCommand
                    ?? ( _callCommand = new RelayCommand<String>(
                    (phoneNumber) => Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(phoneNumber, "ITComp")));
            }
        }
        #endregion 
        #region Ctors and Methods

        public ContactViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion
    }
}
