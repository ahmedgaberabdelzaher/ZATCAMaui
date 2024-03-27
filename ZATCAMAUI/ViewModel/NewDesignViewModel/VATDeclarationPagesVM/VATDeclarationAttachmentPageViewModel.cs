using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Mangers;
using RGPopup.Maui.Services;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM
{

    public class VATDeclarationAttachmentPageViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public static decimal AttachmentUploadedSize = 0;
        public static bool isToBeFilled = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;

        #endregion

        #region Property
        
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
        public VATDeclarationAttachmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnAttachmentClick = new Command(async () =>
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

                        filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentTypeStringForAll();
                        PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                        //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                        var fileData = await FilePicker.PickAsync(options);
                        var stream = await fileData.OpenReadAsync();
                        var attachment = UtilityManager.ReadFully(stream as Stream);

                        if (fileData != null && attachment != null && attachment.Length > 0)
                        {
                            //attachment = fileData.DataArray;
                            AttachmentName = fileData.FileName;
                            if (fileData.FileName.Contains("."))
                            {
                                string[] ExtensionArray = fileData.FileName.Split('.');
                                string Extention = ExtensionArray.Last();
                                if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls" || Extention.ToLower() == "png" || Extention.ToLower() == "ppt" || Extention.ToLower() == "pptx" || Extention.ToLower() == "gif" || Extention.ToLower() == "txt")
                                {
                                    if (TotalAttachmentSize <= 300)
                                    {
                                        AttachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 2);
                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);

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
                                                        MainThread.BeginInvokeOnMainThread(() =>
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
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }
                                                        AttachmentCount++;
                                                        CloneAttachmentList(VatAttachmentsList);
                                                        AttachmentName = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                            }
                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan20MB));

                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTotalFilesizeshouldnotbemorethan300MB));

                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                            }
                        }
                    }
                    else
                    {
                        AttachmentName = string.Empty;
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoofallowedattachmentsare40));

                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    });
                }
            }
            catch (Exception)
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
                catch (Exception)
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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
                            if (isToBeFilled == true)
                            {
                                isToBeFilled = false;
                                AttachmentList.Clear();
                            }

                        }
                        else
                        {
                            isToBeFilled = false;
                            AttachmentList.Clear();
                        }
                    }
                }
                else
                {
                    if (App.ICRStatus.Equals("E0001"))
                    {
                        if (isToBeFilled == true)
                        {
                            isToBeFilled = false;
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        public void OnPageLoad()
        {
            AttachmentName = string.Empty;
            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
            {
                if (VATReturnsPageViewModelEX.IsAmend == true)
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
                GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer = GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer - 1;
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
                if (App.ICRStatus.Equals("E0045") || App.ICRStatus.Equals("E0006") || App.ICRStatus.Equals("E0056"))
                {
                    if (NumberOfAttachmentComingFromServer > 0 && i < NumberOfAttachmentComingFromServer)
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
