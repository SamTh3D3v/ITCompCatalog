using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Cour> _serachResult;
        private String _searchText = String.Empty;
        private readonly ICatalogueService _catalogueService;
        private String _searchBySelectedItem;
        private INavigationService _navigationService;

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

        #endregion
        #region Commands
        private RelayCommand<Cour> _selectCourseCommand;
        public RelayCommand<Cour> SelectCourseCommand
        {
            get
            {
                return _selectCourseCommand
                    ?? (_selectCourseCommand = new RelayCommand<Cour>(
                    (cour) => _navigationService.NavigateTo("CourDetails", cour)));
            }
        }
        #endregion
        #region Ctor and Methods
        private async void SearchCourses()
        {
            SearchResult = new ObservableCollection<Cour>(await _catalogueService.SearchCourses(SearchText, SearchBySelectedItem));
        }

        public SearchViewModel(ICatalogueService catalogueService,INavigationService navigationService)
        {
            _catalogueService = catalogueService;
            _navigationService = navigationService;
            SearchBySelectedItem = SearchByItems.First();
        }
        #endregion
    }
}
