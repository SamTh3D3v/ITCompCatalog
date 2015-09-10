using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using ITCompCatalogue.Helper;
using ITCompCatalogue.Model;
using ITCompCatalogue.View;

namespace ITCompCatalogue.ViewModel
{
    public class ReviewsViewModel : NavigableViewModelBase
    {
        #region Fields
        private CourReview _selectedReview; 
        private ObservableCollection<CourReview> _reviewsList;     
        private Cour _selectedCourse;
        private bool _isLoadingProgressRing;
        private Visibility _isNoReviewMessageVisible=Visibility.Collapsed;        
        #endregion
        #region Properties      
        public ObservableCollection<CourReview> ReviewsList
        {
            get
            {
                return _reviewsList;
            }

            set
            {
                if (_reviewsList == value)
                {
                    return;
                }

                _reviewsList = value;
                RaisePropertyChanged();
            }
        }                 
        public CourReview SelectedReview
        {
            get
            {
                return _selectedReview;
            }

            set
            {
                if (_selectedReview == value)
                {
                    return;
                }

                _selectedReview = value;
                RaisePropertyChanged();
            }
        }
        public Cour SelectedCourse
        {
            get
            {
                return _selectedCourse;
            }

            set
            {
                if (_selectedCourse == value)
                {
                    return;
                }

                _selectedCourse = value;
                RaisePropertyChanged();
            }
        }
        public bool IsLoadingProgressRing
        {
            get
            {
                return _isLoadingProgressRing;
            }

            set
            {
                if (_isLoadingProgressRing == value)
                {
                    return;
                }

                _isLoadingProgressRing = value;
                RaisePropertyChanged();
            }
        }
        public Visibility IsNoReviewMessageVisible
        {
            get
            {
                return _isNoReviewMessageVisible;
            }

            set
            {
                if (_isNoReviewMessageVisible == value)
                {
                    return;
                }

                _isNoReviewMessageVisible = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _addReviewCommand;
        public RelayCommand AddReviewCommand
        {
            get
            {
                return _addReviewCommand
                    ?? (_addReviewCommand = new RelayCommand(
                    () => (new NewReviewFlyout()).Show()));
            }
        }
        private RelayCommand _reviewsListLoadedCommand;
        public RelayCommand ReviewsListLoadedCommand
        {
            get
            {
                return _reviewsListLoadedCommand
                    ?? (_reviewsListLoadedCommand = new RelayCommand(
                    () =>
                    {                        
                        
                    }));
            }
        }
        
        #endregion
        #region Ctors and Methods
        public ReviewsViewModel(ICatalogueService catalogueService, INavigationService navigationService)
            : base(catalogueService, navigationService)
        {
            Messenger.Default.Register<long>(this, async (cId) =>
            {
                IsLoadingProgressRing = true;
                ReviewsList = new ObservableCollection<CourReview>(await CatalogueService.GetCourseReviewByCourseId(cId));
                IsLoadingProgressRing = false;
                IsNoReviewMessageVisible = (ReviewsList.Count > 0) ? Visibility.Collapsed : Visibility.Visible;                  
            });
        }              
        #endregion        
    }
}
