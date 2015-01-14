using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;
using Telerik.UI.Xaml.Controls.Input;

namespace ITCompCatalogue.ViewModel
{
    class ScheduleViewModel : ViewModelBase,INavigable
    {
        #region Fields

        private ICatalogueService _catalogueService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<CoursSchedule> _coursesScheduleList;
        private CalendarDisplayMode _displayMode;
        #endregion
        #region Properties          
        public ObservableCollection<CoursSchedule> CoursesScheduleList
        {
            get
            {
                return _coursesScheduleList;
            }

            set
            {
                if (_coursesScheduleList == value)
                {
                    return;
                }

                _coursesScheduleList = value;
                RaisePropertyChanged();
            }
        }
             
        public CalendarDisplayMode DisplayMode
        {
            get
            {
                return _displayMode;
            }

            set
            {
                if (_displayMode == value)
                {
                    return;
                }

                _displayMode = value;
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
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand(
                    () => _navigationService.NavigateTo("SearchView")));
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

        public async void Activate(object parameter)
        {
            var course = parameter as Cour;
            if (course !=null)
            {
                CoursesScheduleList = new ObservableCollection<CoursSchedule>(await _catalogueService.GetCoursScheduleByCoursId(course.C_id)); 
            }
            else
            {
                var cursus = parameter as Cursu;
                if (cursus != null)
                    CoursesScheduleList = new ObservableCollection<CoursSchedule>(await _catalogueService.GetCoursScheduleByCursusId(cursus.C_id));
            }
        }

        public void Deactivate(object parameter)
        {
            
        }

        public void GoBack()
        {
            
        }
    }
}
