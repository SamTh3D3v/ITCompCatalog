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
    class CourseDetailsViewModel:NavigableViewModelBase
    {
        #region Fields 
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
                    CatalogueService.UnFavoriteCourse(CourseDetails.C_id);
                }
                else
                {
                    CatalogueService.FavoriteCourse(_courseDetails.C_id);
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
                        IsCourseFavorite = true;

                    }));
            }
        }
        private RelayCommand _presentationCommand;
        public RelayCommand PresenationCommand
        {
            get
            {
                return _presentationCommand
                    ?? (_presentationCommand = new RelayCommand(
                    () =>
                    {
                        NavigationService.NavigateTo("PresentationView");
                    }));
            }
        }        
       
        private RelayCommand _refClientCommand;
        public RelayCommand RefClientsCommand
        {
            get
            {
                return _refClientCommand
                    ?? (_refClientCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("RefClient")));
            }
        }
        private RelayCommand _partnerCommand;
        public RelayCommand PartnerCommand
        {
            get
            {
                return _partnerCommand
                    ?? (_partnerCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("PartnerView")));
            }
        }
        private RelayCommand _contactCommand;
        public RelayCommand ContactCommand
        {
            get
            {
                return _contactCommand
                    ?? (_contactCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("ContactView")));
            }
        }
        private RelayCommand _favorieCommand;
        public RelayCommand FavorieCommand
        {
            get
            {
                return _favorieCommand
                    ?? (_favorieCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("FavoriteCoursesView")));
            }
        }
        private RelayCommand<Cour> _goToScheduleCommand;  
        public RelayCommand<Cour> GoToScheduleCommand
        {
            get
            {
                return _goToScheduleCommand
                    ?? (_goToScheduleCommand = new RelayCommand<Cour>(
                    (cour) => NavigationService.NavigateTo("ScheduleView",cour)));
            }
        }
        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return  _goBackCommand
                    ?? ( _goBackCommand = new RelayCommand(
                    () => NavigationService.GoBack()));
            }
        }
        #endregion
        #region Ctors and methods

        public CourseDetailsViewModel(INavigationService navigationService, ICatalogueService catalogueService)
            :base(catalogueService,navigationService)
        {          
        }
        #endregion

        public override void Activate(object parameter)
        {
            CourseDetails = (parameter as Cour);
            var cour = parameter as Cour;
            if (cour != null)
                IsCourseFavorite = CatalogueService.IsCourseFavorite(cour.C_id);
        }

        public override void Deactivate(object parameter)
        {
            
        }      
    }
}
