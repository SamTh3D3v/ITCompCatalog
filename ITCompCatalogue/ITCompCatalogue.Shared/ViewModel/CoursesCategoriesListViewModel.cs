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
    class CoursesCategoriesListViewModel:NavigableViewModelBase
    {
        #region Fields                
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
                            NavigationService.NavigateTo("CourDetails", cour);
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
                        () =>
                        {
                            NavigationService.NavigateTo("SearchView");
                        }));
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
        #region Ctors and Methods

        public CoursesCategoriesListViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            :base(catalogueService,navigationService)
        {
           
        }

        #endregion

        public override async void Activate(object parameter)
        {
           ListCategories=new ObservableCollection<Category>(await CatalogueService.GetCategoriesByTechnology((long)parameter));
        }

        public override void Deactivate(object parameter)
        {
            
        }
      
    }
}
