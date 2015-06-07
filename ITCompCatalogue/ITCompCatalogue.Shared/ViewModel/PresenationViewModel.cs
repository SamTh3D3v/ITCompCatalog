using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    public class PresenationViewModel:NavigableViewModelBase
    {
        #region Fields       
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
        #endregion
        #region Ctors and Methods
        public PresenationViewModel(INavigationService navigationService,ICatalogueService catalogueService)
            :base(catalogueService,navigationService)
        {            
            //this needs to be places in a ressource xml file 
            PresentationText="ITComp est une société de service en ingénieurie informatique (SSII) à forte valeur ajoutée, en plein croissance avec un réseau"+
                " de compétences national et international important. Grace à une démarche de qualité innovante, ITComp represente une force de conseil, et d'intégration "+
                "de solution informatique, trés renommée en Algérie. ITComp fournit des services centrés autour de la formation, l'integration, le conseil et l'assistance technique. "+
                "La valeur ajoutée d'ITComp est la ressource humain. Cette ressource prend le temps de comprendre, prépare les conditions de travail afin de batir les solution "+
                "adéquatres aux besoins spécifiques des entreprises, puis intervient en mettant en oeuvre des nouvelles technologies au coeur du system d'information des clients ";
        }
        public override void Activate(object parameter)
        {
            base.Activate(parameter);
        }

        public override void Deactivate(object parameter)
        {
            base.Deactivate(parameter);
        }
        #endregion

       
    }
}
