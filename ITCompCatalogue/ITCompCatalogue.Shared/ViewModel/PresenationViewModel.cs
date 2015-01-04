using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ITCompCatalogue.ViewModel
{
    class PresenationViewModel:ViewModelBase
    {
        #region Fields

        private INavigationService _navigationService;
        #endregion
        #region Properties
     

        private String  _presentationText ;

  
        public String PresentationText  
        {
            get
            {
                return _presentationText;
            }

            set
            {
                if ( _presentationText == value)
                {
                    return;
                }

                _presentationText = value;                
            }
        }
        #endregion
        #region Commands
        private RelayCommand _navigateToIndexCommand;
        public RelayCommand NavigateToIndexCommand
        {
            get
            {
                return _navigateToIndexCommand
                    ?? (_navigateToIndexCommand = new RelayCommand(
                    () => _navigationService.NavigateTo("MainPage")));
            }
        }
        #endregion
        #region Ctors and Methods
        public PresenationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //this needs to be places in a ressource xml file 
            PresentationText="ITComp est une société de service en ingénieurie informatique (SSII) à forte valeur ajoutée, en plein croissance avec un réseau"+
                " de compétences national et international important. Grace à une démarche de qualité innovante, ITComp represente une force de conseil, et d'intégration "+
                "de solution informatique, trés renommée en Algérie. ITComp fournit des services centrés autour de la formation, l'integration, le conseil et l'assistance technique. "+
                "La valeur ajoutée d'ITComp est la ressource humain. Cette ressource prend le temps de comprendre, prépare les conditions de travail afin de batir les solution "+
                "adéquatres aux besoins spécifiques des entreprises, puis intervient en mettant en oeuvre des nouvelles technologies au coeur du system d'information des clients ";
        }
        #endregion
    }
}
