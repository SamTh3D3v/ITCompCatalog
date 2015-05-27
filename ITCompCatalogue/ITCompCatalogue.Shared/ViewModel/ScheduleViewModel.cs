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


namespace ITCompCatalogue.ViewModel
{
    public class ScheduleViewModel : NavigableViewModelBase
    {
        #region Fields
                
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
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("SearchView")));
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
                        var cour = CatalogueService.GetCourseByCourseId(courSce.CoursId);
                        NavigationService.NavigateTo("CourDetails", cour);
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
        public ScheduleViewModel(ICatalogueService catalogueService, INavigationService navigationService):base(catalogueService,navigationService)
        {
          
        }
        public async override void Activate(object parameter)
        {
            var course = parameter as Cour;
            if (course != null)
            {
                CoursesScheduleList = new ObservableCollection<CoursSchedule>(await CatalogueService.GetCoursScheduleByCoursId(course.C_id));
            }
            else
            {
                var cursus = parameter as Cursu;
                if (cursus != null)
                {
                    _globaleCoursesScheduleList = await CatalogueService.GetCoursScheduleByCursusId(cursus.C_id);
                    CoursesScheduleList = new ObservableCollection<CoursSchedule>(_globaleCoursesScheduleList);
                    var courInCursus = await CatalogueService.GetCoursesByCursusId(cursus.C_id);
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
        public override void  Deactivate(object parameter)
        {

        }
        public override void GoBack()
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
