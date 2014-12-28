using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class CoursesCategoriesListViewModel:ViewModelBase,INavigable
    {
        #region Fields
        private readonly ICatalogueService _catalogueService;
        private readonly INavigationService _navigationService;
        #endregion
        #region Properties  
        private ObservableCollection<Category> _listCategories; 
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
                            _navigationService.NavigateTo("CourDetails", cour);
                        }));
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
        #region Ctors and Methods

        public CoursesCategoriesListViewModel(ICatalogueService catalogueService, INavigationService navigationService)
        {
            _catalogueService = catalogueService;
            _navigationService = navigationService;
        }

        #endregion

        public async void Activate(object parameter)
        {
           ListCategories=new ObservableCollection<Category>(await _catalogueService.GetCategoriesByTechnology((long)parameter));
        }

        public void Deactivate(object parameter)
        {
            
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }
    }
}
