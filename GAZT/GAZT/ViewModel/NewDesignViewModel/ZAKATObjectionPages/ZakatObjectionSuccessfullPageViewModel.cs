using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    public class ZakatObjectionSuccessfullPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnInvoiceClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }

        public bool IsrefreshEnabled = false;
       
            
        //============================start===================================================
        string Cokey = "";
        string Cotyp = "";

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
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                if (_estimatedZAKATSADADNumber == value) return;

                _estimatedZAKATSADADNumber = value;
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
            }
        }

        private bool _setSuccessMessageVisibility = false;
        public bool SetSuccessMessageVisibility
        {
            get
            {
                return _setSuccessMessageVisibility;
            }
            set
            {
                if (_setSuccessMessageVisibility == value) return;

                _setSuccessMessageVisibility = value;
                RaisePropertyChanged("SetSuccessMessageVisibility");
            }
        }

        private string _refreshIconImageSource = "";
        public string RefreshIconImageSource
        {
            get
            {
                return _refreshIconImageSource;
            }
            set
            {
                if (_refreshIconImageSource == value) return;

                _refreshIconImageSource = value;
                RaisePropertyChanged("RefreshIconImageSource");
            }
        }

        #endregion

        #region Constructor
        public ZakatObjectionSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            //=======================start==================================================
            OnInvoiceClicked = new Xamarin.Forms.Command(() =>
            {
                OnDownLoadInvoiceClicked();
            });

            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion

        #region Method

        protected void OnDownLoadInvoiceClicked()
        {
            GetPdfUrl();
        }


        public async Task OnPageLoad(ZakatReturnDetailsD ZakatReturnDetail)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    SetSuccussMessageVisibility();
                    EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(ZakatReturnDetail.Fbnum, ZAKATReturnDetailsViewModel.Fbguid); // Method to get the invoice
                    PopToRootPage();
                    if (estimatedZAKATReturnsSADADNumber != null && estimatedZAKATReturnsSADADNumber.d != null)
                    {
                        if (Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Undisamt) > 0 || Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Disamt) > 0)
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;

                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel))
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatAcknowledgmentWaitingText));

                            }
                            else
                            {
                                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
                                IsrefreshEnabled = false;
                                RefreshIconImageSource = "";
                                GetUpdatedDataAfterAddingComma();

                            }

                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                           // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                        IsLoading = false;
                       }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                       // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }


        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public void GetPdfUrl()
        {
            String url = Constants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "',Cotyp='" + Cotyp + "')/$value?saml2=enabled";
            ShowPdf(url);
        }
        public void ShowPdf(string pdfUrl)
        {
            if (pdfUrl != null)
            {
                _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                //pop that certificate is not available
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

                   // await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
            //}
        }
        public void ClearData()
        {
            if (EstimatedZAKATSADADNumber != null)
            {
                EstimatedZAKATSADADNumber = null;
            }
            IsrefreshEnabled = false;
        }
        private void GetUpdatedDataAfterAddingComma()
        {
            if (EstimatedZAKATSADADNumber != null)
            {
                try
                {
                    EstimatedZAKATSADADNumber.Undisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Undisamt);
                    EstimatedZAKATSADADNumber.Disamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Disamt);
                    EstimatedZAKATSADADNumber.Totamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Totamt);
                    EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
                    EstimatedZAKATSADADNumber.Sdisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Sdisamt);
                    EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                    // Handle Exception
                }
            }
        }

        private void SetSuccussMessageVisibility()
        {
            if (ZAKATReturnDetailsViewModel.IsBillsButtonTapped == true)
            {
                SetSuccessMessageVisibility = false;
            }
            else
            {
                SetSuccessMessageVisibility = true;
            }
        }

        #endregion
    }
}
