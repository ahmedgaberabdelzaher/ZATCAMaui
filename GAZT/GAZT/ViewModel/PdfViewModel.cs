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
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
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
                RaisePropertyChanged("IsLoading");
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

        private string _pdfUrl;
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
          await  Task.Run(async () =>
            {
                IsLoading = true;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
               
                String response = await WebServiceManager.GAZTGetPdfUrl(lang, TaxPayerProfile.Tin);
               
                if (string.IsNullOrEmpty(response) != true)
                {

                    DownloadUrl = response;// "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='005056B1365C1EEA80F0BFC0C36DE462',Cotyp='ZVT3')/$value";
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

                    await _dialogService.ShowMessageBox(OnSuccessfulAuthentication, "Information");

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
                       _dialogService.ShowMessageBox("Error loading PDF", "Information");

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
                        _dialogService.ShowMessageBox("Error loading PDF", "Information");
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
        #endregion
    }
}
