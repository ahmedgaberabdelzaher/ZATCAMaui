using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
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
                await _dialogService.ShowMessage(message, AppResources.Information, AppResources.ZZZOkayText, () =>
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
