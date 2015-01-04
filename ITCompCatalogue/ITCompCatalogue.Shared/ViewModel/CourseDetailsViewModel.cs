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
        private bool _isCourseFavorite;
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
        public bool IsCourseFavorite
        {
            get
            {
                return _isCourseFavorite;
            }

            set
            {
                if (_isCourseFavorite == value)
                {
                    return;
                }

                _isCourseFavorite = value;
                RaisePropertyChanged();
                if (!_isCourseFavorite)
                {
                    _catalogueService.UnFavoriteCourse(CourseDetails.C_id);
                }
                else
                {
                    _catalogueService.FavoriteCourse(_courseDetails.C_id);
                }
            }
        }
        #endregion
        #region Commands    
        private RelayCommand _favoriteCommand;    
        public RelayCommand FavoriteCommand
        {
            get
            {
                return _favoriteCommand
                    ?? (_favoriteCommand = new RelayCommand(
                    () =>
                    {
                        //add this courses to the list of courses in the fav localStorage File
                        
                        IsCourseFavorite = true;

                    }));
            }
        }
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
            var cour = parameter as Cour;
            if (cour != null)
                IsCourseFavorite = _catalogueService.IsCourseFavorite(cour.C_id);
        }

        public void Deactivate(object parameter)
        {
            
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
