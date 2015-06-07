using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class SearchViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Cour> _serachResult;
        private String _searchText = String.Empty;        
        private String _searchBySelectedItem;        
        private bool _searchIntituleIsEnabled = true;
        private bool _searchCodeIsSelected = false;

        private ObservableCollection<String> _searchByItems = new ObservableCollection<string>()
        {
            "Code",
            "Intitule"
        };    
        #endregion
        #region Properties
        public ObservableCollection<Cour> SearchResult
        {
            get
            {
                return _serachResult;
            }

            set
            {
                if (_serachResult == value)
                {
                    return;
                }

                _serachResult = value;
                RaisePropertyChanged();
            }
        }
        public String SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged();
                SearchCourses();
            }
        }
        public String SearchBySelectedItem
        {
            get
            {
                return _searchBySelectedItem;
            }

            set
            {
                if (_searchBySelectedItem == value)
                {
                    return;
                }
                _searchBySelectedItem = value;
                RaisePropertyChanged();
                SearchCourses();
            }
        }
        public ObservableCollection<String> SearchByItems
        {
            get
            {
                return _searchByItems;
            }

            set
            {
                if (_searchByItems == value)
                {
                    return;
                }

                _searchByItems = value;
                RaisePropertyChanged();
            }
        }
        public bool SearchCodeIsSelected
        {
            get
            {
                return _searchCodeIsSelected;
            }

            set
            {
                if (_searchCodeIsSelected == value)
                {
                    return;
                }

                _searchCodeIsSelected = value;
                if (_searchCodeIsSelected)
                {                    
                    SearchBySelectedItem = "Code";
                }               
                RaisePropertyChanged();
            }
        }
    
        public bool SearchIntituleIsEnabled
        {
            get
            {
                return _searchIntituleIsEnabled;
            }

            set
            {
                if (_searchIntituleIsEnabled == value)
                {
                    return;
                }

                _searchIntituleIsEnabled = value;
                if (_searchIntituleIsEnabled)
                {                    
                    SearchBySelectedItem = "Intitule";
                }               
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand<Cour> _selectCourseCommand;
        public RelayCommand<Cour> SelectCourseCommand
        {
            get
            {
                return _selectCourseCommand
                    ?? (_selectCourseCommand = new RelayCommand<Cour>(
                        (cour) => NavigationService.NavigateTo("CourDetails", cour)));
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
        
        #endregion
        #region Ctor and Methods
        private async void SearchCourses()
        {
            SearchResult = new ObservableCollection<Cour>(await CatalogueService.SearchCourses(SearchText, SearchBySelectedItem));
        }

        public SearchViewModel(ICatalogueService catalogueService,INavigationService navigationService)
            :base(catalogueService,navigationService)
        {           
            SearchBySelectedItem = SearchByItems.First();
        }
        #endregion
    }
}
