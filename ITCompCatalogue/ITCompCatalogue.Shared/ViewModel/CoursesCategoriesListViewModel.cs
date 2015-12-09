using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Converters;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class CoursesCategoriesListViewModel : NavigableViewModelBase
    {
        #region Fields
        private Category _dateContextCategory;
        private ObservableCollection<Category> _listCategories;
        private bool _searchIsEnabled = false;
        #endregion
        #region Properties
        public bool SearchIsEnabled
        {
            get
            {
                return _searchIsEnabled;
            }

            set
            {
                if (_searchIsEnabled == value)
                {
                    return;
                }

                _searchIsEnabled = value;
                RaisePropertyChanged();
            }
        }     
        public Category DataContextCategory
        {
            get
            {
                return _dateContextCategory;
            }

            set
            {
                if (_dateContextCategory == value)
                {
                    return;
                }

                _dateContextCategory = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Category> ListCategories
        {
            get
            {
                return _listCategories;
            }
            set
            {
                if (_listCategories == value)
                {
                    return;
                }

                _listCategories = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand<Cour> _courseSelectedCommand;
        public RelayCommand<Cour> CourseSelectedCommand
        {
            get
            {
                return _courseSelectedCommand
                    ?? (_courseSelectedCommand = new RelayCommand<Cour>(
                        (cour) =>
                        {
                            NavigationService.NavigateTo("CourDetails", cour);
                        }));
            }
        }
        private RelayCommand<Object> _searchCommand;
        public RelayCommand<Object> SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand<Object>(
                    (queryText) => NavigationService.NavigateTo("SearchView", queryText)));
            }
        }
        private RelayCommand<Cursu> _goToScheduleCommand;
        public RelayCommand<Cursu> GoToScheduleCommand
        {
            get
            {
                return _goToScheduleCommand
                    ?? (_goToScheduleCommand = new RelayCommand<Cursu>(
                    (cursus) => base.NavigationService.NavigateTo("ScheduleViewWithFilter", cursus)));
            }
        }
        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand
                    ?? (_goBackCommand = new RelayCommand(
                    () => base.NavigationService.GoBack()));
            }
        }
        #endregion
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
        private RelayCommand _listTechnologiesCommand;
        public RelayCommand ListTechnologiesCommand
        {
            get
            {
                return _listTechnologiesCommand
                    ?? (_listTechnologiesCommand = new RelayCommand(
                    () =>
                    {

                        NavigationService.NavigateTo("ListTechnologiesView");


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
        private RelayCommand<ISuggestionQuery> _suggestionRequestCommand;
        public RelayCommand<ISuggestionQuery> SuggestionRequest
        {
            get
            {
                return _suggestionRequestCommand
                    ?? (_suggestionRequestCommand = new RelayCommand<ISuggestionQuery>(async (query) =>
                    {
                        IEnumerable<Cour> filteredQuery = await CatalogueService.SearchCourses(query.QueryText, null);
                        foreach (var cour in filteredQuery)
                        {
                            RandomAccessStreamReference stream;
                            switch (cour.Category.TechnologieID)
                            {
                                case 1:
                                    stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Android.png"));
                                    break;
                                case 2:
                                    stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Microsoft.png"));
                                    break;
                                case 7:
                                    stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Oracle.png"));
                                    break;
                                default:
                                    stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/General.png"));
                                    break;
                            }
                            query.Request.SearchSuggestionCollection.AppendResultSuggestion(cour.Code, cour.Intitule, cour.Category.TechnologieID.ToString(), stream, "Result");
                        }
                    }));
            }
        }
        private RelayCommand<object> _suggestionSelectedCommand;
        public RelayCommand<object> SuggestionSelectedCommand
        {
            get
            {
                return _suggestionSelectedCommand
                    ?? (_suggestionSelectedCommand = new RelayCommand<object>(
                        (args) =>
                        {

                            var course = CatalogueService.GetCourseByCourseCode(args.ToString());
                            NavigationService.NavigateTo("CourDetails", course);
                        }));
            }
        }
        private RelayCommand _pageLoadedCommand;
        public RelayCommand PageLoadedCommand
        {
            get
            {
                return _pageLoadedCommand
                    ?? (_pageLoadedCommand = new RelayCommand(
                    () =>
                    {
                        SearchIsEnabled = true;
                    }));
            }
        }
        #region Ctors and Methods

        public CoursesCategoriesListViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {

        }

        #endregion

        public override async void Activate(object parameter)
        {
            var cat = parameter as Category;
            if (cat == null)
                ListCategories =
                    new ObservableCollection<Category>(
                        await CatalogueService.GetCategoriesByTechnology((long) parameter));
            else
                DataContextCategory = cat;

            base.Activate(parameter);
        }

        public override void Deactivate(object parameter)
        {
            SearchIsEnabled = false;
        }

    }
}
