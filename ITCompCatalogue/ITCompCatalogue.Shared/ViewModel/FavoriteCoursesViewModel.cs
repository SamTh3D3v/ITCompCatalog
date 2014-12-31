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
    class FavoriteCoursesViewModel : ViewModelBase, INavigable
    {
        #region Fields
        private ObservableCollection<Cour> _listFavoriteCourses;
        private INavigationService _navigationService;
        private ICatalogueService _catalogueService;
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
                        _catalogueService.UnfavoriteAllCourses();
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
                    (cour) => _navigationService.NavigateTo("CourDetails", cour)));
            }
        }
        private RelayCommand<EventArgs> _unfavCourseCommand;
        public RelayCommand<EventArgs> UnfavCourseCommand
        {
            get
            {
                return _unfavCourseCommand
                    ?? (_unfavCourseCommand = new RelayCommand<EventArgs>(
                    (e) =>
                    {
                        var red = e;
                        //     _catalogueService.UnFavoriteCourse(null);
                    }));
            }
        }
        #endregion
        #region Ctors and Methods

        public FavoriteCoursesViewModel(INavigationService navigationService, ICatalogueService catalogueService)
        {
            _navigationService = navigationService;
            _catalogueService = catalogueService;
        }

        #endregion

        public async void Activate(object parameter)
        {
            ListFavoriteCourses = new ObservableCollection<Cour>(await _catalogueService.GetFavoriteCourses());
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
