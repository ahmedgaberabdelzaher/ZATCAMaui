

using Mopups.Services;
using System.Net;
using System.Windows.Input;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PdfViewPage
{

    public class PdfViewModel : BaseViewModel
    {
        #region Variable
        public string pdfUrl;
        #endregion

        #region Property
        private byte[] _pdfBytes = null;
        public byte[] PdfBytes
        {
            get
            {
                return _pdfBytes;
            }
            set
            {
                _pdfBytes = value;
                OnPropertyChanged("PdfBytes");
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
                OnPropertyChanged("IsVisiblePdfView");
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
                OnPropertyChanged("PdfUrl");
            }
        }

        private bool _isShareButtonEnable;
        public bool IsShareButtonEnable
        {
            get
            {
                return _isShareButtonEnable;
            }
            set
            {
                _isShareButtonEnable = value;
                OnPropertyChanged("IsShareButtonEnable");
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

        public PdfViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        #endregion

        #region Method

        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    if (!string.IsNullOrEmpty(pdfUrl))
                    {
                        DownloadUrl = pdfUrl;
                        await getPdfStreamAsync();

                    }
                    else
                    {
                        IsShareButtonEnable = false;
                        string OnSuccessfulAuthentication = AppResources.PdfIsNoteAvailable;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(OnSuccessfulAuthentication));
                    }
                    IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }

        public Task getPdfStreamAsync()
        {
            Stream stream = null;
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(DownloadUrl);
                String lang = "EN";
                HttpWebRequest myReq = GetPdfDocument(DownloadUrl, lang);


                try
                {

                    WebResponse myResp = myReq.GetResponse();

                    if (myResp != null)
                    {
                        using (Stream streams = myResp.GetResponseStream())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                int count = 0;
                                do
                                {
                                    byte[] buf = new byte[1024];
                                    count = streams.Read(buf, 0, 1024);
                                    ms.Write(buf, 0, count);
                                } while (streams.CanRead && count > 0);

                                if (ms != null)
                                    PdfBytes = ms.ToArray();
                            }
                        }
                        string strBase64 = string.Empty;
                        if (PdfBytes != null && PdfBytes.Length > 0)
                        {
                            strBase64 = Convert.ToBase64String(PdfBytes);

                            if (!string.IsNullOrEmpty(strBase64))
                            {
                                try
                                {
                                    byte[] sPDFDecoded = Convert.FromBase64String(strBase64);
                                    if (sPDFDecoded != null)
                                    {
                                        stream = new MemoryStream(sPDFDecoded, true);
                                        if (StreamForDownloadURL != null)
                                        {
                                            StreamForDownloadURL.Flush();
                                            StreamForDownloadURL.Close();
                                            // StreamForDownloadURL = null;
                                        }
                                        IsShareButtonEnable = true;
                                        StreamForDownloadURL = stream;
                                    }
                                    else
                                    {
                                        IsShareButtonEnable = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    IsShareButtonEnable = false;
                                    
                                    
                                }
                            }
                            else
                            {
                                IsShareButtonEnable = false;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    // await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                                });
                            }

                        }
                        else
                        {
                            IsShareButtonEnable = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //   await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                            });
                        }
                    }
                    else
                    {
                        IsShareButtonEnable = false;
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                        });
                    }

                }
                catch (Exception)
                {
                    IsShareButtonEnable = false;
                }
            }
            catch (Exception)
            {
            }

            return Task.CompletedTask;
        }

        private static HttpWebRequest GetPdfDocument(string downloadUrl, string lang)
        {
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(downloadUrl);
            //SOAPAction  
            string deviceOs = DeviceInfo.Platform.ToString();
            string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
            string deviceModel = DeviceInfo.Model;
            Req.Accept = "application/json";
            Req.Headers.Add("X-Session-Language", lang);
            Req.Headers.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
            Req.Headers.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
            Req.Headers.Add("X-Device-Id", deviceUdid);
            Req.Headers.Add("X-Device-Name", deviceModel);
            Req.Headers.Add("X-Device-Platform", deviceOs);
            Req.Headers.Add("Authorization", App.Token);
            //HTTP method 
            //return HttpWebRequest  
            return Req;
        }
        #endregion
    }
}
