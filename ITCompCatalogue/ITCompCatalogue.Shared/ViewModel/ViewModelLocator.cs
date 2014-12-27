using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Model;
using ITCompCatalogue.View;
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
            SimpleIoc.Default.Register<CoursesCategoriesListViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<CourseDetailsViewModel>();

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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CoursesCategoriesListViewModel CoursesCategoriesListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CoursesCategoriesListViewModel>();
            }
        }
     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CourseDetailsViewModel CourseDetailsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CourseDetailsViewModel>();
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SearchViewModel SearchViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }
        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Courses", typeof(CoursesCategoiesListView));   
            navigationService.Configure("CourDetails",typeof(CourseDetailsView));
            return navigationService;
        }
    }
}
