using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Helper
{
    public interface INavigable
    {
        void Activate(object parameter);
        void Deactivate(object parameter);                
        void GoBack();
    }
}
