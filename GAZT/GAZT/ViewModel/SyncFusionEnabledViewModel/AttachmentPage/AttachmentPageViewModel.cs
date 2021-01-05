using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ICRListPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.AttachmentPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class AttachmentPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public static Decimal AttachmentUploadedSize = 0;
        public static bool IsToBeFilled = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;
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
        private bool _attachmentSizeVisibility = attachmentSizeVisibility;
        public bool AttachmentSizeVisibility
        {
            get
            {
                return _attachmentSizeVisibility;
            }
            set
            {
                _attachmentSizeVisibility = value;
                RaisePropertyChanged("AttachmentSizeVisibility");
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
                RaisePropertyChanged("TotalAttachmentSize");
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

        private ObservableCollection<VATAttachment> _attachmentList;
        public ObservableCollection<VATAttachment> AttachmentList
        {
            get
            {
                return _attachmentList;
            }
            set
            {
                _attachmentList = value;
                RaisePropertyChanged("AttachmentList");
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

        private bool _isAmendClickedOnVAT = true;
        public bool IsAmendClickedOnVAT
        {
            get
            {
                return _isAmendClickedOnVAT;
            }
            set
            {
                _isAmendClickedOnVAT = value;
                RaisePropertyChanged("IsAmendClickedOnVAT");
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            GoBackClick = new Command(async () =>
            { 
                _navigationService.GoBack();
            });
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachment();
            });
        }
        #endregion
        #region Method
        public async Task AddAttachment()
        {
            try
            {
                try
                {
                    if (AttachmentCount <= 40)
                    {
                        string[] filetypes;

                        filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForAll();

                        //                if (Device.RuntimePlatform == Device.iOS)
                        //                {
                        //                    filetypes = new string[] {
                        ////            UTType.PDF,
                        ////            "org.openxmlformats.wordprocessingml.document",
                        ////            "com.microsoft.word.doc",
                        ////"org.openxmlformats.spreadsheetml.sheet",
                        ////"org.openxmlformats.presentationml.presentation",
                        ////            UTType.JPEG,
                        ////            UTType.PNG,
                        ////            UTType.GIF,
                        ////            "com.microsoft.excel.xls",
                        ////            "com.microsoft.powerpoint.​ppt",
                        ////             UTType.Text
                        //                        };
                        //                }
                        //                else
                        //                {
                        //                    filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };
                        //                }
                        var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                        if (fileData != null && fileData.DataArray != null && fileData.DataArray.Length > 0)
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
                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                                        if (Convert.ToDecimal(AttachmentSize) <= 20)
                                        {
                                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
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
                                                    AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType);// await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0");
                                                    PopToRootPage();
                                                    if (_attachment != null && _attachment.d != null)
                                                    {
                                                        AttachmentName = string.Empty;
                                                        TimeZone localZone = TimeZone.CurrentTimeZone;
                                                        string standardName = localZone.DaylightName;
                                                        _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                        string uploadedDate = _attachment.d.Erfdt;// _zakatAttachment.UploadededDateToShow;
                                                        uploadedDate = uploadedDate.Replace("’", "");
                                                        uploadedDate = uploadedDate.Replace("‘", "");
                                                        uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                        _attachment.d.Erfdt = uploadedDate;
                                                        VATDeclarationDataForAttch.d.ATTACHSet.results.Add(_attachment.d);
                                                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationDataForAttch.d.ATTACHSet.results as List<Attachment>);
                                                        Device.BeginInvokeOnMainThread(async () =>
                                                        {
                                                            VatAttachmentsList = myCollection;

                                                        });
                                                        VatAttachmentsList = myCollection;
                                                        foreach (var item in VatAttachmentsList)
                                                        {
                                                            try
                                                            {
                                                                if (App.IsArabic)
                                                                {
                                                                    if (item.Erfdt != null)
                                                                    {
                                                                        //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                        //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                        item.Erfdt = item.Erfdt;
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
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        }
                                                        AttachmentCount++;
                                                        CloneAttachmentList(VatAttachmentsList);
                                                        // TotalAttachmentSize += AttachmentSize;
                                                        AttachmentName = string.Empty;
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
                                                    _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
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
            catch(Exception ex)
            {

            }
        }
        private async Task<AttachmentRootOject> SaveAttachment(byte[] attachmentByteData, string contentType)
        {
            AttachmentRootOject _attachment = null;
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachmentByteData, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0", contentType);
                    if (attachment != null && attachment.d != null)
                    {
                        attachmentSizeVisibility = true;
                        AttachmentSizeVisibility = attachmentSizeVisibility;
                        SizeList.Add(AttachmentSize);
                        AttachmentUploadedSize = GetAttachMentSize(SizeList);// AttachmentUploadedSize + AttachmentSize;
                        TotalAttachmentSize = AttachmentUploadedSize;
                        _attachment = attachment;
                    }
                    else
                    {
                        _attachment = null;
                    }
                }
                catch (Exception ex)
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
        //public async void ShowPdf(string pdfUrl, String Doguid)
        //{
        //    if (Device.RuntimePlatform == Device.iOS)
        //    {
        //        if (pdfUrl != null)
        //        {
        //            //Uri uri = new Uri(pdfUrl);
        //            //Device.OpenUri(uri);
        //            _navigationService.NavigateTo(App.PdfiOSView, "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + Doguid + "',Cotyp='VTA0')/$value?saml2=disabled");
        //        }
        //        else
        //        {
        //            //pop that certificate is not available
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //            });
        //        }
        //    }
        //    else
        //    {
        //        if (pdfUrl != null)
        //        {
        //            _navigationService.NavigateTo(App.PdfView, pdfUrl);
        //        }
        //        else
        //        {
        //            //pop that certificate is not available
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //            });
        //        }
        //    }
        //}
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

        public void ClearData()
        {
            try
            {
                if (AttachmentList != null)
                {
                    if (AttachmentList.Count > 0)
                    {
                        if (App.ICRStatus.Equals("E0001"))
                        {
                            if (AttachmentPageViewModel.IsToBeFilled == true)
                            {
                                AttachmentPageViewModel.IsToBeFilled = false;
                                AttachmentList.Clear();
                            }

                        }
                        else
                        {
                            AttachmentPageViewModel.IsToBeFilled = false;
                            AttachmentList.Clear();
                        }
                    }
                }
                else
                {
                    if (App.ICRStatus.Equals("E0001"))
                    {
                        if (AttachmentPageViewModel.IsToBeFilled == true)
                        {
                            AttachmentPageViewModel.IsToBeFilled = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnPageLoad()
        {
            AttachmentName = string.Empty;
            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
            {
                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsAmend == true)
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
        public decimal GetAttachMentSize(List<decimal> SizeList)
        {
            decimal TotalSize = 0;
            foreach (decimal attachmentSize in SizeList)
            {
                TotalSize = TotalSize + attachmentSize;
            }
            return TotalSize;
        }
        public int GetDeletedAttachmentIndex(VATAttachment attachment)
        {
            int indexToDelete = -1;
            for (int i = 0; i < VatAttachmentsList.Count; i++)
            {
                if (attachment.Filename.Equals(VatAttachmentsList[i].Filename))
                {
                    indexToDelete = i;
                    break;
                }
            }
            return indexToDelete;
        }
        public void ReduceTotalAttachmentSize(int indexToReduceTheSize)
        {
            if (indexToReduceTheSize > NumberOfAttachmentComingFromServer - 1)
            {
                if (SizeList != null && SizeList.Count > 0)
                {
                    int indexToDelete = indexToReduceTheSize - NumberOfAttachmentComingFromServer;
                    SizeList.RemoveAt(indexToDelete);
                    AttachmentUploadedSize = GetAttachMentSize(SizeList);
                    TotalAttachmentSize = AttachmentUploadedSize;
                }
            }
            else
            {
                NumberOfAttachmentComingFromServer = NumberOfAttachmentComingFromServer - 1;
                ICRListPageViewModel.numberOfAttachmentComingFromServer = ICRListPageViewModel.numberOfAttachmentComingFromServer - 1;
            }
        }


        public void CloneAttachmentList(ObservableCollection<Attachment> attachmentList)
        {
            ObservableCollection<VATAttachment> list = new ObservableCollection<VATAttachment>();
            for (int i = 0; i < attachmentList.Count; i++)
            {
                VATAttachment vATAttachment = new VATAttachment();

                vATAttachment.RetGuid = attachmentList[i].RetGuid;
                vATAttachment.Seqno = attachmentList[i].Seqno;
                vATAttachment.SchGuid = attachmentList[i].SchGuid;
                vATAttachment.Dotyp = attachmentList[i].Dotyp;
                vATAttachment.Srno = attachmentList[i].Srno;
                vATAttachment.Doguid = attachmentList[i].Doguid;
                vATAttachment.AttBy = attachmentList[i].AttBy;
                vATAttachment.Filename = attachmentList[i].Filename;
                vATAttachment.FileExtn = attachmentList[i].FileExtn;
                vATAttachment.Mimetype = attachmentList[i].Mimetype;
                vATAttachment.ByPusr = attachmentList[i].ByPusr;
                vATAttachment.Erfdt = attachmentList[i].Erfdt;
                vATAttachment.Erftm = attachmentList[i].Erftm;
                vATAttachment.DataVersion = attachmentList[i].DataVersion;
                vATAttachment.DocUrl = attachmentList[i].DocUrl;
                vATAttachment.OutletRef = attachmentList[i].OutletRef;
                vATAttachment.Enbedit = attachmentList[i].Enbedit;
                vATAttachment.Enbdele = attachmentList[i].Enbdele;
                vATAttachment.Visedit = attachmentList[i].Enbdele;
                vATAttachment.Visdel = attachmentList[i].Enbdele;
                if(App.ICRStatus.Equals("E0045") || App.ICRStatus.Equals("E0006") || App.ICRStatus.Equals("E0056"))
                {
                    if(NumberOfAttachmentComingFromServer > 0 && i < NumberOfAttachmentComingFromServer)
                    {
                            vATAttachment.DeleteImageSource = "ic_Delete_disabled.png";
                    }
                    else
                    {
                        vATAttachment.DeleteImageSource = "ic_delete.png";
                    }
                }
                else
                {
                    vATAttachment.DeleteImageSource = "ic_delete.png";
                }

                list.Add(vATAttachment);
            }
            AttachmentList = list;


    }
       
        #endregion
    }
}
