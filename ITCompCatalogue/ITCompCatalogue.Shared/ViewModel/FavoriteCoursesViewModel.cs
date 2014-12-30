using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class FavoriteCoursesViewModel:ViewModelBase
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
        
        #endregion
        #region Ctors and Methods
        
        #endregion
    }
}
