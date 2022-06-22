using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using GAZT.Manager;
using GAZT.Models;
using System.Threading.Tasks;
using System.Net;
using pdfjs.Interfaces;
using System.IO;
using System.Collections.Generic;
using GAZT.Helper;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.MyCertificate_ViewModel
{
    [Preserve(AllMembers = true)]
    public class MyCertificateViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnHomeButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnCertificateClicked { get; set; }
        public ICommand OnZakatCertificateClicked { get; set; }
        public ICommand OnVATCertificateClicked { get; set; }
        public ICommand OnExciseCertificateClicked { get; set; }
        public AllCertificate allCertificate { get; set; }
        #endregion
        #region Property
        private int _selectedTab = 0;
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }
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
        private bool _isCertificateAvailableZakat = false;
        public bool IsCertificateAvailableZakat
        {
            get
            {
                return _isCertificateAvailableZakat;
            }
            set
            {
                _isCertificateAvailableZakat = value;
                RaisePropertyChanged("IsCertificateAvailableZakat");
            }
        }
        private bool _isCertificateAvailableVAT = false;
        public bool IsCertificateAvailableVAT
        {
            get
            {
                return _isCertificateAvailableVAT;
            }
            set
            {
                _isCertificateAvailableVAT = value;
                RaisePropertyChanged("IsCertificateAvailableVAT");
            }
        }
        private bool _isCertificateAvailableET = false;
        public bool IsCertificateAvailableET
        {
            get
            {
                return _isCertificateAvailableET;
            }
            set
            {
                _isCertificateAvailableET = value;
                RaisePropertyChanged("IsCertificateAvailableET");
            }
        }
        private bool _isCertificateAvailableZakatTab = false;
        public bool IsCertificateAvailableZakatTab
        {
            get
            {
                return _isCertificateAvailableZakatTab;
            }
            set
            {
                _isCertificateAvailableZakatTab = value;
                RaisePropertyChanged("IsCertificateAvailableZakatTab");
            }
        }
        private bool _isCertificateAvailableVATTab = false;
        public bool IsCertificateAvailableVATTab
        {
            get
            {
                return _isCertificateAvailableVATTab;
            }
            set
            {
                _isCertificateAvailableVATTab = value;
                RaisePropertyChanged("IsCertificateAvailableVATTab");
            }
        }
        private bool _isCertificateAvailableETTab = false;
        public bool IsCertificateAvailableETTab
        {
            get
            {
                return _isCertificateAvailableETTab;
            }
            set
            {
                _isCertificateAvailableETTab = value;
                RaisePropertyChanged("IsCertificateAvailableETTab");
            }
        }
        private bool _setNoDataLabelVisibilityZakat = false;
        public bool SetNoDataLabelVisibilityZakat
        {
            get
            {
                return _setNoDataLabelVisibilityZakat;
            }
            set
            {
                _setNoDataLabelVisibilityZakat = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityZakat");
            }
        }
        private bool _setNoDataLabelVisibilityVAT = false;
        public bool SetNoDataLabelVisibilityVAT
        {
            get
            {
                return _setNoDataLabelVisibilityVAT;
            }
            set
            {
                _setNoDataLabelVisibilityVAT = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityVAT");
            }
        }
        private bool _setNoDataLabelVisibilityET = false;
        public bool SetNoDataLabelVisibilityET
        {
            get
            {
                return _setNoDataLabelVisibilityET;
            }
            set
            {
                _setNoDataLabelVisibilityET = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityET");
            }
        }
        private bool _isVATCertificateAvailable = false;
        public bool IsVATCertificateAvailable
        {
            get
            {
                return _isVATCertificateAvailable;
            }
            set
            {
                _isVATCertificateAvailable = value;
                RaisePropertyChanged("IsVATCertificateAvailable");
            }
        }
        private List<Result> _certificateListZakat;
        public List<Result> CertificateListZakat
        {
            get
            {
                return _certificateListZakat;
            }
            set
            {
                _certificateListZakat = value;
                RaisePropertyChanged("CertificateListZakat");
            }
        }
        private List<Result> _certificateListVAT;
        public List<Result> CertificateListVAT
        {
            get
            {
                return _certificateListVAT;
            }
            set
            {
                _certificateListVAT = value;
                RaisePropertyChanged("CertificateListVAT");
            }
        }
        private List<Result> _certificateListET;
        public List<Result> CertificateListET
        {
            get
            {
                return _certificateListET;
            }
            set
            {
                _certificateListET = value;
                RaisePropertyChanged("CertificateListET");
            }
        }
        private Result _selectedCertificate;
        public Result SelectedCertificate
        {
            get
            {
                return _selectedCertificate;
            }
            set
            {
                _selectedCertificate = value;
                RaisePropertyChanged("SelectedCertificate");
                if (SelectedCertificate != null && SelectedCertificate.Pdfurl != null)
                {
                    ShowPdf(SelectedCertificate.Pdfurl);
                }
            }
        }
        private string _PdfSelected;
        public string PdfSelected
        {
            get
            {
                _navigationService.NavigateTo(App.PdfView);
                return _PdfSelected;
            }
            set
            {
                _PdfSelected = value;
                RaisePropertyChanged("_PdfSelected");
                // _dialogService.ShowMessageBox("Please Wait Pdf Is Loading", AppResources.Information);
                if (_PdfSelected != null)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        _navigationService.NavigateTo(App.PdfView);
                    }
                    else
                    {
                        // ShowPdf();
                    }
                }
            }
        }
        private bool _CertificateVisible = false;
        public bool CertificateVisible
        {
            get
            {
                return _CertificateVisible;
            }
            set
            {
                _CertificateVisible = value;
                RaisePropertyChanged("CertificateVisible");
            }
        }
        private TaxPayerProfile _TaxPayerProfile = App.TP;
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                _TaxPayerProfile = value;
                RaisePropertyChanged("TaxPayerProfile");
            }
        }
        private string _PathOfPdf;
        public string PathOfPdf
        {
            get
            {
                return _PathOfPdf;
            }
            set
            {
                _PathOfPdf = value;
                RaisePropertyChanged("PathOfPdf");
            }
        }
        private string _certificateType;
        public string CertificateType
        {
            get
            {
                return _certificateType;
            }
            set
            {
                _certificateType = value;
                RaisePropertyChanged("CertificateType");
            }
        }
        private string _DownloadUrl = String.Empty;
        public string DownloadUrl
        {
            get
            {
                return _DownloadUrl;
            }
            set
            {
                _DownloadUrl = value;
                RaisePropertyChanged("DownloadUrl");
            }
        }
        private Stream _StreamForDownloadURL = null;
        public Stream StreamForDownloadURL
        {
            get
            {
                return _StreamForDownloadURL;
            }
            set
            {
                _StreamForDownloadURL = value;
                RaisePropertyChanged("StreamForDownloadURL");
            }
        }
        #endregion
        #region Constructor
        public MyCertificateViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnLoginButtonClicked = new RelayCommand(async () =>
            {
               
                // _navigationService.NavigateTo(App.LoginView);
            });
            //OnVATCertificateClicked = new RelayCommand(async () =>
            //{
            //    try
            //    {
            //        if (allCertificate != null)
            //        {
            //            if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
            //            {
            //                CertificateType = AppResources.VATCertificates;
            //                SetCertificateListViewVisibility();
            //                CertificateList = allCertificate.VATSet.results;
            //            }
            //            else
            //            {
            //                CertificateType =String.Empty;
            //                SetNoDataLabelViewVisibility();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //});
            OnHomeButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            //OnZakatCertificateClicked = new RelayCommand(async () =>
            //{
            //    try
            //    {
            //        if (allCertificate != null)
            //        {
            //            if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
            //            {
            //                CertificateType = AppResources.ZakatCertificates;
            //                SetCertificateListViewVisibility();
            //                CertificateList = allCertificate.ZakatSet.results;
            //            }
            //            else
            //            {
            //                CertificateType = String.Empty;
            //                SetNoDataLabelViewVisibility();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //});
            //OnExciseCertificateClicked = new RelayCommand(async () =>
            //{
            //    try
            //    {
            //        if (allCertificate != null)
            //        {
            //            if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
            //            {
            //                CertificateType = AppResources.ExciseCertificates;
            //                SetCertificateListViewVisibility();
            //                CertificateList = allCertificate.ExciseSet.results;
            //            }
            //            else
            //            {
            //                CertificateType = String.Empty;
            //                SetNoDataLabelViewVisibility();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //});
            OnBellClicked = new Command(async () =>
            {
                //_navigationService.NavigateTo(App.MyCertificate);
            });
            OnHomeIconClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
        #region Method
        public void pdf()
        {
            var localPath = string.Empty;
            Stream stream = null;
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    var dependency = DependencyService.Get<ILocalFileProvider>();
                    if (dependency == null)
                    {
                        // DisplayAlert("Error loading PDF", "Computer says no", "OK");
                        return;
                    }
                    var fileName = Guid.NewGuid().ToString();
                    // Download PDF locally for viewing
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        try
                        {
                            StreamForDownloadURL = client.OpenRead(DownloadUrl);
                            BinaryReader br = new BinaryReader(StreamForDownloadURL);
                            byte[] result = br.ReadBytes((int)StreamForDownloadURL.Length);
                            string strBase64 = Convert.ToBase64String(result);
                            if (string.IsNullOrEmpty(strBase64) != true)
                            {
                                byte[] sPDFDecoded = Convert.FromBase64String(strBase64);
                                stream = new MemoryStream(sPDFDecoded);
                                StreamForDownloadURL = stream;
                            }
                            localPath =
                          Task.Run(() => dependency.SaveFileToDisk(StreamForDownloadURL, $"{fileName}.pdf")).Result;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Write(ex.StackTrace.ToString());
                        }
                    }
                    //    using (var httpClient = new HttpClient())
                    //{
                    //    var pdfStream = Task.Run(() => httpClient.GetStreamAsync("https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='C4346B23F48E1ED982858E704178C406',Cotyp='ZVT3')/$value")).Result;
                    //    localPath =
                    //        Task.Run(() => dependency.SaveFileToDisk(pdfStream, $"{fileName}.pdf")).Result;
                    //}
                    if (string.IsNullOrWhiteSpace(localPath))
                    {
                        //   DisplayAlert("Error loading PDF", "Computer says no", "OK");
                        return;
                    }
                }
                if (Device.RuntimePlatform == Device.Android)
                    PathOfPdf = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";
                //else
                //    Path = url;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async void ShowPdf(string pdfUrl)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (pdfUrl != null)
                {
                    //Uri uri = new Uri(pdfUrl);
                    //Device.OpenUri(uri);
                    // _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
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
            }
            else
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
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            }
        }
        public void OnPageLoad()
        {
            try
            {
                IsCertificateAvailableVATTab = true;
                IsCertificateAvailableZakatTab = true;
                IsCertificateAvailableETTab = true;
                //if (!string.IsNullOrEmpty(UtilityManager.TPTaxAvalable))
                //{
                //    string[] TpTypes = UtilityManager.TPTaxAvalable.Split(',');
                //    foreach (string ItemType in TpTypes)
                //    {
                //        if (ItemType == "05")
                //        {
                //            IsCertificateAvailableZakatTab = true;
                //        }
                //        if (ItemType == "03" || ItemType == "13")
                //        {
                //            IsCertificateAvailableVATTab = true;
                //        }
                //        if (ItemType == "07")
                //        {
                //            IsCertificateAvailableETTab = true;
                //        }
                //    }
                //}
                string lang = UtilityManager.GetLanguageParameter();
                TaxPayerProfile = App.TP;
                allCertificate = WebServiceManager.GAZTGetAllCertificate(lang, App.TP.Userid);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                bool Flag = false;
                if (allCertificate != null)
                {
                    if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
                    {
                        CertificateType = AppResources.ZakatCertificates;
                       // SetCertificateListViewVisibility();
                        CertificateListZakat = allCertificate.ZakatSet.results;
                        SelectedTab = 0;
                        Flag = true;
                        IsCertificateAvailableZakat = true;
                        SetNoDataLabelVisibilityZakat = false;
                    }
                    else
                    {
                        IsCertificateAvailableZakat = false;
                        SetNoDataLabelVisibilityZakat = true;
                    }
                    if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
                    {
                        CertificateType = AppResources.VATCertificates;
                      //  SetCertificateListViewVisibility();
                        CertificateListVAT = allCertificate.VATSet.results;
                        if(Flag==false)
                        {
                            SelectedTab = 1;
                        }
                        IsCertificateAvailableVAT = true;
                        SetNoDataLabelVisibilityVAT = false;
                    }
                    else
                    {
                        IsCertificateAvailableVAT = false;
                        SetNoDataLabelVisibilityVAT = true;
                    }
                    if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
                    {
                        CertificateType = AppResources.ExciseCertificates;
                     //   SetCertificateListViewVisibility();
                        CertificateListET = allCertificate.ExciseSet.results;
                        if(Flag==false)
                        {
                            SelectedTab = 2;
                        }
                        IsCertificateAvailableET = true;
                        SetNoDataLabelVisibilityET = false;
                    }
                    else
                    {
                        IsCertificateAvailableET = false;
                        SetNoDataLabelVisibilityET = true;
                    }
                    //ZAKATCertificateList = allCertificate.ZakatSet.results;
                    //EXICISECertificateList = allCertificate.ExciseSet.results;
                    //if ((VATCertificateList != null && VATCertificateList.Count == 0) && (ZAKATCertificateList != null && ZAKATCertificateList.Count == 0) && (EXICISECertificateList != null && EXICISECertificateList.Count == 0))
                    //{
                    //    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    //      _navigationService.GoBack();
                    //}
                    // SetLayoutVisibility();
                }
                else
                {
                    SetNoDataLabelVisibilityET = true;
                    SetNoDataLabelVisibilityVAT = true;
                    SetNoDataLabelVisibilityZakat = true;
                    
                    //   _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    // _navigationService.GoBack();
                }
            }
            catch(InternetException ex)
            {   
                 _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        //private void SetLayoutVisibility()
        //{
        //    if(VATCertificateList != null && VATCertificateList.Count > 0)
        //    {
        //        IsVATCertificateAvailable = true;
        //    }
        //    else
        //    {
        //        IsVATCertificateAvailable = false;
        //    }
        //    if (ZAKATCertificateList != null && ZAKATCertificateList.Count > 0)
        //    {
        //        IsZAKATCertificateAvailable = true;
        //    }
        //    else
        //    {
        //        IsZAKATCertificateAvailable = false;
        //    }
        //    if (EXICISECertificateList != null && EXICISECertificateList.Count > 0)
        //    {
        //        IsEXISECertificateAvailable = true;
        //    }
        //    else
        //    {
        //        IsEXISECertificateAvailable = false;
        //    }
        //}
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        //private void SetCertificateListViewVisibility()
        //{
        //    IsCertificateAvailable = true;
        //    SetNoDataLabelVisibility = false;
        //}
        //private void SetNoDataLabelViewVisibility()
        //{
        //    IsCertificateAvailable = false;
        //    SetNoDataLabelVisibility = true;
        //}
        #endregion
    }
}
