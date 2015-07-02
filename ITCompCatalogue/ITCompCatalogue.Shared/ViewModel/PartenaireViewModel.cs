using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Converters;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class PartenaireViewModel : NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<Partenaire> _listPartenaires;
        private bool _searchIsEnabled = false;
        #endregion
        #region Properties
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
        public ObservableCollection<Partenaire> ListPartenaires
        {
            get
            {
                return _listPartenaires;
            }

            set
            {
                if (_listPartenaires == value)
                {
                    return;
                }

                _listPartenaires = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
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
        private RelayCommand _gobackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return _gobackCommand
                    ?? (_gobackCommand = new RelayCommand(
                    () => NavigationService.GoBack()));
            }
        }
        private RelayCommand<Object> _searchCommand;
        public RelayCommand<Object> SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand<Object>(
                    (queryText) => NavigationService.NavigateTo("SearchView", queryText)));
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
                        IEnumerable<Cour> filteredQuery = await CatalogueService.SearchCourses(query.QueryText, null);
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
        #region Ctors and Methods

        public PartenaireViewModel(INavigationService navigationService,ICatalogueService catalogueService)
            :base(catalogueService,navigationService)
        {            
            ListPartenaires = new ObservableCollection<Partenaire>()
            {  
                new Partenaire()
                {
                    Nom = "Oracle",
                    LogoImageSource = "../Images/Partners/oracle_partner.png",
                    WebSite = "www.Oracle.com",
                    Description = "ITComp a acquis le domain d'expertises de niveau GOLD " +
                                  "pour l'integration des technologies Oracle"
                },
                new Partenaire()
                {
                    Nom = "Mirosoft",
                    LogoImageSource = "../Images/Partners/microsoft_partner.png",
                    WebSite = "www.Mirosoft.com",
                    Description = "ITComp a acquis le domain d'expertises de niveau GOLD" +
                                  "ITComp est un centre MCPLS (Microsoft Certified Partner for Learning Solution) de niveau GOLD "
                },
                
                new Partenaire()
                {
                    Nom = "Prometric",
                    LogoImageSource = "../Images/Partners/prometric_partner.png",
                    WebSite = "www.Prometric.com",
                    Description = "ITComp est un centre de certafication officiel PROMETRIC. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement 3856"
                },
                new Partenaire()
                {
                    Nom = "CertiPort",
                    LogoImageSource = "../Images/Partners/certiport_partner.png",
                    WebSite = "www.CertiPort.com",
                    Description = "ITComp est un centre de certafication officiel CertiPort. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement ----"
                },
                new Partenaire()
                {
                    Nom = "PearsonVue",
                    LogoImageSource = "../Images/Partners/pearsonvue_partner.png",
                    WebSite = "www.CertiPort.com",
                    Description = "ITComp est un centre de certafication officiel PearsonVue. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement ----"
                },
                new Partenaire()
                {
                    Nom = "Android",
                    LogoImageSource = "../Images/Partners/android_partner.png",
                    WebSite = "www.Android.com",
                    Description = "ITComp est un centre ATC (Android Advenced Training Autorized Center)"
                }               
            };
        }
        public override void Deactivate(object parameter)
        {
            SearchIsEnabled = false;
        }
        #endregion
    }
}
