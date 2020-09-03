using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using pdfjs.Interfaces;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATDeregistrationSuccessPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        public ICommand DownloadConfirmationTapped { get; set; }

        #endregion
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
        private string _fBNumber = string.Empty;
        public string FBNumber
        {
            get
            {
                return _fBNumber;
            }
            set
            {
                _fBNumber = value;
                RaisePropertyChanged("FBNumber");
            }
        }
        public VATDeregistrationSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            DownloadConfirmationTapped = new Command(this.DownloadConfirmationClicked);

        }
        public async void DownloadConfirmationClicked()
        {
            try
            {
                downloadConfirmation();

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Download Confirmation

        public void downloadConfirmation()
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

                            String downloadurl = Constants.downloadFile + "'" + FBNumber + "')/$value";

                            StreamForDownloadURL = client.OpenRead(downloadurl);
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

        #endregion

    }
}
