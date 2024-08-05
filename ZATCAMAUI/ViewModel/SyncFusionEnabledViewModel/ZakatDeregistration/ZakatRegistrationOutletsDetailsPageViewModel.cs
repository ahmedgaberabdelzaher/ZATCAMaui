using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
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
