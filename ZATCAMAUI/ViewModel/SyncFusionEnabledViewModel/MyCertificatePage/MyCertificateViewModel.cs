
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Net;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyCertificatePage
{

    public class MyCertificateViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnHomeButtonClicked { get; set; }
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
                OnPropertyChanged("SelectedTab");
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
                OnPropertyChanged("IsCertificateAvailableZakat");
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
                OnPropertyChanged("IsCertificateAvailableVAT");
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
                OnPropertyChanged("IsCertificateAvailableET");
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
                OnPropertyChanged("IsCertificateAvailableZakatTab");
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
                OnPropertyChanged("IsCertificateAvailableVATTab");
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
                OnPropertyChanged("IsCertificateAvailableETTab");
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
                OnPropertyChanged("SetNoDataLabelVisibilityZakat");
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
                OnPropertyChanged("SetNoDataLabelVisibilityVAT");
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
                OnPropertyChanged("SetNoDataLabelVisibilityET");
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
                OnPropertyChanged("IsVATCertificateAvailable");
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
                OnPropertyChanged("CertificateListZakat");
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
                OnPropertyChanged("CertificateListVAT");
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
                OnPropertyChanged("CertificateListET");
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
                OnPropertyChanged("SelectedCertificate");
                if (SelectedCertificate != null && SelectedCertificate.pdfURL != null)
                {
                    ShowPdf(SelectedCertificate.pdfURL);
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
                OnPropertyChanged("_PdfSelected");
                // _dialogService.ShowMessageBox("Please Wait Pdf Is Loading", AppResources.Information);
                if (_PdfSelected != null)
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
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
                OnPropertyChanged("CertificateVisible");
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
                OnPropertyChanged("TaxPayerProfile");
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
                OnPropertyChanged("PathOfPdf");
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
                OnPropertyChanged("CertificateType");
            }
        }
        private string _DownloadUrl = string.Empty;
        public string DownloadUrl
        {
            get
            {
                return _DownloadUrl;
            }
            set
            {
                _DownloadUrl = value;
                OnPropertyChanged("DownloadUrl");
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
                OnPropertyChanged("StreamForDownloadURL");
            }
        }
        #endregion
        #region Constructor
        public MyCertificateViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            OnHomeButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
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
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    var dependency = DependencyService.Get<ILocalFileProvider>();
                    if (dependency == null)
                    {
                        // DisplayAlert("Error loading PDF", "Computer says no", "OK");
                        return;
                    }
                    var fileName = Guid.NewGuid().ToString();
                    // Download PDF locally for viewing
                    using (WebClient client = new WebClient())
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
                        catch (Exception)
                        {


                        }
                    }
                    if (string.IsNullOrWhiteSpace(localPath))
                    {
                        return;
                    }
                }
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    PathOfPdf = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";
            }
            catch (Exception e)
            {
            }
        }
        public async Task ShowPdf(string pdfUrl)
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                if (pdfUrl != null)
                {
                    await _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                }
            }
            else
            {
                if (pdfUrl != null)
                {
                    await _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                }
            }
        }
        public async Task OnPageLoad()
        {
            try
            {
                IsCertificateAvailableVATTab = true;
                IsCertificateAvailableZakatTab = true;
                IsCertificateAvailableETTab = true;

                string lang = UtilityManager.GetLanguageParameter();
                TaxPayerProfile = App.TP;
                allCertificate = await WebServiceManager.GAZTGetAllCertificate(lang, App.TP.TIN);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                bool Flag = false;
                if (allCertificate != null)
                {
                    if (allCertificate.ZakatSet != null && allCertificate.ZakatSet != null && allCertificate.ZakatSet.Count > 0)
                    {
                        CertificateType = AppResources.ZakatCertificates;
                        // SetCertificateListViewVisibility();
                        CertificateListZakat = allCertificate.ZakatSet;
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
                    if (allCertificate.VATSet != null && allCertificate.VATSet != null && allCertificate.VATSet.Count > 0)
                    {
                        CertificateType = AppResources.VATCertificates;
                        //  SetCertificateListViewVisibility();
                        CertificateListVAT = allCertificate.VATSet;
                        if (Flag == false)
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
                    if (allCertificate.exciseSet != null && allCertificate.exciseSet != null && allCertificate.exciseSet.Count > 0)
                    {
                        CertificateType = AppResources.ExciseCertificates;
                        //   SetCertificateListViewVisibility();
                        CertificateListET = allCertificate.exciseSet;
                        if (Flag == false)
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

                }
                else
                {
                    SetNoDataLabelVisibilityET = true;
                    SetNoDataLabelVisibilityVAT = true;
                    SetNoDataLabelVisibilityZakat = true;

                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        #endregion
    }
}
