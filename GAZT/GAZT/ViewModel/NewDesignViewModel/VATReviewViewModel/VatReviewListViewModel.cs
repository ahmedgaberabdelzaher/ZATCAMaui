using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel
{
    public class VatReviewListViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand SelectionGoBackClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand NewRequestBtnTapped { get; set; }

        private int selectedFilter = (int) FilterOptions.All;
        
        enum FilterOptions
        {
            All,
            Objections,
            VatReviews
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        
        private bool _isBackButtonVisible = false;
        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }
        
        private bool _isVatListVisible = false;
        public bool IsVatListVisible
        {
            get { return _isVatListVisible; }
            set
            {
                _isVatListVisible = value;
                RaisePropertyChanged("IsVatListVisible");
            }
        }
        
        private bool _summaryVisible = false;
        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }
        
        private string _filteredSelectionName = "";
        public string FilteredSelectionName
        {
            get { return _filteredSelectionName; }
            set
            {
                _filteredSelectionName = value;
                RaisePropertyChanged("FilteredSelectionName");
            }
        }
        
        private string _numberOfObjAndReviews = "";
        public string NumberOfObjAndReviews
        {
            get { return _numberOfObjAndReviews; }
            set
            {
                _numberOfObjAndReviews = value;
                RaisePropertyChanged("NumberOfObjAndReviews");
            }
        }
        
        public class SelectionModel
        {
            public SelectionModel()
            {
            }
            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
        }
        
        public ObservableCollection<SelectionModel> selectionOptions { get; set; }
        public ObservableCollection<SelectionModel> SelectionOptions
        {
            get
            {
                return selectionOptions;
            }

            set
            {
                if (selectionOptions == value)
                {
                    return;
                }
                selectionOptions = value;
                RaisePropertyChanged("SelectionOptions");
            }
        }

        public VatReviewListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            _dialogService = dialogService;
            
            GoBackClick = new Command(async () =>
            {
                EnableListView();
            });
            SelectionGoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            NewRequestBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.VatReviewPageView);
            });
            
        }
        
        public void EnableSummaryView()
        {
            IsBackButtonVisible = true;
            SummaryVisible = true;
            IsVatListVisible = false;
        }
        
        public void EnableListView()
        {
            IsBackButtonVisible = false;
            SummaryVisible = false;
            IsVatListVisible = true;
        }
        
        public void ResetSelectionData()
        {
            IsLoading = false;
            //AddOutletDecisionOptions();
        }
        
        public async void ShowVatReviewPage()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new VatReviewPageView());
            _navigationService.NavigateTo(App.VatReviewPageView);
        }
        
        public void ResetListData()
        {
            IsLoading = false;
            SetFilterOptions((int) FilterOptions.All);
            NumberOfObjAndReviews = "0 "+AppResources.VRObjectionsAndReviews;
            EnableListView();
        }
        
        private void SetFilterOptions(int selectedFilter)
        {
            if (selectedFilter == (int) FilterOptions.All)
            {
                FilteredSelectionName = AppResources.VRAll;
            }else if (selectedFilter == (int) FilterOptions.Objections)
            {
                FilteredSelectionName = AppResources.ZakatObjection;
            }else if (selectedFilter == (int) FilterOptions.VatReviews)
            {
                FilteredSelectionName = AppResources.VatReview;
            }
              
        }
        
        private void AddOutletDecisionOptions()
        {
            
            var outletDecisionOptions = new ObservableCollection<SelectionModel>();
            outletDecisionOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VatReview,
                IsSelected = false
            });
            outletDecisionOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.ZakatObjection,
                IsSelected = false
            });
            SelectionOptions = outletDecisionOptions;
        }
        
    }
}