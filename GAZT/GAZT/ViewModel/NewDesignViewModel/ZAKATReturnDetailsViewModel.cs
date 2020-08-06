using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class ZAKATReturnDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }

        #region Property
        
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private ZakatReturnDetailsD _zakatReturnDetail;
        public ZakatReturnDetailsD ZakatReturnDetail
        {
            get
            {
                return _zakatReturnDetail;
            }
            set
            {
                _zakatReturnDetail = value;
                RaisePropertyChanged("ZakatReturnDetail");
            }
        }


        private ZakatReturnDetails _zakatReturnDetails;
        public ZakatReturnDetails ZakatReturnDetails
        {
            get
            {
                return _zakatReturnDetails;
            }
            set
            {
                _zakatReturnDetails = value;
                RaisePropertyChanged("ZakatReturnDetails");
            }
        }

        #endregion

        #region Constructor
        public ZAKATReturnDetailsViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            //OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.GoBack();
            //});

        }
        #endregion


        #region Method
        public async Task OnPageLoad(string fbguid)
        {
            try
            {
               // Fbguid = fbguid;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbguid);
                    PopToRootPage();
                    if (zakatReturnDetails != null && zakatReturnDetails.d != null)
                    {
                        ZakatReturnDetails = zakatReturnDetails;
                        ZakatReturnDetail = zakatReturnDetails.d;
                        GetUpdatedDataAfterAddingComma();

                        //SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
                        //SetChangeFromEstimateTAccountringBasisButtonVisibility(ZakatReturnDetails.d.Statusz);

                        //Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                       
                    }
                    else
                    {
                      //  IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            //     Dear taxpayer, the return is under GAZT review and cannot be amended.
                            if (WebServiceManager.ErrorMessage.Equals("Dear taxpayer, the return is under GAZT review and cannot be amended."))// message is always coming in english from the server
                            {
                                Device.BeginInvokeOnMainThread(async () => {
                                    if (App.IsArabic)
                                    {
                                        await _dialogService.ShowMessage(AppResources.ZDearTaxpayerTheReturnIsUnderGAZTReviewAndCannotBeAmended, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                        _navigationService.GoBack();
                                        WebServiceManager.ErrorMessage = string.Empty;
                                    }
                                });
                            }
                        }
                    }
                });
                await Task.Run(() =>
                {
                   // IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }

        ///* Method to insert the comma to amount variable
        ///

        private ZakatReturnDetailsD GetUpdatedDataAfterAddingComma()
        {
            if (ZakatReturnDetail != null)
            {
                ZakatReturnDetail.Estsl = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Estsl);
                ZakatReturnDetail.Cpamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Cpamt);
                ZakatReturnDetail.Zbamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zbamt);
                ZakatReturnDetail.Zkamt = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.Zkamt);
            }
            return ZakatReturnDetail;
        }


        // Method to pop all the pages from the stack
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    
                });
            }
        }
        #endregion

    }
}
