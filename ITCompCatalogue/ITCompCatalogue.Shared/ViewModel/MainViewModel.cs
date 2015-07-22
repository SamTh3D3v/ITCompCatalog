using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Converters;
using ITCompCatalogue.Model;

using ITCompCatalogue.Helper;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;


namespace ITCompCatalogue.ViewModel
{
    public class MainViewModel : NavigableViewModelBase
    {      
        #region Fields     
        private Technology _selectedTechnology;
        private ObservableCollection<Technology> _listTechnologies ;
        private bool _searchIsEnabled = false;
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
        public bool SearchIsEnabled
        {
            get
            {
                return _searchIsEnabled;
            }

            set
            {
                if (_searchIsEnabled == value)
                {
                    return;
                }

                _searchIsEnabled = value;
                RaisePropertyChanged();
            }
        }       
        #endregion
        #region Commands
        private RelayCommand<Object> _searchCommand;
        public RelayCommand<Object> SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand<Object>(
                        (queryText) =>
                        {
                            NavigationService.NavigateTo("SearchView",queryText );
                        }));
            }
        }
        private RelayCommand _presentationCommand;   
        public RelayCommand PresenationCommand
        {
            get
            {
                return  _presentationCommand
                    ?? ( _presentationCommand = new RelayCommand(
                    () => NavigationService.NavigateTo("PresentationView")));
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
        private RelayCommand<Category> _selectCategotyCommand;
        public RelayCommand<Category> SelectCategotyCommand
        {
            get
            {
                return _selectCategotyCommand
                    ?? (_selectCategotyCommand = new RelayCommand<Category>(
                    (cat) =>
                    {
                        NavigationService.NavigateTo("CoursesOneCategoryListView", cat);
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
        private RelayCommand<ISuggestionQuery> _suggestionRequestCommand;
        public RelayCommand<ISuggestionQuery> SuggestionRequest
        {
            get
            {
                return _suggestionRequestCommand
                    ?? (_suggestionRequestCommand = new RelayCommand<ISuggestionQuery>(async (query) =>
                    {
                        IEnumerable<Cour> filteredQuery =await CatalogueService.SearchCourses(query.QueryText,null);
                        foreach (var cour in filteredQuery)
                        {
                            RandomAccessStreamReference stream;
                            switch (cour.Category.TechnologieID)
                            {
                                case 1:
                                     stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Android.png"));
                                    break;
                                case 2:
                                     stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Microsoft.png"));
                                    break;
                                case 7:
                                     stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/Oracle.png"));
                                    break;
                                default:
                                     stream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/General.png"));
                                    break;
                            }
                            query.Request.SearchSuggestionCollection.AppendResultSuggestion(cour.Code, cour.Intitule, cour.Category.TechnologieID.ToString(), stream, "Result"); 
                        }                    
                    }));
            }
        }
        private RelayCommand<SearchBoxResultSuggestionChosenEventArgs> _suggestionSelectedCommand;
        public RelayCommand<SearchBoxResultSuggestionChosenEventArgs> SuggestionSelectedCommand
        {
            get
            {
                return _suggestionSelectedCommand
                    ?? (_suggestionSelectedCommand = new RelayCommand<SearchBoxResultSuggestionChosenEventArgs>(
                    (args) => NavigationService.NavigateTo("CourDetails", CatalogueService.GetCourseByCourseId(long.Parse(args.Tag)))));
            }
        }
        private RelayCommand _pageLoadedCommand;
        public RelayCommand PageLoadedCommand
        {
            get
            {
                return _pageLoadedCommand
                    ?? (_pageLoadedCommand = new RelayCommand(
                    () =>
                    {
                        SearchIsEnabled = true;
                    }));
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

        

        public override void Deactivate(object parameter)
        {
            SearchIsEnabled = false;
        }
        public override void GoBack()
        {
            NavigationService.GoBack();
        }
    }
}
