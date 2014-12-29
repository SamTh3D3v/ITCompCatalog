using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using ITCompCatalogue.Model;

namespace ITCompCatalogue.ViewModel
{
    class ClientReferenceViewModel:ViewModelBase
    {
        #region Fields
        private ObservableCollection<CategoryClient> _listCategoryClient;
        #endregion
        #region Properties
        public ObservableCollection<CategoryClient> ListCategoryClient
        {
            get
            {
                return _listCategoryClient;
            }

            set
            {
                if (_listCategoryClient == value)
                {
                    return;
                }

                _listCategoryClient = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands

        #endregion
        #region Ctors and Methods

        public ClientReferenceViewModel()
        {
                //this load must be done async from a local storage xml file
            LoadListCategoryClient();
        }

        private void LoadListCategoryClient()
        {
            //this load must be done async from a local storage xml file
            
            ListCategoryClient=new ObservableCollection<CategoryClient>()
            {
                new CategoryClient()
                {
                    NomCategory ="Finances" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/finance.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances AssurancesAssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurancesAssurances",
                        "Aliance AssurancesAssurances",
                        "Aliance AssurancesAssurances",
                        "Aliance AssurancesAssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Energie et mines" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/energy.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances AssurancesAssurancesAssurances",                        
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurancesAssurances",
                        "Aliance AssurancesAssurances",
                        "Aliance AssurancesAssurances",
                        "Aliance AssurancesAssurancesAssurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Ecoles" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/ecoles.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Etranger" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/etranger.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Gouvernement et administration" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/governement.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",  
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Industrie" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/industrie.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",  
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Télécommunication" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/telecommunication.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",  
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",                                                
                    },
                },
                 new CategoryClient()
                {
                    NomCategory ="Transport" ,
                    LogoCategoryImage ="../Images/CategoryRefClients/transport.png" ,
                    ListClien = new ObservableCollection<string>()
                    {
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance Assurances",
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",  
                        "Aliance AssurancesAssurances",                                                
                        "Aliance Assurances",                                                
                        "Aliance Assurances",                                                
                    },
                }

            };
        }
        #endregion
    }
}
