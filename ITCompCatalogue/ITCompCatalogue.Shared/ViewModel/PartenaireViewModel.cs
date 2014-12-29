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
                    Nom = "Oracle",
                    LogoImageSource = "../Images/oracle_partner.png",
                    WebSite = "www.Oracle.com",
                    Description = "ITComp a acquis le domain d'expertises de niveau GOLD" +
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
        #endregion
    }
}
