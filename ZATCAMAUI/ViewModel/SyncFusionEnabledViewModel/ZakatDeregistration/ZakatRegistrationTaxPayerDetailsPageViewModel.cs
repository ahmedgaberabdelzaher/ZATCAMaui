using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{

    public class ZakatRegistrationTaxPayerDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {

        public ZakatRegistrationTaxPayerDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
       

        public async Task LoadDataTaxPayerDetails()
        {
            try
            {
                IsLoading = true;
                var retVal = await FetchDataForDisplayDetailsExt(EstablishmentRegistrationTabsEnum.RegistrationType);
                await FetchDataForDisplayDetailsExt(EstablishmentRegistrationTabsEnum.TaxpayerDetail);

                if (!retVal)
                {
                    var someThingWhentWrong = new AttachmentInformationPopUp(AppResources.SomethingwentwrongTaxDetails)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };

                    someThingWhentWrong.OnDone = () =>
                    {
                        IsLoading = false;
                        if (MopupService.Instance.PopupStack.Count > 0)
                            MopupService.Instance.PopAllAsync();
                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                        _navigationService.GoBack();
                    };

                    await MopupService.Instance.PushAsync(someThingWhentWrong);
                }
                if (MopupService.Instance.PopupStack.Count > 0 && retVal)
                    await MopupService.Instance.PopAsync();

            }
            catch (InternetException)
            {
                //  IsLoading = true;
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);


                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
                });

            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

            }
            catch (Exception)
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync();
            }

        }


    }
}
