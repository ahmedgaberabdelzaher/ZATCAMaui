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
    public class ZakatRegistrationTaxPayerDetailsPageViewModel : EstablishmentRegistrationPageViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        public ZakatRegistrationTaxPayerDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        public bool _isLoading { get; set; }
        public bool isLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("isLoading");
            }
        }

        public async Task LoadDataTaxPayerDetails()
        {
            try
            {
                isLoading = true;
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
                        isLoading = false;
                        if (PopupNavigation.PopupStack.Count > 0)
                            PopupNavigation.PopAllAsync();
                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                        _navigationService.GoBack();
                    };
                    
                    await PopupNavigation.Instance.PushAsync(someThingWhentWrong);
                }
                if (PopupNavigation.PopupStack.Count > 0 && retVal)
                    await PopupNavigation.PopAsync();

            }
            catch (InternetException)
            {
                //  isLoading = true;
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync(true);


                Device.BeginInvokeOnMainThread(async () =>
                {
                    
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));

                    // await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });

            }
            catch (GAZTErrorException ex)
            {
                isLoading = false;

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
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync(true);

                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


                //if (PopupNavigation.PopupStack.Count > 0)
                //    await PopupNavigation.PopAsync();

                //string message = ex.Message;

                //await _dialogService.ShowMessage(message, AppResources.Information, AppResources.ZZZOkayText, () =>
                //{
                //   // isLoading = false;
                //    _navigationService.GoBack();
                //});
            }
            catch (Exception mex)
            {
               // isLoading = false;
                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.PopAsync();
                Console.WriteLine(mex.Message);
            }
           
        }


    }
}
