using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
  
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

                    someThingWhentWrong.OnDone = () =>
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


                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
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

            }
            catch (Exception)
            {
                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.PopAsync();
            }

        }


    }
}
