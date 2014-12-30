using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                       //Clear the LIst From The Isolated Storage As Well
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        
        #endregion
    }
}
