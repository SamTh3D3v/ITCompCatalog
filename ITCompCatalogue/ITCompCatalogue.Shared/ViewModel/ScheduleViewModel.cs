using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Annotations;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;
using Telerik.UI.Xaml.Controls.Input;
using WinRTXamlToolkit.Tools;

namespace ITCompCatalogue.ViewModel
{
    class ScheduleViewModel : ViewModelBase, INavigable
    {
        #region Fields

        private ICatalogueService _catalogueService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<CoursSchedule> _coursesScheduleList;
        private CalendarDisplayMode _displayMode;
        private ObservableCollection<CoursSchedule> _listCoursesInDate;
        private ObservableCollection<CourVisible> _listCoursesInCursus;
        private List<CoursSchedule> _globaleCoursesScheduleList;
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
        public ObservableCollection<CoursSchedule> ListCoursesInDate
        {
            get
            {
                return _listCoursesInDate;
            }

            set
            {
                if (_listCoursesInDate == value)
                {
                    return;
                }

                _listCoursesInDate = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<CourVisible> ListCoursesInCursus
        {
            get
            {
                return _listCoursesInCursus;
            }

            set
            {
                if (_listCoursesInCursus == value)
                {
                    return;
                }

                _listCoursesInCursus = value;
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
        private RelayCommand<DateTime> _cellTappedCommand;
        public RelayCommand<DateTime> CellTappedCommand
        {
            get
            {
                return _cellTappedCommand
                    ?? (_cellTappedCommand = new RelayCommand<DateTime>(
                    (date) =>
                    {
                        ListCoursesInDate = new ObservableCollection<CoursSchedule>(CoursesScheduleList.Where(c => c.DateDebut <= date && c.DateFin >= date).GroupBy(x => x.CoursId).Select(y => y.FirstOrDefault()));
                    }));
            }
        }

        private RelayCommand<CoursSchedule> _navigateToCourseCommand;

        public RelayCommand<CoursSchedule> NavigateToCourseCommand
        {
            get
            {
                return _navigateToCourseCommand
                    ?? (_navigateToCourseCommand = new RelayCommand<CoursSchedule>(
                    (courSce) =>
                    {
                        var cour = _catalogueService.GetCourseByCourseId(courSce.CoursId);
                        _navigationService.NavigateTo("CourDetails", cour);
                    }));
            }
        }
        private RelayCommand _filterCheckedCommand;
        public RelayCommand FilterCheckedCommand
        {
            get
            {
                return _filterCheckedCommand
                    ?? (_filterCheckedCommand = new RelayCommand(
                    () =>
                    {
                        foreach (var cour in ListCoursesInCursus)
                        {
                            var courSec = _globaleCoursesScheduleList.FirstOrDefault(x => x.CoursId == cour.C_id);
                            if (courSec != null)
                                CoursesScheduleList.Add(courSec); 
                        }
                    }));
            }
        }
        private RelayCommand _filterUnCheckedCommand;
        public RelayCommand FilterUnCheckedCommand
        {
            get
            {
                return _filterUnCheckedCommand
                    ?? (_filterUnCheckedCommand = new RelayCommand(
                    () =>
                    {
                        foreach (var cour in ListCoursesInCursus)
                        {
                            var courSec = _globaleCoursesScheduleList.FirstOrDefault(x => x.CoursId == cour.C_id);
                            if (courSec != null)
                                CoursesScheduleList.Remove(courSec);
                        }
                    }));
            }
        }

        #endregion
        #region Ctor & Mothods
        public ScheduleViewModel(ICatalogueService catalogueService, INavigationService navigationService)
        {
            _catalogueService = catalogueService;
            _navigationService = navigationService;
        }
        public async void Activate(object parameter)
        {
            var course = parameter as Cour;
            if (course != null)
            {
                CoursesScheduleList = new ObservableCollection<CoursSchedule>(await _catalogueService.GetCoursScheduleByCoursId(course.C_id));
            }
            else
            {
                var cursus = parameter as Cursu;
                if (cursus != null)
                {
                    _globaleCoursesScheduleList = await _catalogueService.GetCoursScheduleByCursusId(cursus.C_id);
                    CoursesScheduleList = new ObservableCollection<CoursSchedule>(_globaleCoursesScheduleList);
                    var courInCursus = await _catalogueService.GetCoursesByCursusId(cursus.C_id);
                    ListCoursesInCursus = new ObservableCollection<CourVisible>(courInCursus.Select(x => new CourVisible()
                    {
                        C_id = x.C_id,
                        Intitule = x.Intitule,
                        Category = x.Category,
                        Visible = true
                    }));
                }
            }
        }
        public void Deactivate(object parameter)
        {

        }
        public void GoBack()
        {

        }

        #endregion

    }

    public class CourVisible : Cour, INotifyPropertyChanged
    {
        private bool _visible = true;
        public bool Visible
        {
            get
            {
                return _visible;
            }

            set
            {
                if (_visible == value)
                {
                    return;
                }

                _visible = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
