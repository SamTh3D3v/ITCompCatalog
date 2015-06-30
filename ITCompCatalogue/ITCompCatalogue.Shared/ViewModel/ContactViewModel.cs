using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Converters;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class ContactViewModel : NavigableViewModelBase
    {
        #region Fields
        private bool _searchIsEnabled = false;
        private String _phoneNumber = "+213 (0) 21 56 32 33";
        private String _faxNumber = "+213 (0) 21 56 18 26";
        private String _email = "Lazhar.Guendouz@ITComp-dz.com";
        private String _altEmail = "Radia.Lamari@ITComp-dz.com";
        private String _siteWeb = "http://www.itcomp-dz.com";
        private String _adress = "10, Rue Khoudjat Eldjeld, Bir Mourad Rais Alger. Algérie.";

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
        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                if (_phoneNumber == value)
                {
                    return;
                }

                _phoneNumber = value;
                RaisePropertyChanged();
            }
        }
        public String FaxNumber
        {
            get
            {
                return _faxNumber;
            }

            set
            {
                if (_faxNumber == value)
                {
                    return;
                }

                _faxNumber = value;
                RaisePropertyChanged();
            }
        }
        public String Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email == value)
                {
                    return;
                }

                _email = value;
                RaisePropertyChanged();
            }
        }
        public String SiteWeb
        {
            get
            {
                return _siteWeb;
            }

            set
            {
                if (_siteWeb == value)
                {
                    return;
                }

                _siteWeb = value;
                RaisePropertyChanged();
            }
        }
        public String Adress
        {
            get
            {
                return _adress;
            }

            set
            {
                if (_adress == value)
                {
                    return;
                }

                _adress = value;
                RaisePropertyChanged();
            }
        }
        public String AltEmail
        {
            get
            {
                return _altEmail;
            }

            set
            {
                if (_altEmail == value)
                {
                    return;
                }

                _altEmail = value;
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
        private RelayCommand<String> _searchCommand;
        public RelayCommand<String> SearchCommand
        {
            get
            {
                return _searchCommand
                    ?? (_searchCommand = new RelayCommand<String>(
                    (queryText) => NavigationService.NavigateTo("SearchView", queryText)));
            }
        }
        private RelayCommand<String> _callCommand;
        public RelayCommand<String> CallCommand
        {
            get
            {
                return _callCommand
                    ?? (_callCommand = new RelayCommand<String>(
                        (phoneNumber) =>
                        {
#if WINDOWS_PHONE_APP
                            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(phoneNumber, "ITComp");
#endif
                        }));
            }
        }
        private RelayCommand<String> _navigateToWebSiteCommand;


        public RelayCommand<String> NavigateToWebSiteCommand
        {
            get
            {
                return _navigateToWebSiteCommand
                    ?? (_navigateToWebSiteCommand = new RelayCommand<String>(async (site) => await Windows.System.Launcher.LaunchUriAsync(new Uri(site))));
            }
        }
        private RelayCommand<String> _sendEmailCommand;

        public RelayCommand<String> SendEmailCommand
        {
            get
            {
                return _sendEmailCommand
                    ?? (_sendEmailCommand = new RelayCommand<String>(async (mail) =>
                    {
                        await Launcher.LaunchUriAsync(new Uri("mailto:" + mail));

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
        public ContactViewModel(INavigationService navigationService, ICatalogueService catalogueService)
            : base(catalogueService, navigationService)
        {

        }
        public override void Deactivate(object parameter)
        {
            SearchIsEnabled = false;
        }
        #endregion
    }
}
