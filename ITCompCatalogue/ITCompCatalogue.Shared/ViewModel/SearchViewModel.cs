using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Cour> _serachResult;
        private String _searchText = String.Empty;
        private readonly ICatalogueService _catalogueService;
        private bool _parCodeIsChecked = true;
        private bool _parIntituleIsChecked = true;
        private bool _parDescIsChecked = true;
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

        public bool ParCodeIsChecked
        {
            get
            {
                return _parCodeIsChecked;
            }

            set
            {
                if (_parCodeIsChecked == value)
                {
                    return;
                }

                _parCodeIsChecked = value;
                RaisePropertyChanged();
            }
        }
        public bool ParIntituleIsChecked
        {
            get
            {
                return _parIntituleIsChecked;
            }

            set
            {
                if (_parIntituleIsChecked == value)
                {
                    return;
                }

                _parIntituleIsChecked = value;
                RaisePropertyChanged();
            }
        }
        public bool ParDescIsChecked
        {
            get
            {
                return _parDescIsChecked;
            }

            set
            {
                if (_parDescIsChecked == value)
                {
                    return;
                }

                _parDescIsChecked = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands

        #endregion
        #region Ctor and Methods
        private async void SearchCourses()
        {
           SearchResult=new ObservableCollection<Cour>(await _catalogueService.SearchCourses(SearchText)); 
        }

        public SearchViewModel(ICatalogueService catalogueService)
        {
            _catalogueService = catalogueService;
        }
        #endregion
    }
}
