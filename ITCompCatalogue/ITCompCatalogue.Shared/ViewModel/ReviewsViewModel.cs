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
        private long _selectedCourseId;
        private bool _isLoadingProgressRing;
        private Visibility _isNoReviewMessageVisible=Visibility.Collapsed;
        private CourReview _newReview=new CourReview();
        #endregion
        #region Properties        
        public CourReview NewReview
        {
            get
            {
                return _newReview;
            }
            set
            {
                if (_newReview == value)
                {
                    return;
                }

                _newReview = value;
                RaisePropertyChanged();
            }
        }
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
        public long SelectedCourseId
        {
            get
            {
                return _selectedCourseId;
            }

            set
            {
                if (_selectedCourseId == value)
                {
                    return;
                }

                _selectedCourseId = value;
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
        private RelayCommand _submitRelayCommand;   
        public RelayCommand SubmitRelayCommand
        {
            get
            {
                return _submitRelayCommand
                    ?? (_submitRelayCommand = new RelayCommand(
                    () =>
                    {
                        NewReview.CourId = SelectedCourseId;
                        CatalogueService.AddCourseReviewByCourseId(NewReview);
                        NewReview=new CourReview()
                        {
                            CourId = SelectedCourseId
                        };
                        (new ReviewsListFlyout()).Show();

                    }));
            }
        }
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
                SelectedCourseId = cId;
                IsLoadingProgressRing = true;
                ReviewsList = new ObservableCollection<CourReview>(await CatalogueService.GetCourseReviewByCourseId(cId));
                IsLoadingProgressRing = false;
                IsNoReviewMessageVisible = (ReviewsList.Count > 0) ? Visibility.Collapsed : Visibility.Visible;                  
            });
        }              
        #endregion        
    }
}
