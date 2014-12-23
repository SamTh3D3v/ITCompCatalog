using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;


using Microsoft.Practices.ServiceLocation;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;
using NavigationService = GalaSoft.MvvmLight.Views.NavigationService;


namespace ITCompCatalogue.ViewModel
{
    class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<INavigationService, Design.DesignNavigationService>();
                SimpleIoc.Default.Register<ICatalogueService, CatalogueService>();
            }
            else
            {
                SimpleIoc.Default.Register<ICatalogueService, CatalogueService>();
              //  SimpleIoc.Default.Register<INavigationService>(()=>new NavigationService());             
            }
            
            SimpleIoc.Default.Register<MainViewModel>();
        }
        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            //navigationService.Configure("Courses", typeof());            
            return navigationService;
        }
    }
}
