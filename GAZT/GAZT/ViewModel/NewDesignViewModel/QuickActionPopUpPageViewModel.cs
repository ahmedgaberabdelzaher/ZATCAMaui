using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class QuickActionPopUpPageViewModel : ViewModelBase
    {

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //============================start===================================================
        public ICommand OnMyReturnsClickedForZAKAT { get; set; }
        public ICommand OnMyBillsClicked { get; set; }
        public ICommand OnCorrespondanceClicked { get; set; }

        #region Property
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        #endregion

        #region Constructor
        public QuickActionPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnMyReturnsClickedForZAKAT = new Xamarin.Forms.Command(async() =>
            {
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 5);

            });
            OnMyBillsClicked = new Xamarin.Forms.Command(async() =>
            {
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
            });
            OnCorrespondanceClicked = new Xamarin.Forms.Command(async() =>
            {
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

            });
        }
        #endregion

        #region Method
        #endregion
    }
}
