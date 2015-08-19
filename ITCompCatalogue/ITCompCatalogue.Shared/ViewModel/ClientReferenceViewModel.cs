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
    public class ClientReferenceViewModel:NavigableViewModelBase
    {
        #region Fields
        private ObservableCollection<CategoryClient> _listCategoryClient;
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
        public ObservableCollection<CategoryClient> ListCategoryClient
        {
            get
            {
                return _listCategoryClient;
            }

            set
            {
                if (_listCategoryClient == value)
                {
                    return;
                }

                _listCategoryClient = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
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
        #endregion
        #region Ctors and Methods

        public ClientReferenceViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            :base(catalogueService,navigationService)
        {                            
            LoadListCategoryClient();
        }

        private void LoadListCategoryClient()
        {
            //this load must be done async from a local storage xml file
            
            ListCategoryClient=new ObservableCollection<CategoryClient>()
            {
                new CategoryClient()
                {
                    NomCategory ="Finances" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/finance.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Axa Assurances Algérie Vie",
                        "CAAT",
                        "Caisse Nationale des Assurances Sociales (CNAS)",
                        "Compagnie Algerienne D'Assurance et de Reassurance (CAAR)",
                        "Compagnie Centrale de Reassurance (CCR)",
                        "Compagnie Internationale d'Assurance et de Réassurance (CIAR)",
                        "TRUST ALGERIA Assurances et Réassurance",
                        "Arab Banking Corporation Algeria",
                        "BADR",
                        "Banque Africaine de Développement",
                        "Banque d'Algérie",
                        "Banque Extérieure d'Algérie BEA",
                        "Banque Nationale d'Algérie SPA",
                        "Banque de Développement Local",
                        "Centre de Précompensation Interbancaire (C.P.I.SPA)",
                        "CPA Direction Monétique",
                        "Gulf Bank Algéria (AGB)",
                        "Société Générale Algérie",
                        "Trust Bank Algeria",
                        "CACOBATPH",
                        "Caisse des Retraites Militaires",
                        "Caisse Militaires de Sécurité Sociale et de Prévoyance (CAMSSP)",
                        "Caisse Nationale de Logements (CNL)",
                        "Caisse Nationale des Retraités",
                        "CASNOS",
                        "Fonds National de Perequation des Oeuvres Sociales (FNPOS)",
                        "... etc"
                        
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Energie et mines" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/energy.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Entreprise Nationale de Forage",
                        "Entreprise Nationale de Grands Travaux Pétroliers (ENGTP)",
                        "First Calgary Petroleum's LP-Association SONATRACH SH-FCP LP",
                        "Groupement Berkine SH-AAC",
                        "Groupement Timimoun",                        
                        "Halliburton Energy Services",
                        "L'Agence Nationale de Géologie et du controle Minier (ANGCM)",
                        "NAFTAL SPA",
                        "Ourhoud",
                        "SONATRACH Act AMONT",
                        "SONATRACH ACTIVITE COMMERCIALISATION",
                        "SONATRACH Direction Centrale Informatique et Système d'Information (DC-ISI)",
                        "SONATRACH PED ",
                        "SONATRACH BHP Billiton Ohanet ",                        
                        "... etc",
                        
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Ecoles" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/ecoles.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Ecole Nationale d'Administration",
                        "Ecole Supérieur de Magistrature",
                        "Ecole Supérieur du Commerce",
                        "... etc",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Etranger" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/etranger.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "ABB",
                        "Aerow SAS",
                        "Ambassade du Rouyaume Unis",
                        "Djazair Port World SPA",                                                
                        "PNUD Algérie",                                                
                        "World Learning", 
                        "... etc"                       
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Gouvernement et administration" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/governement.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Agence Nationale de l'Emploi (ANEM)",
                        "Agence Nationale de Soutien a l'Emploi des Jeunes (ANSEJ)",
                        "ALGEX Agence Nationale de la Promotion du Commerce Exterieur",
                        "Centre National du Registre du Commerce",                                                
                        "Chambre Algerienne de Commerce et d'Industrie (CACI)",                                                
                        "Ministère de la défence Nationale",  
                        "Ministère de la Justice",                                                
                        "Ministère de l'Habitat de l'Urbanisme et de la ville",                                                
                        "Ministère de l'Intérieur et Des Collectivités locales",                                                
                        "Ministère des Finances (DGI)",                                                
                        "Ministère du Commerce",                                                
                        "Présidence de la République",                                                
                        "SENAT Majlis El Ouma",                                                
                        "Société des Eaux et de l'Assainissement d'Alger (SEAAL)",  
                        "... etc"                      
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Industrie" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/industrie.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "AMENHYD SPA",
                        "APAVE Algérie",
                        "Cevital Bejaia SPA",
                        "Ciment Factory of hadjar-soud",                                                
                        "CocaCola Fruital",                                                
                        "CR METAL",  
                        "DANONE DJURDJURA",                                                
                        "El kendi Industrie du Médicament",                                                
                        "GROUPE INDUSTRIEL SAIDAL",                                                
                        "GROUPE INDUSTRIEL SIDER SPA",                                                
                        "Rouiba NCA-ROUIBA-SPA",                                                
                        "SARL GSIPH",                                                
                        "SARL Sétifis Viande",                                                
                        "Société Algeriènne de Réalisation de Projet Industriel  (SARPI)", 
                        "SPA HydraPharm",
                        "STAR BRANDS SPA/STAR GOODS SPA",
                        "TAIBA Food Company",
                        "... etc"
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Télécommunication" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/telecommunication.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Algérie Poste",
                        "Algérie Télécom ",
                        "ATM MOBILIS ",
                        "ATS Algeria",                                                
                        "Ericsson",                                                
                        "Orascom Telécom Algérie",  
                        "Ooredoo Télécom Algérie",                                                
                        "ARPT (L'Autorité de Régulation de la Poste et des Télécommunication)",                                                
                        "... etc",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Transport" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/transport.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aéroport d'Alger",
                        "BMT (Bejaia Medetéranean Terminal)",
                        "HYPROC SHIPPING COMPANY",
                        "Maersk Algeria SPA",                                                
                        "Port de Béjaia",                                                
                        "SETRAM SPA",  
                        "Société d'exploitation des Tramways",                                                
                        "Société de Gestion des Services et Infrastructures Aéorpotuaires (SGSIA)",                                                
                        "Société Nationale de Transport Routier SNTR", 
                        "Toyota Algérie",
                        "... etc"
                    },
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
