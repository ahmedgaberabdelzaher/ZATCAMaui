using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
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
                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync(true);
                try
                {
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    //});
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));


                }
                catch (Exception )
                {
                }
            }
            catch (GAZTErrorException ex)
            {

                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync(true);

                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

               
            }
            catch (Exception )
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
        }
    }
}
