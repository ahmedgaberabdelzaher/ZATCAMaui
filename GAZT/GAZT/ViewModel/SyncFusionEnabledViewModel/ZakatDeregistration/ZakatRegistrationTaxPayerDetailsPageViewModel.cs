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
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });
                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.RegistrationType);
                });

                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.TaxpayerDetail);
                });
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (InternetException)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });

                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                string message = ex.Message;
                await _dialogService.ShowMessage(message, AppResources.Information, AppResources.OKText, () =>
                {
                    _navigationService.GoBack();
                });
            }
            catch (Exception mex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
                Console.WriteLine(mex.Message);
            }
        }
    }
}
