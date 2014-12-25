using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class CourseDetailsViewModel:ViewModelBase,INavigable
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private readonly ICatalogueService _catalogueService;
        private Cour _courseDetails;
        #endregion
        #region Properties
        public Cour CourseDetails
        {
            get
            {
                return _courseDetails;
            }

            set
            {
                if (_courseDetails == value)
                {
                    return;
                }

                _courseDetails = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands    
        private RelayCommand _searchCommand;    
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        #endregion
        #region Ctors and methods

        public CourseDetailsViewModel(INavigationService navigationService, ICatalogueService catalogueService)
        {
            _navigationService = navigationService;
            _catalogueService = catalogueService;

        }
        #endregion

        public void Activate(object parameter)
        {
            CourseDetails = (parameter as Cour);
        }

        public void Deactivate(object parameter)
        {
            
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }
    }
}
