using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class PartenaireViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Partenaire> _listPartenaires;
        #endregion
        #region Properties
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

        #endregion
        #region Ctors and Methods

        public PartenaireViewModel()
        {
            ListPartenaires = new ObservableCollection<Partenaire>()
            {                
                new Partenaire()
                {
                    Nom = "Mirosoft",
                    LogoImageSource = "../Images/Mirosoft.png",
                    WebSite = "www.Mirosoft.com",
                    Description = "ITComp a acquis le domain d'expertises de niveau GOLD" +
                                  "ITComp est un centre MCPLS (Microsoft Certified Partner for Learning Solution) de niveau GOLD "
                },
                new Partenaire()
                {
                    Nom = "Oracle",
                    LogoImageSource = "../Images/Mirosoft.png",
                    WebSite = "www.Mirosoft.com",
                    Description = "ITComp a acquis le domain d'expertises de niveau GOLD" +
                                  "pour l'integration des technologies Oracle"
                },
                new Partenaire()
                {
                    Nom = "Prometric",
                    LogoImageSource = "../Images/Prometric.png",
                    WebSite = "www.Prometric.com",
                    Description = "ITComp est un centre de certafication officiel PROMETRIC. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement 3856"
                },
                new Partenaire()
                {
                    Nom = "CertiPort",
                    LogoImageSource = "../Images/CertiPort.png",
                    WebSite = "www.CertiPort.com",
                    Description = "ITComp est un centre de certafication officiel CertiPort. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement ----"
                },
                new Partenaire()
                {
                    Nom = "PearsonVue",
                    LogoImageSource = "../Images/PearsonVue.png",
                    WebSite = "www.CertiPort.com",
                    Description = "ITComp est un centre de certafication officiel PearsonVue. ITComp est aussi un centre de formation professionnelle" +
                                  " agrée par l'état sous le numéro d'enregistrement ----"
                },
                new Partenaire()
                {
                    Nom = "Android",
                    LogoImageSource = "../Images/AndroidATC.png",
                    WebSite = "www.Android.com",
                    Description = "ITComp est un centre ATC (Android Advenced Training Autorized Center)"
                }               
            };
        }
        #endregion
    }
}
