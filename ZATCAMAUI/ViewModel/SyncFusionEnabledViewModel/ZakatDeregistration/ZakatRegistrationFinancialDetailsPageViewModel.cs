using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{

    public class ZakatRegistrationFinancialDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {
        public ZakatRegistrationFinancialDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        public async Task LoadDataFinancialDetails()
        {
            try
            {
                App.DisplayProgressView();
                await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.FinancialDetail);
                App.HideProgressView();
            }
            catch (InternetException)
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
            }
            catch (GAZTErrorException ex)
            {

                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


            }
            catch (Exception)
            {
                App.HideProgressView();
            }
        }
    }
}
