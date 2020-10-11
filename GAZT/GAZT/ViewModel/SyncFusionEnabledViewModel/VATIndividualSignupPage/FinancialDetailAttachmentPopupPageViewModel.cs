using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Models.ZakatInstalationModels;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
   
    public class FinancialDetailAttachmentPopupPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoButtonClick { get; set; }

        public static Decimal AttachmentUploadedSize = 0;
        public static bool IsToBeFilled = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;

        #region Property
        public string _docTypeString;
        public string DocTypeString
        {
            get
            {
                return _docTypeString;
            }
            set
            {
                _docTypeString = value;
                RaisePropertyChanged("DocTypeString");
            }
        }

        public bool _IsAttachmentVisibile=true;
        public bool IsAttachmentVisibile
        {
            get
            {
                return _IsAttachmentVisibile;
            }
            set
            {
                _IsAttachmentVisibile = value;
                RaisePropertyChanged("IsAttachmentVisibile");
            }
        }
        private VATRegistrationDetails _vATRegistrationDetailsForAttach;
        public VATRegistrationDetails VATRegistrationDetailsForAttach
        {
            get
            {
                return _vATRegistrationDetailsForAttach;
            }
            set
            {
                _vATRegistrationDetailsForAttach = value;
                RaisePropertyChanged("VATRegistrationDetailsForAttach");
            }
        } 
        private VATRegistrationOtherDetails _VATRegistrationOtherDetails;
        public VATRegistrationOtherDetails VATRegistrationOtherDetails
        {
            get
            {
                return _VATRegistrationOtherDetails;
            }
            set
            {
                _VATRegistrationOtherDetails = value;
                RaisePropertyChanged("VATRegistrationOtherDetails");
            }
        }
        private VATRegistrationDetails _VATRegistrationDetailsData ;
        public VATRegistrationDetails VATRegistrationDetailsData
        {
            get
            {
                return _VATRegistrationDetailsData;
            }
            set
            {
                _VATRegistrationDetailsData = value;
                RaisePropertyChanged("VATRegistrationDetailsData");
            }
        }

        private VATAttachment _vATAttachmentObj;
        public VATAttachment VATAttachmentObj
        {
            get
            {
                return _vATAttachmentObj;
            }
            set
            {
                _vATAttachmentObj = value;
                RaisePropertyChanged("VATAttachmentObj");
            }
        }

        private int _selectedAttachmentType;
        public int SelectedAttachmentType
        {
            get
            {
                return _selectedAttachmentType;
            }
            set
            {
                _selectedAttachmentType = value;
                RaisePropertyChanged("SelectedAttachmentType");
            }
        }


        

        private List<ResultsItemForElgblDocSet> _resultsItemForDOCSet = null;
        public List<ResultsItemForElgblDocSet>  ResultsItemForDOCSet 
        {
            get
            {
                return _resultsItemForDOCSet;
            }
            set
            {
                _resultsItemForDOCSet = value;
                RaisePropertyChanged("ResultsItemForDOCSet");
            }
        }
        private ResultsItemForElgblDocSet _selectedResultsItemForDOCSet = null;
        public ResultsItemForElgblDocSet SelectedResultsItemForDOCSet
        {
            get
            {
                return _selectedResultsItemForDOCSet;
            }
            set
            {
                _selectedResultsItemForDOCSet = value;
                if (_selectedResultsItemForDOCSet != null)
                {
                    AttachmentTypeTxt = _selectedResultsItemForDOCSet.Txt50;
                    DocTypeString = _selectedResultsItemForDOCSet.DmsTp;
                    VatAttachmentsList.Clear();
                    filterList();
                    CloneAttachmentList(VatAttachmentsList);

                }
                RaisePropertyChanged("SelectedResultsItemForDOCSet");
            }
        }
        private ELGBL_DOCSet _eLGBL_DOCSet = null;
        public ELGBL_DOCSet ELGBL_DOCSet
        {
            get
            {
                return _eLGBL_DOCSet;
            }
            set
            {
                _eLGBL_DOCSet = value;
                RaisePropertyChanged("ELGBL_DOCSet");
            }
        }

        private string _attachmentTypeTxt = null;
        public string AttachmentTypeTxt
        {
            get
            {
                return _attachmentTypeTxt;
            }
            set
            {
                _attachmentTypeTxt = value;
                RaisePropertyChanged("AttachmentTypeTxt");
            }
        }



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
        public WhichAttachment _isComeForWhichAttachment;
        public WhichAttachment IsComeForWhichAttachment
        {
            get
            {
                return _isComeForWhichAttachment;
            }
            set
            {
                _isComeForWhichAttachment = value;
                RaisePropertyChanged("IsComeForWhichAttachment");
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
                if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                {
                    if (_attachmentList != null)
                    {
                        if (_attachmentList.Count >= 5)
                        {
                            IsAttachmentVisibile = false;
                        }
                        else
                        {
                            IsAttachmentVisibile = true; ;
                        }
                    }
                    else
                    {
                        IsAttachmentVisibile = true;
                    }
                }
                else
                {
                    if (_attachmentList != null)
                    {
                        if (_attachmentList.Count >= 1)
                        {
                            IsAttachmentVisibile = false;
                        }
                        else
                        {
                            IsAttachmentVisibile = true; ;
                        }
                    }
                    else
                    {
                        IsAttachmentVisibile = true;
                    }
                }
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

        public ObservableCollection<string> _fileAttachments;
        public ObservableCollection<string> FileAttachments
        {
            get
            {
                return _fileAttachments;
            }
            set
            {
                _fileAttachments = value;
                RaisePropertyChanged("FileAttachments");
            }
        }
        #endregion
        public FinancialDetailAttachmentPopupPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            FileAttachments = new ObservableCollection<string>();
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachmentTest();
            });

            GoButtonClick = new Xamarin.Forms.Command(() =>
            {
                PopupNavigation.Instance.PopAsync();
            });
        }


        public async Task AddAttachment()
        {
            string fname;

            string[] filetypes;

            filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForAll();

            var fileData = await CrossFilePicker.Current.PickFile(filetypes);
            if (fileData != null && fileData.DataArray != null && fileData.DataArray.Length > 0)
            {
                attachment = fileData.DataArray;

                fname = fileData.FileName ?? "null";

                FileAttachments.Add(fname);

                Debug.WriteLine("File = " + fname + "Attachment =" + attachment);
            }
        }
        public async Task AddAttachmentTest()
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
                        if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count;
                                if (count >= 5)
                                {
                                    await _dialogService.ShowMessage(AppResources.ZMaximumnoof5attachmentscanbeuploaded, AppResources.Information);
                                    await PopupNavigation.Instance.PopAsync();
                                    return;
                                }
                            }

                        }
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

                                        if (Convert.ToDecimal(AttachmentSize) <= 5)
                                        {
                                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                            {
                                                bool IsAttachmentPresent = false;
                                                foreach (Attachment ItemA in VATRegistrationDetailsForAttach.d.ATTDETSet.results)
                                                {
                                                    if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                                                    {
                                                        if (AttachmentName == ItemA.Filename && (ItemA.Dotyp == DocTypeString))
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (AttachmentName == ItemA.Filename)
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }
                                                    }
                                                }
                                                if (IsAttachmentPresent == false)
                                                {
                                                    string attachmentType = UtilityManager.GetContentType(Extention);
                                                    //doctypestring - drop down id
                                                    AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType, DocTypeString);// await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0");
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
                                                        _attachment.d.Dotyp = DocTypeString;
                                                        try
                                                        {
                                                            ResultsItemForDOCSetforsubmit _eligibledocset = new ResultsItemForDOCSetforsubmit();
                                                            _eligibledocset.DmsTp = DocTypeString;
                                                            _eligibledocset.DmsTxt = AttachmentTypeTxt;
                                                            _eligibledocset.TxnTp = "CRE_RGVT";
                                                            _eligibledocset.LineNo = 0;
                                                            _eligibledocset.Mandt = "";
                                                            _eligibledocset.DataVersion = "";
                                                            _eligibledocset.FormGuid = "";
                                                            _eligibledocset.Fbtyp = "";
                                                            _eligibledocset.RankingOrder = "";
                                                            VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.results.Add(_eligibledocset);

                                                        }
                                                        catch (Exception ex)
                                                        { 
                                                        
                                                        }
                                                        VATRegistrationDetailsForAttach.d.ATTDETSet.results.Add(_attachment.d);
                                                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
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
                                                        filterList();
                                                        CloneAttachmentList(VatAttachmentsList);
                                                        // TotalAttachmentSize += AttachmentSize;
                                                        AttachmentName = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        //_dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    //_dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));
                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                //_dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                            }
                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;
                                            //_dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);
                                            if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                                            {
                                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZVATRAttachmentNote1));

                                            }
                                            else
                                            {
                                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan20MB));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        //_dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
                                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTotalFilesizeshouldnotbemorethan300MB));
                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    //_dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                //_dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                            }
                        }
                    }
                    else
                    {
                        AttachmentName = string.Empty;
                        //_dialogService.ShowMessage(AppResources.ZMaximumnoofallowedattachmentsare40, AppResources.Information);
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoofallowedattachmentsare40));
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //_dialogService.ShowMessage(ex.Message, AppResources.Information);
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void filterList()
        {
            try
            {
                // VATRegistrationDetailsForAttach.d.ATTDETSet.results
                if (VATRegistrationDetailsForAttach != null && VATRegistrationDetailsForAttach.d != null && VATRegistrationDetailsForAttach.d.ATTDETSet != null && VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                {
                    List<Attachment> attachmentsList = new List<Attachment>();
                    foreach (var item in VATRegistrationDetailsForAttach.d.ATTDETSet.results)
                    {
                        if (item.Dotyp == DocTypeString)
                        {
                            attachmentsList.Add(item);
                        }
                    }
                    VatAttachmentsList = new ObservableCollection<Attachment>(attachmentsList);
                  
                }
               
                //if (VatAttachmentsList != null && VatAttachmentsList.Count != 0)
                //{
                //    List<Attachment> attachmentsList = new List<Attachment>();
                //    foreach (var item in VatAttachmentsList)
                //    {
                //        if (item.Dotyp == DocTypeString)
                //        {
                //            attachmentsList.Add(item);
                //        }
                //    }

                //    VatAttachmentsList = new ObservableCollection<Attachment>(attachmentsList);
                //}
            }
            catch (Exception ex)
            {

            }
        }
        private async Task<AttachmentRootOject> SaveAttachment(byte[] attachmentByteData, string contentType, string Doctype)
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
                    AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachmentForFD(attachmentByteData, AttachmentName, VATRegistrationDetailsForAttach.d.ReturnIdz, Doctype, contentType);
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
                //if (App.ICRStatus.Equals("E0045") || App.ICRStatus.Equals("E0006") || App.ICRStatus.Equals("E0056"))
                //{
                //    if (NumberOfAttachmentComingFromServer > 0 && i < NumberOfAttachmentComingFromServer)
                //    {
                //        vATAttachment.DeleteImageSource = "ic_Delete_disabled.png";
                //    }
                //    else
                //    {
                //        vATAttachment.DeleteImageSource = "ic_delete.png";
                //    }
                //}
                //else
                //{
                //    vATAttachment.DeleteImageSource = "ic_delete.png";
                //}

                list.Add(vATAttachment);
            }
            AttachmentList = list;


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
    }
}
