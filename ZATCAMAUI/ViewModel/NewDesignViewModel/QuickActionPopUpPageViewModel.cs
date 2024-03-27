using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models.SyncfusionEnabledModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class QuickActionPopUpPageViewModel : BaseViewModel
    {

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnMyReturnsClickedForZAKAT { get; set; }
        public ICommand OnMyBillsClicked { get; set; }
        public ICommand OnCorrespondanceClicked { get; set; }

        #region Property

       
        #endregion

        #region Constructor

        public QuickActionPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            OnMyReturnsClickedForZAKAT = new Command(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 5);

            });
            OnMyBillsClicked = new Command(async () =>
            {
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
            });
            OnCorrespondanceClicked = new Command(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
                _navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

            });
        }

        #endregion
    }
}
