using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;

using ITCompCatalogue.Helper;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;


namespace ITCompCatalogue.ViewModel
{
    class MainViewModel : NavigableViewModelBase
    {      
        #region Fields     
        private Technology _selectedTechnology;
        private ObservableCollection<Technology> _listTechnologies ;        
        #endregion
        #region Properties
        public Technology SelectedTechnology
        {
            get
            {
                return _selectedTechnology;
            }

            set
            {
                if (_selectedTechnology == value)
                {
                    return;
                }
                _selectedTechnology = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Technology> ListTechnologies
        {
            get
            {
                return _listTechnologies;
            }

            set
            {
                if (_listTechnologies == value)
                {
                    return;
                }

                _listTechnologies = value;
                RaisePropertyChanged();
            }
        }
       
        #endregion
        #region Commands
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("SearchView")));
            }
        }
        private RelayCommand _presentationCommand;   
        public RelayCommand PresenationCommand
        {
            get
            {
                return  _presentationCommand
                    ?? ( _presentationCommand = new RelayCommand(
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

        private RelayCommand<Technology> _selectTechnologyCommand;  
         
        public RelayCommand<Technology> SelectTechnologyCommand
        {
            get
            {
                return _selectTechnologyCommand
                    ?? (_selectTechnologyCommand = new RelayCommand<Technology>(
                        (tech) =>
                        {
                            NavigationService.NavigateTo("Courses", tech.C_id);
                            
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
        #endregion
        public MainViewModel(ICatalogueService catalogueService,INavigationService navigationService)
            :base(catalogueService,navigationService)
        {            
            Initialisation();
        }

        private async void Initialisation()
        {
            try
            {
                ListTechnologies = new ObservableCollection<Technology>(await CatalogueService.GetAllTechnologies());               
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public override void  Activate(Object parameter)
        {
            //throw new NotImplementedException();
           
        }

        public override void Deactivate(object parameter)
        {
            //throw new NotImplementedException();
        }
        public override void GoBack()
        {
            NavigationService.GoBack();
        }
    }
}
