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
            SimpleIoc.Default.Register<ClientReferenceViewModel>();
            SimpleIoc.Default.Register<ContactViewModel>();
            SimpleIoc.Default.Register<PartenaireViewModel>();
            SimpleIoc.Default.Register<PresenationViewModel>();
            SimpleIoc.Default.Register<FavoriteCoursesViewModel>();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
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
        public ClientReferenceViewModel ClientReferenceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ClientReferenceViewModel>();
            }
        }
      
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PresenationViewModel PresenationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PresenationViewModel>();
            }
        }
     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FavoriteCoursesViewModel FavoriteCoursesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FavoriteCoursesViewModel>();
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PartenaireViewModel PartenaireViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PartenaireViewModel>();
            }
        }
  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ContactViewModel ContactViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ContactViewModel>();
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
            navigationService.Configure("SearchView",typeof(SearchView));
            navigationService.Configure("ContactView", typeof(ContactView));
            navigationService.Configure("PartnerView", typeof(PartenairesView));
            navigationService.Configure("RefClient", typeof(ClientReferencesView));
            navigationService.Configure("PresentationView", typeof(PresentationView));
            navigationService.Configure("FavoriteCoursesView", typeof(FavoriteCoursesView));
            navigationService.Configure("MainPage", typeof(MainPage));
            
            return navigationService;
        }
    }
}
