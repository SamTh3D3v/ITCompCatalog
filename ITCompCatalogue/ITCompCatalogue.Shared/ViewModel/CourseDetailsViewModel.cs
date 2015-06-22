﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class CourseDetailsViewModel : NavigableViewModelBase
    {
        #region Fields
        private Cour _courseDetails;
        private bool _isCourseFavorite;
        private TypedEventHandler<DataTransferManager, DataRequestedEventArgs> _shareHandler;
        private DataTransferManager _dataTransferManager;
        private bool _bottomAppBarIsOpen;
        private Visibility _isDatesVisible;
        #endregion
        #region Properties
        public Visibility IsDatesVisible
        {
            get
            {
                return _isDatesVisible;
            }

            set
            {
                if (_isDatesVisible == value)
                {
                    return;
                }

                _isDatesVisible = value;
                RaisePropertyChanged();
            }
        }
        public bool BottomAppBarIsOpen
        {
            get
            {
                return _bottomAppBarIsOpen;
            }

            set
            {
                if (_bottomAppBarIsOpen == value)
                {
                    return;
                }

                _bottomAppBarIsOpen = value;
                RaisePropertyChanged();
            }
        }
        public Cour CourseDetails
        {
            get
            {
                return _courseDetails;
            }

            set
            {
                if (_courseDetails == value)
                {
                    return;
                }

                _courseDetails = value;
                RaisePropertyChanged();
            }
        }
        public bool IsCourseFavorite
        {
            get
            {
                return _isCourseFavorite;
            }

            set
            {
                if (_isCourseFavorite == value)
                {
                    return;
                }

                _isCourseFavorite = value;
                RaisePropertyChanged();
                if (!_isCourseFavorite)
                {
                    CatalogueService.UnFavoriteCourse(CourseDetails.C_id);
                }
                else
                {
                    CatalogueService.FavoriteCourse(_courseDetails.C_id);
                }
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
                        () =>
                        {
                            NavigationService.NavigateTo("SearchView");
                        }));
            }
        }
        private RelayCommand _favoriteCommand;
        public RelayCommand FavoriteCommand
        {
            get
            {
                return _favoriteCommand
                    ?? (_favoriteCommand = new RelayCommand(
                    () =>
                    {
                        IsCourseFavorite = true;

                    }));
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
        private RelayCommand<Cour> _goToScheduleCommand;
        public RelayCommand<Cour> GoToScheduleCommand
        {
            get
            {
                return _goToScheduleCommand
                    ?? (_goToScheduleCommand = new RelayCommand<Cour>(
                    (cour) => NavigationService.NavigateTo("ScheduleView", cour)));
            }
        }
        private RelayCommand _shareCommand;
        public RelayCommand ShareCommand
        {
            get
            {
                return _shareCommand
                    ?? (_shareCommand = new RelayCommand(
                    DataTransferManager.ShowShareUI));
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
        private RelayCommand _pinCommand;
        public RelayCommand PinCommand
        {
            get
            {
                return _pinCommand
                    ?? (_pinCommand = new RelayCommand(
                    PinSecondaryTile));
            }
        }
        private RelayCommand _unpinCommand;
        public RelayCommand UnpinCommand
        {
            get
            {
                return _unpinCommand
                    ?? (_unpinCommand = new RelayCommand(
                    UnpinSecondaryTile));
            }
        }
        #endregion
        #region Ctors and methods

        public CourseDetailsViewModel(INavigationService navigationService, ICatalogueService catalogueService)
            : base(catalogueService, navigationService)
        {
            IsDatesVisible = App.IsConnectedToInternet() ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        public override void Activate(object parameter)
        {
            CourseDetails = (parameter as Cour);
            if (CourseDetails != null)
            {
                IsCourseFavorite = CatalogueService.IsCourseFavorite(CourseDetails.C_id);

            }
            else
            {
                CourseDetails = CatalogueService.GetCourseByCourseId(long.Parse(parameter.ToString()));
                IsCourseFavorite = CatalogueService.IsCourseFavorite(CourseDetails.C_id);
            }            
            RegisterForShare();
            SendLiveTileUpdate();
            Network.InternetConnectionChanged += async (s, e) => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IsDatesVisible = (e.IsConnected ? Visibility.Visible : Visibility.Collapsed);
            });
        }

        public override void Deactivate(object parameter)
        {
            _dataTransferManager.DataRequested -= _shareHandler;
        }
        private void RegisterForShare()
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _shareHandler = new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareTextHandler);
            _dataTransferManager.DataRequested += _shareHandler;
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            // The Title is mandatory
            request.Data.Properties.Title = CourseDetails.Code + ": " + CourseDetails.Intitule;
            request.Data.Properties.Description = "";
            request.Data.SetText(CourseDetails.Description + "\n\n Category: " + CourseDetails.Category.Intitule);
        }
        private void SendLiveTileUpdate()
        {

            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText02);

            var tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = CourseDetails.Code;
            tileTextAttributes[1].InnerText = CourseDetails.Intitule;

            var tileImageAttributes = tileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/Logo150X150.png");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "ITComp Logo");

            var sqTileBinding = (XmlElement)tileXml.GetElementsByTagName("binding").Item(0);
            if (sqTileBinding != null) sqTileBinding.SetAttribute("branding", "none");


            var wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideSmallImageAndText04);

            var wideTileTextAttributes = wideTileXml.GetElementsByTagName("text");
            wideTileTextAttributes[0].AppendChild(wideTileXml.CreateTextNode(this.CourseDetails.Code));
            wideTileTextAttributes[1].AppendChild(wideTileXml.CreateTextNode(this.CourseDetails.Intitule));

            var wideTileImageAttributes = wideTileXml.GetElementsByTagName("image");
            ((XmlElement)wideTileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/Logo150X150.png");
            ((XmlElement)wideTileImageAttributes[0]).SetAttribute("alt", "ITComp Logo");

            var wideTileBinding = (XmlElement)wideTileXml.GetElementsByTagName("binding").Item(0);
            if (wideTileBinding != null) wideTileBinding.SetAttribute("branding", "none");


            var node = tileXml.ImportNode(wideTileXml.GetElementsByTagName("binding").Item(0), true);
            var xmlNode = tileXml.GetElementsByTagName("visual").Item(0);
            if (xmlNode != null)
                xmlNode.AppendChild(node);


            var tileNotification = new TileNotification(tileXml) { ExpirationTime = DateTimeOffset.UtcNow.AddDays(1) };
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
        private async void PinSecondaryTile()
        {
            BottomAppBarIsOpen = true;

            var uriLogo = new Uri("ms-appx:///Assets/Logo150X150.png");
            var uriSmallLogo = new Uri("ms-appx:///Assets/Logo56X56.png");
            const string tileActivationArguments = "CourDetails";
            var tile = new SecondaryTile(CourseDetails.C_id.ToString(),
                                                               CourseDetails.Code,
                                                                CourseDetails.Intitule,
                                                                tileActivationArguments,
                                                                TileOptions.ShowNameOnLogo,
                                                                uriLogo)
            {
                ForegroundText = ForegroundText.Dark,
                SmallLogo = uriSmallLogo
            };
            tile.RequestCreateAsync();
            BottomAppBarIsOpen = false;
        }
        private async void UnpinSecondaryTile()
        {
            BottomAppBarIsOpen = true;

            if (SecondaryTile.Exists(CourseDetails.C_id.ToString()))
            {
                var tile = new SecondaryTile(CourseDetails.C_id.ToString());
                tile.RequestDeleteAsync();
            }
            BottomAppBarIsOpen = false;
        }

    }
}
