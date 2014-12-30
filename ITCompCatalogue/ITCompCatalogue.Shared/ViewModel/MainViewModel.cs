using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class MainViewModel : ViewModelBase,INavigable
    {
        #region Cons
        #endregion
        #region Fields

        private readonly ICatalogueService _catalogueService;
        private readonly INavigationService _navigationService;
        private Technology _selectedTechnology;
        private ObservableCollection<Technology> _listTechnologies  ;
        
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
                    () => _navigationService.NavigateTo("SearchView")));
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
                        _navigationService.NavigateTo("PresentationView");
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
                    () => _navigationService.NavigateTo("RefClient")));
            }
        }
        private RelayCommand _partnerCommand;  
        public RelayCommand PartnerCommand
        {
            get
            {
                return _partnerCommand
                    ?? (_partnerCommand = new RelayCommand(
                    () => _navigationService.NavigateTo("PartnerView")));
            }
        }
        private RelayCommand _contactCommand;
        public RelayCommand ContactCommand
        {
            get
            {
                return _contactCommand
                    ?? (_contactCommand = new RelayCommand(
                    () => _navigationService.NavigateTo("ContactView")));
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
                            _navigationService.NavigateTo("Courses", tech.C_id);
                            
                        }));
            }
        }
        #endregion
        public MainViewModel(ICatalogueService catalogueService,INavigationService navigationService)
        {
            _catalogueService = catalogueService;
            _navigationService = navigationService;
            Initialisation();

        }

        private async void Initialisation()
        {
            try
            {
                ListTechnologies = new ObservableCollection<Technology>(await _catalogueService.GetAllTechnologies());               
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void Activate(Object parameter)
        {
            //throw new NotImplementedException();
           
        }

        public void Deactivate(object parameter)
        {
            //throw new NotImplementedException();
        }
        public void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
