﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using ITCompCatalogue.View;
using SQLitePCL;

namespace ITCompCatalogue
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif
        public static bool IsListViewSelected { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
           UnhandledException += (sender, e) => e.Handled = true;
        }
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandRequested;
        }

        private void OnCommandRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var presSettings = new SettingsCommand("ItCompPresentation", "ITComp Presentation", handler =>
            {
                //(Window.Current.Content as Frame).Navigate(typeof(PresentationView));
                new PresentationSettingsFlyout().Show();
            });
            var contactsSettings = new SettingsCommand("contactsSettings", "Contacts", handler =>
            {
                //(Window.Current.Content as Frame).Navigate(typeof(ContactView));
                new ContactSettingsFlyout().Show();
            });
            var aboutSettings = new SettingsCommand("about ", "About ", handler => new AboutSettingsFlyout().Show());
            var privacySettings = new SettingsCommand("PrivacyPolicy ", "Privacy Policy ", handler => new PrivacyPolicySettingsFlyout().Show());
            args.Request.ApplicationCommands.Add(presSettings);
            args.Request.ApplicationCommands.Add(contactsSettings);
            args.Request.ApplicationCommands.Add(privacySettings);
            args.Request.ApplicationCommands.Add(aboutSettings);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;
            await CopyDatabase();
            await CopyFavorateDatabase();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {

                var tileId = e.TileId;
                var tileArgs = e.Arguments;
                if (tileArgs == "CourDetails")
                {
                    if (!rootFrame.Navigate(typeof(CourseDetailsView), tileId))
                    {
                        throw new Exception("Failed to load Course details");
                    }

                }
                else if (tileArgs == "ScheduleCour")
                {
                    if (!rootFrame.Navigate(typeof(ScheduleView), tileId))
                    {
                        throw new Exception("Failed to load Course Schedule");
                    }
                    
                }
                

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                else if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        private async Task CopyDatabase()
        {
            bool isDatabaseExisting = false;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("ITCompTrainingDB.db");
                isDatabaseExisting = true;
                // CreateFavoriteTable();
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(@"Data\ITCompTrainingDB.db");
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
                //CreateFavoriteTable();
            }

        }

        private async Task CopyFavorateDatabase()
        {
            bool isDatabaseExisting = false;
            try
            {
                Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;

                StorageFile storageFile = await roamingFolder.GetFileAsync("ITCompFavoritesDB.db");
                isDatabaseExisting = true;
                // CreateFavoriteTable();
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(@"Data\ITCompFavoritesDB.db");
                await databaseFile.CopyAsync(ApplicationData.Current.RoamingFolder);
                //CreateFavoriteTable();
            }

        }


        private void CreateFavoriteTable()
        {
            var connection = new SQLiteConnection("ITCompTrainingDB.db");
            using (var statement = connection.Prepare(@"CREATE TABLE IF NOT EXISTS Favorite (
                                                       _id long NOT NULL PRIMARY KEY)"))
            {
                statement.Step();
            }
        }

        //private async void CreateRoamedDataBase()
        //{
        //    Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        //    StorageFile dbFile = await roamingFolder.CreateFileAsync("FavoritesDB.db",CreationCollisionOption.ReplaceExisting);
        //    await FileIO.WriteTextAsync(sampleFile, formatter.Format(DateTime.Now));



        //}
        public static bool IsConnectedToInternet()
        {
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }
    }
}