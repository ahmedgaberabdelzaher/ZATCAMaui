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

namespace GAZT
{
    public class MyCertificateViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnMyTaxPayerProfileClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnCertificateClicked { get; set; }
        public ICommand OnZakatCertificateClicked { get; set; }
        
        #endregion

        #region Property
        private bool _isLoading=false;

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
               if(_PdfSelected!=null)
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
            //Task.Run(async () =>
            //{
            //    IsLoading = true;
            //    String lang = "EN";
            //    if (App.IsArabic == true)
            //        lang = "AR";
            //    ineligible:
            //    String response = await WebServiceManager.GAZTGetPdfUrl(lang, TaxPayerProfile.Tin);

            //    if (string.IsNullOrEmpty(response) != true)
            //    {
            //        DownloadUrl = response;
            //        pdf();
            //    }
            //    else
            //    {
            //        TaxPayerProfile.Tin = "3300057436";
            //        goto ineligible;
            //    }
            //    IsLoading = false;
            //});
            _dialogService = dialogService;
            OnLoginButtonClicked = new RelayCommand(async () =>
            {
                bool IsComingFromSearch = true;
                // _navigationService.NavigateTo(App.LoginView);

            });
            OnZakatCertificateClicked = new RelayCommand(async () =>
            {
                try
                {
                  
                        ShowZAKATPdf();
   
                }
                catch(Exception ex)
                {

                }
               

            });
            
            OnCertificateClicked = new Command(() =>
            {

               
                    ShowVATPdf();
               
            });


            OnBellClicked = new Command(async () =>
            {
                //_navigationService.NavigateTo(App.MyCertificate);

            });
            OnMyTaxPayerProfileClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.TaxPayerProfileView);

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
            catch(Exception e)
            {
                throw e;
            }
        }


        public async void ShowVATPdf()
        {
             string pdfUrl = await GetCertificateLink();
            if(Device.RuntimePlatform == Device.iOS)
            {
  if (pdfUrl != null)
            {
                Uri uri = new Uri(pdfUrl);
                Device.OpenUri(uri);
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

        public async void ShowZAKATPdf()
        {
            // string pdfUrl = await GetCertificateLink();

            string pdfUrl = await GetZakatCertificateLink();
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (pdfUrl != null)
                {
                    Uri uri = new Uri(pdfUrl);
                    Device.OpenUri(uri);
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
        private  async Task<string> GetCertificateLink()
        {
            String response = null;
           await Task.Run(async () =>
            {

                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                 response = await WebServiceManager.GAZTGetPdfUrl(lang, TaxPayerProfile.Tin);

            });
            return response;
        }

        private async Task<string> GetZakatCertificateLink()
        {
            String response = null;
            try
            {
               
                await Task.Run(async () =>
                {

                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";
                    response = await WebServiceManager.GAZTZakatGetPdfUrl(lang, TaxPayerProfile.Tin);

                });
            }
            catch(Exception ex)
            {

            }
            
            return response;
        }

        public async void OnPageLoad()
        {
            string lang = null;
            if (App.IsArabic)
            {
                lang = "AR";
            }
            else
            {
                lang = "EN";
            }
            TaxPayerProfile = App.TP;
           await WebServiceManager.GetAllGAZTCertificate(lang, App.TP.Userid);
        }

        #endregion
    }
}
