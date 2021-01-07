using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.BillDetailsPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class BillDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCopySadadNumberButtonClicked { get; set; }
        public ICommand DowmoadForm { get; set; }
        public ZakatReturnDetailsD zakatReturnDetailsD { get; set; }
        string Cokey = "";
        string Cotyp = "";
        public bool IsrefreshEnabled = false;
        public ICommand GoBackClick { get; set; }
        #endregion
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
        private bool _isMainGridVisble = false;
        public bool IsMainGridVisble
        {
            get
            {
                return _isMainGridVisble;
            }
            set
            {
                _isMainGridVisble = value;
                RaisePropertyChanged("IsMainGridVisble");
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
        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                _estimatedZAKATSADADNumber = value;
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
            }
        }
        private string _refreshIconImageSource = "ic_refresh.png";
        public string RefreshIconImageSource
        {
            get
            {
                return _refreshIconImageSource;
            }
            set
            {
                _refreshIconImageSource = value;
                RaisePropertyChanged("RefreshIconImageSource");
            }
        }
        //private string _sopbel;
        //public string Sopbel
        //{
        //    get
        //    {
        //        return _stotamt;
        //    }
        //    set
        //    {
        //        _stotamt = value;
        //        RaisePropertyChanged("Sopbel");
        //    }
        //}
        //private string _sadadid;
        //public string Sadadid
        //{
        //    get
        //    {
        //        return _sadadid;
        //    }
        //    set
        //    {
        //        _sadadid = value;
        //        RaisePropertyChanged("Sadadid");
        //    }
        //}
        //private string _stotamt;
        //public string Stotamt
        //{
        //    get
        //    {
        //        return _sopbel;
        //    }
        //    set
        //    {
        //        _sopbel = value;
        //        RaisePropertyChanged("Stotamt");
        //    }
        //}
        #endregion
        #region Constructor
        public BillDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            DowmoadForm = new Command(async () =>
            {
                GetPdfUrl();
            });
            OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                await Clipboard.SetTextAsync(EstimatedZAKATSADADNumber.Sopbel);
                if (Clipboard.HasText)
                {
                    var text = await Clipboard.GetTextAsync();
                    await _dialogService.ShowMessageBox(AppResources.ZZIthascopiedsadadpaymentnumber + Environment.NewLine + " " + text, AppResources.Information);
                    //DisplayAlert("Success", string.Format("Your copied text is({0})", text), "OK");
                }
                //await _dialogService.ShowMessage(AppResources.ZZIthascopiedsadadpaymentnumber, AppResources.Information);
            });
        }
        #endregion
        public async Task OnPageLoad()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.Fbnum, ZakatReturnDetailsPageViewModel.Fbguid); // Method to get the invoice
                    PopToRootPage();
                    if (estimatedZAKATReturnsSADADNumber != null && estimatedZAKATReturnsSADADNumber.d != null)
                    {
                        IsMainGridVisble = true;
                        if (Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Undisamt) > 0 || Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Disamt) > 0)
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                            EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel))
                                IsrefreshEnabled = true;
                            //  RefreshIconImageSource = "ic_refresh.png";
                            //else
                            //    IsrefreshEnabled = false;
                        }
                        else
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = false;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = true;
                            EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel))
                                IsrefreshEnabled = true;
                            //    RefreshIconImageSource = "ic_refresh.png";
                            //else
                            //    IsrefreshEnabled = false;
                        }
                        GetUpdatedDataAfterAddingComma();
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                        IsLoading = false;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        #region Method
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
            // string url =  await  WebServiceManager.GAZTEstimatedZAKATReturnInvoicePdf(Cokey);
            ShowPdf(url);
        }
        public async void ShowPdf(string pdfUrl)
        {
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    if (pdfUrl != null)
            //    {
            //        //Uri uri = new Uri(pdfUrl);
            //        //Device.OpenUri(uri);
            //        _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
            //    }
            //    else
            //    {
            //        //pop that certificate is not available
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
            //        });
            //    }
            //}
            //else
            //{
            if (pdfUrl != null)
            {
                _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                //pop that certificate is not available
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
            //}
        }
        public void ClearData()
        {
            IsrefreshEnabled = false;
            IsMainGridVisble = false;
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
                    // Handle Exception
                }
            }
        }
        #endregion
    }
}
