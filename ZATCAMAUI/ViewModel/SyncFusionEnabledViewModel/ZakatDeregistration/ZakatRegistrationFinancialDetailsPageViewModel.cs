using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{

    public class ZakatRegistrationFinancialDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        public ZakatRegistrationFinancialDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }

        public async Task LoadDataFinancialDetails()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            try
            {
                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.FinancialDetail);
                });
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (InternetException)
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);
                try
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));


                }
                catch (Exception)
                {
                }
            }
            catch (GAZTErrorException ex)
            {

                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
        }
    }
}
