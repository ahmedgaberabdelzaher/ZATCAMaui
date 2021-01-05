using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using pdfjs.Interfaces;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.Pdf_ViewModel
{
    [Preserve(AllMembers = true)]
    public class PdfViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
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
                RaisePropertyChanged("PdfBytes");
            }
        }

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

        private bool _loading = false;
        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                RaisePropertyChanged("Loading");
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
                RaisePropertyChanged("IsShareButtonEnable");
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

            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();

            });
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
                        getPdfStream();

                    }
                    else
                    {
                        IsShareButtonEnable = false;
                        String OnSuccessfulAuthentication = AppResources.PdfIsNoteAvailable;
                        //await _dialogService.ShowMessageBox(OnSuccessfulAuthentication, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(OnSuccessfulAuthentication));
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {

            }
        }

        public void getPdfStream()
        {
            Stream stream = null;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(DownloadUrl);
                myReq.Headers["ichannel"] = App.IncomingChannel;

                CookieContainer cookieContainer = new CookieContainer();

                try
                {
                    foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                    {
                        Cookie cookie = new Cookie();

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            if (cookieModel.Domain.StartsWith(".") == false)
                            {
                                cookie.Domain = "." + cookieModel.Domain;
                            }
                            else
                            {
                                cookie.Domain = cookieModel.Domain;
                            }
                        }
                        else if (Device.RuntimePlatform == Device.Android)
                        {
                            cookie.Domain = Constants.PartialDomainUrlForCookies;
                        }

                        cookie.Comment = cookieModel.Comment;   
                        cookie.Version = cookieModel.Version;
                        cookie.HttpOnly = cookieModel.IsHttpOnly;
                        cookie.Path = cookieModel.Path;
                        cookie.Name = cookieModel.CName;
                        cookie.Value = cookieModel.CValue;
                        cookie.Secure = cookieModel.Secure;
                        cookieContainer.Add(cookie);
                    }

                    myReq.CookieContainer = cookieContainer;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

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
                    string strBase64 = String.Empty;
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                               // await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                            });
                        }

                    }
                    else
                    {
                        IsShareButtonEnable = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                         //   await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                        });
                    }
                }
                else
                {
                    IsShareButtonEnable = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //await _dialogService.ShowMessageBox(AppResources.PdfIsNotAvailableFor, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNotAvailableFor));
                    });
                }

            }
            catch (Exception ex)
            {
                IsShareButtonEnable = false;
            }
        }

        #endregion
    }
}
