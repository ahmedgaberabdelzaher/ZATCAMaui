using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using pdfjs.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GAZT.ViewModel
{
    public class PdfViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public string pdfUrl;
        #endregion

        #region Property

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                if (_isLoading == false)
                {
                    IsVisiblePdfView = true;
                }
                else
                {
                    IsVisiblePdfView = false;
                }
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isVisiblePdfView = false;
        public bool IsVisiblePdfView
        {
            get
            {
                return _isVisiblePdfView;
            }
            set
            {
                _isVisiblePdfView = value;
                RaisePropertyChanged("IsVisiblePdfView");
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

        private string _PathOfPdf = string.Empty;
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

        private string _pdfUrl = string.Empty;
        public string PdfUrl
        {
            get
            {
                return _pdfUrl;
            }
            set
            {
                _pdfUrl = value;
                RaisePropertyChanged("PdfUrl");
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

        private string _localPath = string.Empty;
        public string LocalPath
        {
            get
            {
                return _localPath;
            }
            set
            {
                _localPath = value;
                RaisePropertyChanged("LocalPath");
            }
        }
        //Streamoffile
        private Stream _streamoffile;
        public Stream Streamoffile
        {
            get
            {
                return _streamoffile;
            }
            set
            {
                _streamoffile = value;
                RaisePropertyChanged("Streamoffile");
            }
        }


        #endregion

        #region Constructor

        public PdfViewModel(INavigationService navigationService, IDialogService dialogService)
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




        }

        #endregion

        #region Method

        public async Task OnPageLoad()
        {
           
            await Task.Run(async () =>
             {
                 IsLoading = true;
                 String lang = "EN";
                 if (App.IsArabic == true)
                     lang = "AR";

                //  String response = await WebServiceManager.GAZTGetPdfUrl(lang, TaxPayerProfile.Tin);

                if (!string.IsNullOrEmpty(pdfUrl))
                 { DownloadUrl = pdfUrl;
                     //DownloadUrl = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='005056B1365C1EEA80F0BFC0C36DE462',Cotyp='ZVT3')/$value";
                    if (Device.RuntimePlatform == Device.Android)
                     {
                         pdf();
                     }
                     else
                     {
                        // Device.OpenUri(new Uri(response));
                    }

                 }
                 else
                 {
                     String OnSuccessfulAuthentication = AppResources.PdfIsNoteAvailable;

                     await _dialogService.ShowMessageBox(OnSuccessfulAuthentication, AppResources.Information);

                 }
                 IsLoading = false;
             });
        }

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
                        _dialogService.ShowMessageBox("Error loading PDF", AppResources.Information);
                        // DisplayAlert("Error loading PDF", "Computer says no", "OK");
                        return;
                    }
                    var fileName = Guid.NewGuid().ToString();
                    // Download PDF locally for viewing
                    try
                    {
                        byte[] PdfBytes;
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(DownloadUrl);

                        WebResponse myResp = myReq.GetResponse();
                        using (Stream streams = myResp.GetResponseStream())
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                byte[] buf = new byte[1024];
                                count = streams.Read(buf, 0, 1024);
                                ms.Write(buf, 0, count);
                            } while (streams.CanRead && count > 0);
                            PdfBytes = ms.ToArray();
                        }
                        string strBase64 = String.Empty;
                        if (PdfBytes != null)
                        {

                            strBase64 = Convert.ToBase64String(PdfBytes);
                        }
                        if (string.IsNullOrEmpty(strBase64) != true)
                        {
                            byte[] sPDFDecoded = Convert.FromBase64String(strBase64);
                            stream = new MemoryStream(sPDFDecoded);
                            StreamForDownloadURL = stream;
                            Streamoffile = StreamForDownloadURL;
                        }
                        localPath =
                      Task.Run(() => dependency.SaveFileToDisk(StreamForDownloadURL, $"{fileName}.pdf")).Result;
                        LocalPath = localPath;
                    }
                    catch (Exception ex)
                    {
                    }

                    if (string.IsNullOrWhiteSpace(localPath))
                    {
                        _dialogService.ShowMessageBox("Error loading PDF", AppResources.Information);
                        //   DisplayAlert("Error loading PDF", "Computer says no", "OK");

                        return;
                    }
                }

                PathOfPdf = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";
            }
            catch (Exception e)
            {
                throw e;
            }

        }

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

        #endregion
    }
}
