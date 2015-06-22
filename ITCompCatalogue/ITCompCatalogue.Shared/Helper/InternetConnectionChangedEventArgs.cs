using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Helper
{
    public class InternetConnectionChangedEventArgs:EventArgs
    {
        public InternetConnectionChangedEventArgs(bool isConnected)
        {
            _isConnected = isConnected;
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
        }
    }
}
