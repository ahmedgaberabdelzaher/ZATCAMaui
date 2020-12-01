using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class ZakatRegistrationTaxPayerDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        public ZakatRegistrationTaxPayerDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }

        public async Task LoadDataTaxPayerDetails()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(App.ActivityIndicatorView, false);
                var retVal = await FetchDataForDisplayDetailsExt(EstablishmentRegistrationTabsEnum.RegistrationType);
                await FetchDataForDisplayDetailsExt(EstablishmentRegistrationTabsEnum.TaxpayerDetail);

                if (!retVal)
                {
                    var someThingWhentWrong = new AttachmentInformationPopUp(AppResources.SomethingwentwrongTaxDetails)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };

                    someThingWhentWrong.OnDone = async () =>
                    {
                        if (PopupNavigation.PopupStack.Count > 0)
                            PopupNavigation.PopAllAsync();
                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                        _navigationService.GoBack();
                    };
                    if (PopupNavigation.PopupStack.Count > 0)
                        PopupNavigation.PopAsync();
                    await PopupNavigation.Instance.PushAsync(someThingWhentWrong);
                    if (PopupNavigation.PopupStack.Count > 0 && retVal)
                        await PopupNavigation.PopAsync();
                }
            }
            catch (InternetException)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (PopupNavigation.PopupStack.Count > 0)
                        await PopupNavigation.PopAsync();
                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });

            }
            catch (GAZTErrorException ex)
            {

                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.PopAsync();
                string message = ex.Message;
                await _dialogService.ShowMessage(message, AppResources.Information, AppResources.OKText, () =>
                {
                    _navigationService.GoBack();
                });
            }
            catch (Exception mex)
            {
                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.PopAsync();
                Console.WriteLine(mex.Message);
            }
           
        }


    }
}
