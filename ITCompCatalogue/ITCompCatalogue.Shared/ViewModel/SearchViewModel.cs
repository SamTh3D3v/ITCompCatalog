using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Cour> _serachResult;
        private String _searchText = String.Empty;
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
            }
        }

        #endregion
        #region Commands

        #endregion
        #region Ctor and Methods

        #endregion
    }
}
