using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using MobileCoreServices;
using Newtonsoft.Json;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class AttachmentPageViewModel : ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        byte[] attachment;

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

        private VATDeclaration _vATDeclarationDataForAttch;
        public VATDeclaration VATDeclarationDataForAttch
        {
            get
            {
                return _vATDeclarationDataForAttch;
            }
            set
            {
                _vATDeclarationDataForAttch = value;
                RaisePropertyChanged("VATDeclarationDataForAttch");
            }
        }

        private string _dateSubmitted;
        public string DateSubmitted
        {
            get
            {
                return _dateSubmitted;
            }
            set
            {
                _dateSubmitted = value;
                RaisePropertyChanged("DateSubmitted");
            }
        }

        private string _attachmentName = "";
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;

                RaisePropertyChanged("AttachmentName");
            }
        }

        public decimal _attachmentSize = 0;

        public decimal AttachmentSize
        {
            get
            {
                return _attachmentSize;
            }
            set
            {
                _attachmentSize = value;

                RaisePropertyChanged("AttachmentSize");

            }
        }

        public decimal _totalAttachmentSize = 0;

        public decimal TotalAttachmentSize
        {
            get
            {
                return _totalAttachmentSize;
            }
            set
            {
                _totalAttachmentSize = value;
                if(_totalAttachmentSize!=0)
                {
                    AttachmentString = _totalAttachmentSize.ToString() + " of 300MB";
                }
                else
                {
                    AttachmentString = "0.00" + " of 300MB";
                }

                RaisePropertyChanged("TotalAttachmentSize");

            }
        }


        public string _attachmentString;

        public string AttachmentString
        {
            get
            {
                return _attachmentString;
            }
            set
            {
                _attachmentString = value;

                RaisePropertyChanged("AttachmentString");

            }
        }


        public int _attachmentCount = 0;

        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                _attachmentCount = value;

                RaisePropertyChanged("AttachmentCount");

            }
        }
        private ObservableCollection<Attachment> _vatAttachmentsList;
        public ObservableCollection<Attachment> VatAttachmentsList
        {
            get
            {
                return _vatAttachmentsList;
            }
            set
            {
                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
            }
        }
        private bool _isShowAttachmentButton = true;

        public bool IsShowAttachmentButton
        {
            get
            {
                return _isShowAttachmentButton;
            }
            set
            {
                _isShowAttachmentButton = value;
                RaisePropertyChanged("IsShowAttachmentButton");
            }
        }


        #endregion

        #region Constructor
        public AttachmentPageViewModel(INavigationService navigationService, IDialogService dialogService)
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



            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
              await  AddAttachment();
            });
        }
        #endregion

        #region Method

        public async Task AddAttachment()
        {
            try
            {
              

               
                    if (AttachmentCount <= 40)
                    {
                        string[] filetypes;
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            filetypes = new string[] {

                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
    "org.openxmlformats.spreadsheetml.sheet",
    "org.openxmlformats.presentationml.presentation",
                UTType.JPEG,
                UTType.PNG,
                UTType.GIF,
                "com.microsoft.excel.xls",
                "com.microsoft.powerpoint.​ppt",
                 UTType.Text
                            };


                        }
                        else
                        {
                            filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };

                        }
                       
                        var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                        if (fileData != null)
                        {
                            

                            attachment = fileData.DataArray;
                            AttachmentName = fileData.FileName;
                            if (fileData.FileName.Contains("."))
                            {
                                string Extention = fileData.FileName.Split('.')[1];
                                if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls" || Extention.ToLower() == "png" || Extention.ToLower() == "ppt" || Extention.ToLower() == "pptx" || Extention.ToLower() == "gif" || Extention.ToLower() == "txt")
                                {
                                    if (TotalAttachmentSize <= 300)
                                    {
                                        AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);


                                        if (Convert.ToDecimal(AttachmentSize) <= 20)
                                        {
                                            bool IsAttachmentPresent = false;
                                            foreach (Attachment ItemA in VATDeclarationDataForAttch.d.ATTACHSet.results)
                                            {
                                                if (AttachmentName == ItemA.Filename)
                                                {
                                                    IsAttachmentPresent = true;
                                                }
                                            }
                                            if (IsAttachmentPresent == false)
                                        {
                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                                AttachmentRootOject _attachment =await SaveAttachment(attachment, attachmentType);// await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0");
                                                PopToRootPage();
                                                if (_attachment != null && _attachment.d != null)
                                                {
                                                    AttachmentName = string.Empty;

                                                    VATDeclarationDataForAttch.d.ATTACHSet.results.Add(_attachment.d);
                                                    ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationDataForAttch.d.ATTACHSet.results as List<Attachment>);
                                                    Device.BeginInvokeOnMainThread(async () =>
                                                    {
                                                        VatAttachmentsList = myCollection;
                                                    });
                                                    VatAttachmentsList = myCollection;
                                                    foreach (var item in VatAttachmentsList)
                                                    {
                                                        if (App.IsArabic)
                                                        {
                                                            if (item.Erfdt != null)
                                                            {
                                                                item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                                                item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                                                item.Erfdt = UtilityManager.ToArabicDate(item.Erfdt);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (item.Erfdt != null)
                                                            {
                                                                item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                                                item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                            }
                                                        }
                                                    }

                                                    AttachmentCount++;
                                                    TotalAttachmentSize += AttachmentSize;
                                                    AttachmentName = string.Empty;
                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);

                                            }

                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;
                                            _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);

                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);

                                    }

                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                    }
                    else
                    {
                        AttachmentName = string.Empty;
                        _dialogService.ShowMessage(AppResources.ZMaximumnoofallowedattachmentsare40, AppResources.Information);

                    }
              
                
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }

        private async Task<AttachmentRootOject> SaveAttachment(byte[] attachmentByteData, string contentType)
        {
            AttachmentRootOject _attachment = null;
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async() =>
            {
                try
                {
                   
                    AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachmentByteData, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0", contentType);

                    if (attachment != null)
                    {
                        _attachment = attachment;
                    }
                    else
                    {
                        _attachment = null;
                    }
                 
                }
                catch(Exception ex)
                {
                  //  return null;
                }
                
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
            return _attachment;
        }
        public async void ShowPdf(string pdfUrl, String Doguid)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (pdfUrl != null)
                {
                    //Uri uri = new Uri(pdfUrl);
                    //Device.OpenUri(uri);
                    _navigationService.NavigateTo(App.PdfiOSView, "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='"+ Doguid + "',Cotyp='VTA0')/$value?saml2=disabled");
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        public void OnPageLoad()
        {
            AttachmentName = string.Empty;
            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
            {
                if (VATReturnsPageViewModel.IsAmend == true)
                {
                    IsShowAttachmentButton = true;
                }
                else
                {
                    IsShowAttachmentButton = false;
                }
            }
            else
            {
                IsShowAttachmentButton = true;
            }
        }

       
        #endregion
    }
}
