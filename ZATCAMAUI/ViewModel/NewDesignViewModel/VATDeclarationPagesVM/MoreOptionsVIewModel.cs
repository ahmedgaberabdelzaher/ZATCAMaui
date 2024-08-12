
using System.Windows.Input;

using Newtonsoft.Json;

using System.Collections.ObjectModel;
using System.Globalization;
using Foundation;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Interfaces;
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using ZATCAMAUI.Core.Helper;

namespace EGAZT.ViewModel.NewDesignViewModel.VATDeclarationPagesVM
{
    [Preserve(AllMembers = true)]
    public class MoreOptionsVIewModel : BaseViewModel
    {
        private List<String> _vatReturnUIButtons;
        public List<String> VatReturnUIButtons
        {
            get
            {
                return _vatReturnUIButtons;
            }
            set
            {
                if (_vatReturnUIButtons == value) return;
                _vatReturnUIButtons = value;
                OnPropertyChanged("VatReturnUIButtons");
            }
        }

        public static string NoteString = string.Empty;
        public static bool ClearNoteClicked = false;
        public static int NoteCount = 0;
        public static bool IsClearAndCloseForDraft = false;
        public static bool IsComingFromNotePage = false;
        public ICommand OnAddButtonClicked { get; set; }
        public ICommand OnClearButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
       
        private VATDeclaration _vATDeclarationData;

        public ICommand OnAttachmentClick { get; set; }
        public static Decimal AttachmentUploadedSize = 0;
        public static bool isToBeFilled = false;
        public bool isUploadHappened = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;

      
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
                OnPropertyChanged("IsLoading");
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
                OnPropertyChanged("AttachmentSizeVisibility");
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
                OnPropertyChanged("VATDeclarationDataForAttch");
            }
        }

        private string _CR2215flag;
        public string CR2215flag
        {
            get
            {
                return _CR2215flag;
            }
            set
            {
                _CR2215flag = value;
                OnPropertyChanged("CR2215flag");
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
                OnPropertyChanged("DateSubmitted");
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
                OnPropertyChanged("AttachmentName");
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
                OnPropertyChanged("AttachmentSize");
            }
        }

        private bool _isAttachEnabled = false;
        public bool IsAttachEnabled
        {
            get
            {
                return _isAttachEnabled;
            }
            set
            {
                _isAttachEnabled = value;
                OnPropertyChanged("IsAttachEnabled");
            }
        }

        //public Color _colorOf;
        //public Color ColorOf
        //{
        //    get
        //    {
        //        return _colorOf;
        //    }
        //    set
        //    {
        //        _colorOf = value;
        //        RaisePropertyChanged("ColorOf");
        //    }
        //}

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
                OnPropertyChanged("TotalAttachmentSize");
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
                OnPropertyChanged("AttachmentCount");
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
                OnPropertyChanged("VatAttachmentsList");
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
                OnPropertyChanged("AttachmentList");
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
                OnPropertyChanged("IsShowAttachmentButton");
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
                OnPropertyChanged("IsAmendClickedOnVAT");
            }
        }

        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                OnPropertyChanged("VATDeclarationData");
            }
        }
        private string _noteText;
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;
                if (!string.IsNullOrEmpty(_noteText))
                {
                    NoteString = _noteText;
                }
                else
                {
                    NoteString = string.Empty;
                }
                OnPropertyChanged("NoteText");
            }
        }
        private string _previousNoteText;
        public string PreviousNoteText
        {
            get
            {
                return _previousNoteText;
            }
            set
            {
                _previousNoteText = value;
                OnPropertyChanged("PreviousNoteText");
            }
        }
      
        public MoreOptionsVIewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnClearButtonClicked = new Command(() =>
            {
                NoteString = string.Empty;
                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote = false;
                    }
                }
                if (String.Compare(PreviousNoteText, NoteText) != 0)
                {
                    NoteText = PreviousNoteText;
                }
                ClearNoteClicked = true;
                MopupService.Instance.PopAsync();
                MessagingCenter.Send<Object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");

            });
            OnAddButtonClicked = new Command(() =>
            {
                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote = false;
                    }
                }
                AddNote();
               // MessagingCenter.Send<Object, string>(this, "AddNoteForVATDeclaration", "AddNoteForVATDeclaration");
               
            });
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            OnAttachmentClick = new Command(async () =>
            {
                await AddAttachment();
            });
        }

        public async Task AddAttachment()
        {
            try
            {
                try
                {
                    if (AttachmentCount < 10)
                    {
                        string[] filetypes;

                        filetypes = DependencyService.Get<ZATCAMAUI.Core.Interfaces.IDeviceInfo>().GetAttachmentTypeStringForAll();
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
                                        AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                                        if (Convert.ToDecimal(AttachmentSize) < 10)
                                        {
                                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                            {
                                                bool IsAttachmentPresent = false;
                                                foreach (Attachment ItemA in VATDeclarationDataForAttch.data.ATTACHSet)
                                                {
                                                    if (AttachmentName == ItemA.Filename)
                                                    {
                                                        IsAttachmentPresent = true;
                                                    }
                                                }
                                                if (IsAttachmentPresent == false)
                                                {
                                                    string attachmentType = UtilityManager.GetContentType(Extention);
                                                    AttachmentRootOject _attachment = await SaveAttachment(stream, attachmentType);// await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0");
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
                                                        _attachment.d.ColorOf = (Color)App.Current.Resources["SecondaryNew"];
                                                        VATDeclarationDataForAttch.data.ATTACHSet.Add(_attachment.d);
                                                      //  VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                                                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationDataForAttch.data.ATTACHSet as List<Attachment>);
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
                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                            }
                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;
                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan10MB));

                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTotalFilesizeshouldnotbemorethan100MB));

                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                            }
                        }
                    }
                    else
                    {
                        AttachmentName = string.Empty;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoofallowedattachmentsare10));

                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task<AttachmentRootOject> SaveAttachment(Stream attachmentByteData, string contentType)
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
                    AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachmentByteData, AttachmentName, VATDeclarationDataForAttch.data.ReturnIdz, "ZVLU", contentType);
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
                            if (VATDeclarationAttachmentPageViewModel.isToBeFilled == true)
                            {
                                VATDeclarationAttachmentPageViewModel.isToBeFilled = false;
                                AttachmentList.Clear();
                            }

                        }
                        else
                        {
                            VATDeclarationAttachmentPageViewModel.isToBeFilled = false;
                            AttachmentList.Clear();
                        }
                    }
                }
                else
                {
                    if (App.ICRStatus.Equals("E0001"))
                    {
                        if (VATDeclarationAttachmentPageViewModel.isToBeFilled == true)
                        {
                            VATDeclarationAttachmentPageViewModel.isToBeFilled = false;
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
                // vATAttachment.ColorOf = attachmentList[i].

                if (App.ICRStatus.Equals("E0045") || App.ICRStatus.Equals("E0006") || App.ICRStatus.Equals("E0056"))
                {
                    if (NumberOfAttachmentComingFromServer > 0 && i < NumberOfAttachmentComingFromServer)
                    {
                        vATAttachment.DeleteImageSource = "ic_Delete_disabled.png";
                        vATAttachment.ColorOf = (Color)App.Current.Resources["Gray"];
                    }
                    else
                    {
                        vATAttachment.DeleteImageSource = "ic_delete.png";
                        vATAttachment.ColorOf = (Color)App.Current.Resources["SecondaryNew"];
                    }
                }
                else
                {
                    vATAttachment.DeleteImageSource = "ic_delete.png";
                    vATAttachment.ColorOf = (Color)App.Current.Resources["SecondaryNew"];
                }

                list.Add(vATAttachment);
            }
            AttachmentList = list;
        }


        public void AddNote()
        {
            try
            {
     
                SetNote();

                //if (App.ICRStatus == "E0001")
                //{
                //    SetNote();
                //}
                //if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                //{
                //    Note note123 = VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //    if (note123 != null)
                //    {
                //        foreach (var item in VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //        {
                //            item.Strline = MoreOptionsVIewModel.NoteString;
                //            item.Tdline = MoreOptionsVIewModel.NoteString;
                //        }
                //        MoreOptionsVIewModel.IsComingFromNotePage = false;
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(MoreOptionsVIewModel.NoteString))
                //            SetNoteForDraftModes();
                //    }

                //   // Submit();



                //    if (MoreOptionsVIewModel.ClearNoteClicked == true)
                //    {
                //        Note note12 = VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //        if (note12 != null)
                //        {
                //            foreach (var item in VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //            {
                //                item.Strline = MoreOptionsVIewModel.NoteString;
                //                item.Tdline = MoreOptionsVIewModel.NoteString;
                //            }
                //            MoreOptionsVIewModel.IsComingFromNotePage = false;
                //            MoreOptionsVIewModel.NoteString = string.Empty;
                //        }
                //        MoreOptionsVIewModel.ClearNoteClicked = false;
                //    }
                //    MoreOptionsVIewModel.NoteString = string.Empty;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        public void SetNote()
        {
            SetNote(VatAttachmentsList);
        }

        public async void SetNote(ObservableCollection<Attachment> attachmentList)
        {
            try
            {
                VATDeclarationData.data.NOTESSet = new List<Note>();
                Note objNote = new Note();
                int count = VATDeclarationData.data.NOTESSet.Count;
                string Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
                //objNote.__metadata = new Metadata2();
                //objNote.__metadata.id = Url;
                //objNote.__metadata.uri = Url;
                //objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
                objNote.Notenoz = (count + 1).ToString();
                objNote.DataVersionz = "00000";
                objNote.Refnamez = String.Empty;
                objNote.XInvoicez = String.Empty;
                objNote.XObsoletez = string.Empty;
                objNote.Rcodez = "ZVAU_TPSUB";
                objNote.ByPusrz = string.Empty;
                objNote.Tdformat = string.Empty;
                objNote.Tdline = string.Empty;
                objNote.Erfusrz = VATDeclarationData.data.Gpart;
                objNote.ByGpartz = VATDeclarationData.data.Gpart;
                objNote.Namez = VATDeclarationData.data.Tpnm;
                objNote.AttByz = "TP";
                objNote.Noteno = (count + 1).ToString();
                objNote.Lineno = 1;
                objNote.ElemNo = 0;
                objNote.Strdt = string.Empty;
                objNote.Strtime = string.Empty;
                objNote.Sect = "VAT Return General Note";
                objNote.Strline = MoreOptionsVIewModel.NoteString;
                objNote.Tdline = MoreOptionsVIewModel.NoteString;
                VATDeclarationData.data.NOTESSet.Add(objNote);
                MoreOptionsVIewModel.IsComingFromNotePage = false;
                //AddNotePageViewModel.NoteString = string.Empty;
                foreach (var item in attachmentList)
                {
                    VATDeclarationData.data.ATTACHSet.Add(item);
                }
              
                await submitbtn(VATDeclarationData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        public async void SetNoteForDraftModes()
        {
            try
            {
                if (VATDeclarationData != null && VATDeclarationData.data != null && VATDeclarationData.data.NOTESSet != null && VATDeclarationData.data.NOTESSet != null && VATDeclarationData.data.NOTESSet.Count != 0)
                {
                    if (App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {

                    }
                    else
                    {
                        VATDeclarationData.data.NOTESSet = new List<Note>();
                    }
                }
                else
                {
                    VATDeclarationData.data.NOTESSet = new List<Note>();
                }


                //  VATDeclarationData.d.NOTESSet.results = new List<Note>();
                Note objNote = new Note();
                int count = VATDeclarationData.data.NOTESSet.Count;
                string Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
                //objNote.__metadata = new Metadata2();
                //objNote.__metadata.id = Url;
                //objNote.__metadata.uri = Url;
                //objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
                objNote.Notenoz = (count + 1).ToString();
                objNote.DataVersionz = "00000";
                objNote.Refnamez = String.Empty;
                objNote.XInvoicez = String.Empty;
                objNote.XObsoletez = string.Empty;
                objNote.Rcodez = "ZVAU_TPSUB";
                objNote.ByPusrz = string.Empty;
                objNote.Tdformat = string.Empty;
                objNote.Tdline = string.Empty;
                objNote.Erfusrz = VATDeclarationData.data.Gpart;
                objNote.ByGpartz = VATDeclarationData.data.Gpart;
                objNote.Namez = VATDeclarationData.data.Tpnm;
                objNote.AttByz = "TP";
                objNote.Noteno = (count + 1).ToString();
                objNote.Lineno = 1;
                objNote.ElemNo = 0;
                objNote.Strdt = string.Empty;
                objNote.Strtime = string.Empty;
                objNote.Sect = "VAT Return General Note";
                objNote.Strline = MoreOptionsVIewModel.NoteString;
                objNote.Tdline = MoreOptionsVIewModel.NoteString;
                VATDeclarationData.data.NOTESSet.Add(objNote);
                MoreOptionsVIewModel.IsComingFromNotePage = false;
                //AddNotePageViewModel.NoteString = string.Empty;
                foreach (var item in VatAttachmentsList)
                {
                    VATDeclarationData.data.ATTACHSet.Add(item);
                }
                await submitbtn(VATDeclarationData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }


        public async Task submitbtn(VATDeclaration VATDeclarationData1)
        {
            string operation = "70";// Passed operation "01" to submit the VAT Declaration Data
                                    //   VATDeclarationData.d.StepNumberz = "04";
                                    //VATDeclarationData.d.StepNumber = "00";
                                    // VATDeclarationData.d.Fbguid = string.Empty;
            VATDeclarationData1.data.StepNumberz = "04";
            VATDeclarationData1.data.UserTypz = "TP";
            VATDeclarationData1.data.Operationz = operation;
            VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationData1);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

