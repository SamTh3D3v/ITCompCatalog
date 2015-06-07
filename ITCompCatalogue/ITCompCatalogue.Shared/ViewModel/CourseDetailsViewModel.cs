using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
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
        private TypedEventHandler<DataTransferManager, DataRequestedEventArgs> _shareHandler;
        private DataTransferManager _dataTransferManager;
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
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand(
                        () =>
                        {
                            NavigationService.NavigateTo("SearchView");
                        }));
            }
        }
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
        private RelayCommand _homeCommand;
        public RelayCommand HomeCommand
        {
            get
            {
                return _homeCommand
                    ?? (_homeCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("MainPage")));
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
        private RelayCommand _shareCommand;
        public RelayCommand ShareCommand
        {
            get
            {
                return _shareCommand
                    ?? (_shareCommand = new RelayCommand(
                    DataTransferManager.ShowShareUI));
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
            {
                IsCourseFavorite = CatalogueService.IsCourseFavorite(cour.C_id);
                RegisterForShare();
            }
                
        }

        public override void Deactivate(object parameter)
        {
            _dataTransferManager.DataRequested -= _shareHandler;
        }
        private void RegisterForShare()
        {
             _dataTransferManager = DataTransferManager.GetForCurrentView();            
            _shareHandler = new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareTextHandler);
            _dataTransferManager.DataRequested += _shareHandler;
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            // The Title is mandatory
            request.Data.Properties.Title = CourseDetails.Code+": "+CourseDetails.Intitule;
            request.Data.Properties.Description = "";
            request.Data.SetText(CourseDetails.Description+"\n\n Category: "+CourseDetails.Category.Intitule);
        }
    }
}
