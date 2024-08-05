using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.SyncfusionEnabledModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class QuickActionPopUpPageViewModel : BaseViewModel
    {
        public ICommand OnMyReturnsClickedForZAKAT { get; set; }
        public ICommand OnMyBillsClicked { get; set; }
        public ICommand OnCorrespondanceClicked { get; set; }

        #region Property

       
        #endregion

        #region Constructor

        public QuickActionPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            OnMyReturnsClickedForZAKAT = new Command(async () =>
            {
                await MopupService.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 5);

            });
            OnMyBillsClicked = new Command(async () =>
            {
                BillInfo billInfo = new BillInfo();
                billInfo.BillTypeName = AppResources.All;
                await MopupService.Instance.PopAsync();
                _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
            });
            OnCorrespondanceClicked = new Command(async () =>
            {
                await MopupService.Instance.PopAsync();
                _navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

            });
        }

        #endregion
    }
}
