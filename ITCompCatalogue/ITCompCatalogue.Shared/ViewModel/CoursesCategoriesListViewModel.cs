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
    class CoursesCategoriesListViewModel : NavigableViewModelBase
    {
        #region Fields
        private Category _dateContextCategory;
        private ObservableCollection<Category> _listCategories;
        #endregion
        #region Properties
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
        }

        public override void Deactivate(object parameter)
        {

        }

    }
}
