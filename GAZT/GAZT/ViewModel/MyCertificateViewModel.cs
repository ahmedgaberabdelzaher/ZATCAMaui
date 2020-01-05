using System;
using System;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using GAZT.Manager;
using GAZT.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using pdfjs.Interfaces;
using System.IO;
using System.Collections.Generic;

namespace GAZT
{
    public class MyCertificateViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
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

        private bool _isCertificateAvailable = false;

        public bool IsCertificateAvailable
        {
            get
            {
                return _isCertificateAvailable;
            }
            set
            {
                _isCertificateAvailable = value;
                RaisePropertyChanged("IsCertificateAvailable");
            }
        }

        private bool _setNoDataLabelVisibility = false;

        public bool SetNoDataLabelVisibility
        {
            get
            {
                return _setNoDataLabelVisibility;
            }
            set
            {
                _setNoDataLabelVisibility = value;
                RaisePropertyChanged("SetNoDataLabelVisibility");
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

       


        private List<Result> _certificateList;

        public List<Result> CertificateList
        {
            get
            {
                return _certificateList;
            }
            set
            {
                _certificateList = value;
                RaisePropertyChanged("CertificateList");
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
                bool IsComingFromSearch = true;
                // _navigationService.NavigateTo(App.LoginView);

            });
            OnVATCertificateClicked = new RelayCommand(async () =>
            {
                try
                {
                    if (allCertificate != null)
                    {
                        if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
                        {
                            CertificateType = AppResources.VATCertificates;
                            SetCertificateListViewVisibility();
                            CertificateList = allCertificate.VATSet.results;
                        }
                        else
                        {
                            CertificateType =String.Empty;
                            SetNoDataLabelViewVisibility();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            });


            OnZakatCertificateClicked = new RelayCommand(async () =>
            {
                try
                {
                    if (allCertificate != null)
                    {
                        if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
                        {
                            CertificateType = AppResources.ZakatCertificates;
                            SetCertificateListViewVisibility();
                            CertificateList = allCertificate.ZakatSet.results;
                        }
                        else
                        {
                            CertificateType = String.Empty;
                            SetNoDataLabelViewVisibility();
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            });

            OnExciseCertificateClicked = new RelayCommand(async () =>
            {
                try
                {
                    if (allCertificate != null)
                    {
                        if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
                        {
                            CertificateType = AppResources.ExciseCertificates;
                            SetCertificateListViewVisibility();
                            CertificateList = allCertificate.ExciseSet.results;
                        }
                        else
                        {
                            CertificateType = String.Empty;
                            SetNoDataLabelViewVisibility();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            });


          

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
                        catch (Exception)
                        {
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
                    Uri uri = new Uri(pdfUrl);
                    Device.OpenUri(uri);
                    //_navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
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

        //    public async Task ShowZAKATPdf(string pdfUrl)
        //    {

        //        if (Device.RuntimePlatform == Device.iOS)
        //        {
        //            if (pdfUrl != null)
        //            {
        //	Uri uri = new Uri(pdfUrl);
        //	Device.OpenUri(uri);
        //	// _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
        //}
        //            else
        //            {
        //                //pop that certificate is not available
        //                Device.BeginInvokeOnMainThread(async () =>
        //                {
        //                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //                });
        //            }
        //        }
        //        else
        //        {
        //            if (pdfUrl != null)
        //            {
        //                _navigationService.NavigateTo(App.PdfView, pdfUrl);
        //            }
        //            else
        //            {
        //                //pop that certificate is not available
        //                Device.BeginInvokeOnMainThread(async () =>
        //                {
        //                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //                });
        //            }

        //        }
        //    }

        //    private async Task ShowEXCISECertificate(string pdfUrl)
        //    {
        //        if (Device.RuntimePlatform == Device.iOS)
        //        {
        //            if (pdfUrl != null)
        //            {
        //	Uri uri = new Uri(pdfUrl);
        //	Device.OpenUri(uri);
        //	//  _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
        //}
        //            else
        //            {
        //                //pop that certificate is not available
        //                Device.BeginInvokeOnMainThread(async () =>
        //                {
        //                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //                });
        //            }
        //        }
        //        else
        //        {
        //            if (pdfUrl != null)
        //            {
        //                _navigationService.NavigateTo(App.PdfView, pdfUrl);
        //            }
        //            else
        //            {
        //                //pop that certificate is not available
        //                Device.BeginInvokeOnMainThread(async () =>
        //                {
        //                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //                });
        //            }
        //        }

        //    }


        //private  async Task<string> GetCertificateLink()
        //{
        //    String response = null;
        //   await Task.Run(async () =>
        //    {

        //        String lang = "EN";
        //        if (App.IsArabic == true)
        //            lang = "AR";
        //         response = await WebServiceManager.GAZTGetPdfUrl(lang, TaxPayerProfile.Tin);
        //        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

        //    });
        //    return response;
        //}

        //private async Task<string> GetZakatCertificateLink()
        //{
        //    String response = null;
        //    try
        //    {

        //        await Task.Run(async () =>
        //        {

        //            String lang = "EN";
        //            if (App.IsArabic == true)
        //                lang = "AR";
        //            response = await WebServiceManager.GAZTZakatGetPdfUrl(lang, TaxPayerProfile.Tin);
        //            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

        //        });
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //    return response;
        //}

        public async Task OnPageLoad()
        {
            string lang = UtilityManager.GetLanguageParameter();
            TaxPayerProfile = App.TP;
            allCertificate = await WebServiceManager.GAZTGetAllCertificate(lang, App.TP.Userid);
            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

            if (allCertificate != null)
            {
                if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
                {
                    CertificateType = AppResources.ZakatCertificates;
                    SetCertificateListViewVisibility();
                    CertificateList = allCertificate.ZakatSet.results;
                    
                }
                else if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
                {
                    CertificateType = AppResources.VATCertificates;
                    SetCertificateListViewVisibility();
                    CertificateList = allCertificate.VATSet.results;
                }
                else if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
                {
                    CertificateType = AppResources.ExciseCertificates;
                    SetCertificateListViewVisibility();
                    CertificateList = allCertificate.ExciseSet.results;
                }
                else
                {
                    CertificateType = String.Empty; 
                    SetNoDataLabelViewVisibility();
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
                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
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

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {

                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });

              
            }
        }

        private void SetCertificateListViewVisibility()
        {
            IsCertificateAvailable = true;
            SetNoDataLabelVisibility = false;
        }

        private void SetNoDataLabelViewVisibility()
        {
            IsCertificateAvailable = false;
            SetNoDataLabelVisibility = true;
        }

        #endregion
    }
}
