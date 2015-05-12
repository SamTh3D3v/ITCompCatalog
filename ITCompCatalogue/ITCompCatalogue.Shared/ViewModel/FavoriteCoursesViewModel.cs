using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
   public class FavoriteCoursesViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Cour> _listFavoriteCourses;
        #endregion
        #region Properties
        public ObservableCollection<Cour> ListFavoriteCourses
        {
            get
            {
                return _listFavoriteCourses;
            }

            set
            {
                if (_listFavoriteCourses == value)
                {
                    return;
                }

                _listFavoriteCourses = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _removeAllCommand;
        public RelayCommand RemoveAllCommand
        {
            get
            {
                return _removeAllCommand
                    ?? (_removeAllCommand = new RelayCommand(
                    () =>
                    {
                        ListFavoriteCourses.Clear();
                        CatalogueService.UnfavoriteAllCourses();
                    }));
            }
        }
        private RelayCommand<Cour> _navigateToCourseCommand;
        public RelayCommand<Cour> NavigateToCourseCommand
        {
            get
            {
                return _navigateToCourseCommand
                    ?? (_navigateToCourseCommand = new RelayCommand<Cour>(
                    (cour) => NavigationService.NavigateTo("CourDetails", cour)));
            }
        }
        private RelayCommand<long> _unfavCourseCommand;
        public RelayCommand<long> UnfavCourseCommand
        {
            get
            {
                return _unfavCourseCommand
                    ?? (_unfavCourseCommand = new RelayCommand<long>(async (idCourse) =>
                        {
                            CatalogueService.UnFavoriteCourse(idCourse);
                            ListFavoriteCourses = new ObservableCollection<Cour>(await CatalogueService.GetFavoriteCourses());
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
                    () => NavigationService.NavigateTo("MainPage")));
            }
        }
        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand
                    ?? (_goBackCommand = new RelayCommand(
                    () => NavigationService.GoBack()));
            }
        }
        #endregion
        #region Ctors and Methods

        public FavoriteCoursesViewModel(INavigationService navigationService, ICatalogueService catalogueService)
            :base(catalogueService,navigationService)
        {            
        }
        public override async void Activate(object parameter)
        {
            ListFavoriteCourses = new ObservableCollection<Cour>(await CatalogueService.GetFavoriteCourses());
        }

        public override void Deactivate(object parameter)
        {

        }     
        #endregion

       
    }
}
