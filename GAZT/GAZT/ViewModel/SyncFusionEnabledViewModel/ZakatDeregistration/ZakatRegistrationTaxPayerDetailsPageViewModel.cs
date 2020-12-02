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
               
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.RegistrationType);

                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.TaxpayerDetail);
                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.PopAsync();
            }
            catch (InternetException)
            {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });

            }
            catch (GAZTErrorException ex)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    }); 
                }
                catch
                {

                }
                string message = ex.Message;

                await _dialogService.ShowMessage(message, AppResources.Information, AppResources.OKText, () =>
                {
                   
                    _navigationService.GoBack();
                });
            }
            catch (Exception mex)
            {
                
                Console.WriteLine(mex.Message);
            }
           
        }
    }
}
