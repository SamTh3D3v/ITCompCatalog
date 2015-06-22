using System;
using System.Collections.Generic;
using System.Text;
using Windows.Networking.Connectivity;

namespace ITCompCatalogue.Helper
{
    public static class Network
    {
        public static event EventHandler<InternetConnectionChangedEventArgs> InternetConnectionChanged;

        static Network()
        {
            NetworkInformation.NetworkStatusChanged += (s) =>
            {
                if (InternetConnectionChanged != null)
                {
                    var arg = new InternetConnectionChangedEventArgs(IsConnected);
                    InternetConnectionChanged(null, arg);
                }
            };
        }
        public static bool IsConnected
        {
            get
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                var isConnected = (profile != null
                    && profile.GetNetworkConnectivityLevel() ==
                    NetworkConnectivityLevel.InternetAccess);
                return isConnected;
            }
        }
    }
}
