using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class ZakatRegistrationOutletsDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        public ZakatRegistrationOutletsDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }

        public async Task LoadDataOutletDetails()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            try
            {
                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.Outlets);
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }

            await Task.Run(() =>
            {
                App.HideProgressView();
            });
        }
    }
}
