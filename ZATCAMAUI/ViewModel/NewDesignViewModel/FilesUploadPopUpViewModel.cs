using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Windows.Input;

using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class FilesUploadPopUpViewModel : BaseViewModel
    {
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoButtonClick { get; set; }

        public static decimal AttachmentUploadedSize = 0;
        public static bool IsToBeFilled = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;


        private bool _isImpoterAndExporter = false;
        public bool IsImpoterAndExporter
        {
            get
            {
                return _isImpoterAndExporter;
            }
            set
            {
                _isImpoterAndExporter = value;
                OnPropertyChanged("IsImpoterAndExporter");
            }
        }

        private bool _isEnableSwitchToggled = false;
        public bool IsEnableSwitchToggled
        {
            get
            {
                return _isEnableSwitchToggled;
            }
            set
            {
                _isEnableSwitchToggled = value;
                OnPropertyChanged("IsEnableSwitchToggled");
            }
        }

        private bool _isEnableSwitchToggledForButton = false;
        public bool IsEnableSwitchToggledForButton
        {
            get
            {
                return _isEnableSwitchToggledForButton;
            }
            set
            {
                _isEnableSwitchToggledForButton = value;
                OnPropertyChanged("IsEnableSwitchToggledForButton");
            }
        }

        private string _TitleOne = AppResources.ZZZZVATRAttachmentNote1;
        public string TitleOne
        {
            get
            {
                return _TitleOne;
            }
            set
            {
                _TitleOne = value;
                OnPropertyChanged("TitleOne");
            }
        }



        private string _TitleTwo = AppResources.ZZZZVATRAttachmentNote2;
        public string TitleTwo
        {
            get
            {
                return _TitleTwo;
            }
            set
            {
                _TitleTwo = value;
                OnPropertyChanged("TitleTwo");
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
        private string _dmsType = string.Empty;
        public string DmsType
        {
            get
            {
                return _dmsType;
            }
            set
            {
                _dmsType = value;
                OnPropertyChanged("DmsType");
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
        public bool _showAddAttachments = true;
        public bool ShowAddAttachments
        {
            get
            {
                return _showAddAttachments;
            }
            set
            {
                _showAddAttachments = value;
                OnPropertyChanged("ShowAddAttachments");
            }
        }
        public int _vatAttachmentCount = 0;
        public int VatAttachmentCount
        {
            get
            {
                return _vatAttachmentCount;
            }
            set
            {
                _vatAttachmentCount = value;
                OnPropertyChanged("VatAttachmentCount");
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
        private ObservableCollection<Attachment> _vatAttachmentsListtofilter;
        public ObservableCollection<Attachment> VatAttachmentsListtofilter
        {
            get
            {
                return _vatAttachmentsListtofilter;
            }
            set
            {
                _vatAttachmentsListtofilter = value;
                OnPropertyChanged("VatAttachmentsListtofilter");
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
                OnPropertyChanged("FileAttachments");
            }
        }

        private AttachmentsList _attachments;
        public AttachmentsList AttachmentsList
        {
            get
            {
                return _attachments;
            }
            set
            {
                if (_attachments == value) return;
                _attachments = value;
                OnPropertyChanged("AttachmentsList");
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
                OnPropertyChanged("IsComeForWhichAttachment");
            }
        }

        public string returnIdz = "";

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
                OnPropertyChanged("DocTypeString");
            }
        }

        public string _outletRef = string.Empty;
        public string OutletRef
        {
            get
            {
                return _outletRef;
            }
            set
            {
                _outletRef = value;
                OnPropertyChanged("OutletRef");
            }
        }

        public FilesUploadPopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            FileAttachments = new ObservableCollection<string>();
            OnAttachmentClick = new Command(async () =>
            {
                IsLoading = true;
                await AddAttachmentEx();
                IsLoading = false;
            });

            GoButtonClick = new Command(() =>
            {
                MopupService.Instance.PopAsync();

            });
        }

        public async Task AddAttachment()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async() => 
                {
                    string fname;

                    string[] filetypes;

                    filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForAll();

                    var customFileType = new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                    { DevicePlatform.iOS, filetypes},
                    { DevicePlatform.Android, filetypes }
                        });

                    PickOptions options = new()
                    {
                        PickerTitle = "Please select a comic file",
                        FileTypes = customFileType,
                    };

                    //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                    var fileData = await FilePicker.Default.PickAsync(options);

                    if (fileData != null)
                    {
                        using var stream = await fileData.OpenReadAsync();
                        byte[] bytes = new byte[stream.Length];

                        if (bytes != null && bytes.Length > 0)
                        {
                            attachment = bytes;

                            fname = fileData.FileName ?? "null";

                            FileAttachments.Add(fname);
                        }


                    }

                });
            }
            catch (Exception)
            {
            }
            
        }


        public async Task AddAttachmentEx()
        {
            try
            {
                try
                {
                    if (AttachmentCount <= 40)
                    {
                        string[] filetypes;


                        filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForAll();

                        if (IsComeForWhichAttachment == WhichAttachment.VATDeregistration ||
                        IsComeForWhichAttachment == WhichAttachment.TINDeregistration)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Where(x => x.Dotyp == DocTypeString).ToList().Count();

                                if (count >= 5)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));

                                    //await MopupService.Instance.PopAsync();
                                    return;
                                }

                            }
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VATInstalment)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 10)
                                {
                                    await MopupService.Instance.PopAsync();

                                    return;
                                }
                            }

                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VatReviewLateFiling)
                        {

                            if (VatAttachmentsList != null)
                            {
                                int ListCount = VatAttachmentsList.Count();
                                if (ListCount >= 9)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof9attachmentscanbeuploaded1));

                                    return;
                                }
                            }

                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy || IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountOne || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountTwo)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 1)
                                {
                                    await MopupService.Instance.PopAsync();
                                    return;
                                }
                            }
                        }

                        else if (IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod2Years || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod12Months || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriodOtherDoc)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 5)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));

                                    return;
                                }

                            }
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentFinance || IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentBankStatements)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 3)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.OldZakatInstalmentAttachmentLimitReached));

                                    //await MopupService.Instance.PopAsync();
                                    return;
                                }
                            }
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentBankStatements || IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentFinance || IsComeForWhichAttachment == WhichAttachment.VatReviewAttachments)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 10)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZakatInstalmentAttachmentLimitReached));

                                    //await MopupService.Instance.PopAsync();
                                    return;
                                }
                            }
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment || IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 1)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATReviewAttachmentLimitReached));

                                    //await MopupService.Instance.PopAsync();
                                    return;
                                }
                            }
                        }

                        PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                        //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                        var fileData = await FilePicker.PickAsync(options);
                        var stream = await fileData.OpenReadAsync();
                        attachment = UtilityManager.ReadFully(stream as Stream);

                        VatAttachmentCount++;
                        if (fileData != null && attachment != null && attachment.Length > 0)
                        {
                            // attachment = attachment;
                            AttachmentName = fileData.FileName;
                            if (fileData.FileName.Contains("."))
                            {

                                string myFilePath = fileData.FullPath;
                                string Extention = Path.GetExtension(myFilePath).Replace(".", "");
                                if (Extention.ToLower().Contains("doc") || Extention.ToLower().Contains("docx") || Extention.ToLower().Contains("jpg") || Extention.ToLower().Contains("jpeg") || Extention.ToLower().Contains("pdf") || Extention.ToLower().Contains("xlsx") || Extention.ToLower().Contains("xls"))
                                {
                                    if (TotalAttachmentSize <= 300)
                                    {
                                        AttachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 2);
                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);
                                        double fileSize = attachment.Length / 1024 / 1024.0;

                                        if (IsComeForWhichAttachment == WhichAttachment.TINDeregistration || IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentFinance || IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentBankStatements)
                                        {
                                            if (AttachmentName.Contains(" "))
                                            {
                                                string updatedName = AttachmentName.Replace(' ', '_');
                                                AttachmentName = updatedName;
                                            }
                                            if (fileSize <= 1)
                                            {
                                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                                {
                                                    bool IsAttachmentPresent = false;
                                                    foreach (Attachment ItemA in AttachmentsList.results)
                                                    {

                                                        if (AttachmentName == ItemA.Filename && ItemA.Dotyp == DocTypeString)
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }
                                                    }
                                                    if (IsAttachmentPresent == false)
                                                    {
                                                        string attachmentType = UtilityManager.GetContentType(Extention);
                                                        try
                                                        {

                                                            AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType, DocTypeString);


                                                            if (_attachment != null && _attachment.d != null)
                                                            {
                                                                try
                                                                {
                                                                    AttachmentName = string.Empty;
                                                                    /*TimeZone localZone = TimeZone.CurrentTimeZone;
                                                                    string standardName = localZone.DaylightName;*/
                                                                    TimeZoneInfo localZone = TimeZoneInfo.Local;
                                                                    string standardName = localZone.StandardName;
                                                                    _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                                    string uploadedDate = _attachment.d.Erfdt;
                                                                    uploadedDate = uploadedDate.Replace("’", "");
                                                                    uploadedDate = uploadedDate.Replace("‘", "");
                                                                    uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                                    _attachment.d.Erfdt = uploadedDate;
                                                                    _attachment.d.Dotyp = DocTypeString;
                                                                    AttachmentsList.results.Add(_attachment.d);
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    
                                                                    AttachmentName = string.Empty;
                                                                    await Task.Run(() =>
                                                                    {
                                                                        IsLoading = false;
                                                                    });
                                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                                                }

                                                                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
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
                                                                        
                                                                        
                                                                        await Task.Run(() =>
                                                                        {
                                                                            IsLoading = false;
                                                                        });
                                                                    }
                                                                }
                                                                AttachmentCount++;
                                                                filterList();
                                                                //CloneAttachmentList(VatAttachmentsListtofilter);
                                                                CloneAttachmentList(VatAttachmentsList);
                                                                // TotalAttachmentSize += AttachmentSize;
                                                                AttachmentName = string.Empty;
                                                            }
                                                            else
                                                            {
                                                                AttachmentName = string.Empty;
                                                                await Task.Run(() =>
                                                                {
                                                                    IsLoading = false;
                                                                });
                                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            AttachmentName = string.Empty;
                                                            await Task.Run(() =>
                                                            {
                                                                IsLoading = false;
                                                            });
                                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });
                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });
                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });
                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan1MB));

                                            }
                                        }
                                        else if (IsComeForWhichAttachment == WhichAttachment.VATInstalment || IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy || IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice || IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment || IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo || IsComeForWhichAttachment == WhichAttachment.TINOutletDeregisterAttachment)
                                        {
                                            if (fileSize <= 10)
                                            {
                                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                                {
                                                    bool IsAttachmentPresent = false;
                                                    foreach (Attachment ItemA in AttachmentsList.results)
                                                    {
                                                        var fileName = WebUtility.UrlEncode(AttachmentName);
                                                        if (fileName == ItemA.Filename && ItemA.Dotyp == DocTypeString)
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }
                                                    }
                                                    if (IsAttachmentPresent == false)
                                                    {
                                                        string attachmentType = UtilityManager.GetContentType(Extention);
                                                        AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType, DocTypeString);
                                                        if (_attachment != null && _attachment.d != null)
                                                        {
                                                            AttachmentName = string.Empty;
                                                            /*TimeZone localZone = TimeZone.CurrentTimeZone;
                                                            string standardName = localZone.DaylightName;*/

                                                            TimeZoneInfo localZone = TimeZoneInfo.Local;
                                                            string standardName = localZone.StandardName;

                                                            _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                            string uploadedDate = _attachment.d.Erfdt;
                                                            uploadedDate = uploadedDate.Replace("’", "");
                                                            uploadedDate = uploadedDate.Replace("‘", "");
                                                            uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                            _attachment.d.Erfdt = uploadedDate;
                                                            _attachment.d.Dotyp = DocTypeString;
                                                            AttachmentsList.results.Add(_attachment.d);
                                                            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
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
                                                                            //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            item.Erfdt = item.Erfdt;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (item.Erfdt != null)
                                                                        {
                                                                            //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                                                            if (DateTimeOffset.TryParseExact(item.Erfdt, "ddd, dd MMM yyyy HH:mm:ss 'GMT' zzz", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTimeOffset dateTimeOffset))
                                                                            {
                                                                                DateTime dateTime = dateTimeOffset.DateTime;
                                                                                item.Erfdt = dateTime.ToString("dd-MMMM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                                                                                Console.WriteLine(item.Erfdt);
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                {


                                                                    await Task.Run(() =>
                                                                    {
                                                                        IsLoading = false;
                                                                    });
                                                                }
                                                            }
                                                            AttachmentCount++;
                                                            filterList();
                                                            //CloneAttachmentList(VatAttachmentsListtofilter);
                                                            CloneAttachmentList(VatAttachmentsList);
                                                            // TotalAttachmentSize += AttachmentSize;
                                                            AttachmentName = string.Empty;
                                                        }
                                                        else
                                                        {
                                                            AttachmentName = string.Empty;
                                                            await Task.Run(() =>
                                                            {
                                                                IsLoading = false;
                                                            });
                                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });
                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });

                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });


                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan10MB));

                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDecimal(AttachmentSize) <= 5)
                                            {
                                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                                {
                                                    bool IsAttachmentPresent = false;
                                                    foreach (Attachment ItemA in AttachmentsList.results)
                                                    {
                                                        // var fileName = (WebUtility.UrlEncode(AttachmentName));

                                                        if (AttachmentName == ItemA.Filename && ItemA.Dotyp == DocTypeString)
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }

                                                        if (ItemA.Filename.Contains(" - ") && ItemA.Filename.Length > 2)
                                                        {
                                                            var splitStrings = ItemA.Filename.Split('-');

                                                            if (splitStrings.Count() > 0 && splitStrings[1].Trim() == AttachmentName)
                                                            {
                                                                IsAttachmentPresent = true;
                                                            }
                                                        }
                                                        if (!string.IsNullOrEmpty(ItemA.Filename) && !string.IsNullOrEmpty(AttachmentName) && ItemA.Filename.Replace("+", "").Replace("-", "").Replace("_", "").Replace(" ", "").Replace("1", "") == AttachmentName.Replace("+", "").Replace("-", "").Replace("_", "").Replace(" ", "").Replace("1", "") && ItemA.Dotyp == DocTypeString)
                                                        {
                                                            IsAttachmentPresent = true;
                                                        }
                                                    }


                                                    if (IsAttachmentPresent == false)
                                                    {
                                                        string attachmentType = UtilityManager.GetContentType(Extention);
                                                        AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType, DocTypeString);

                                                        if (_attachment != null && _attachment.d != null)
                                                        {
                                                            AttachmentName = string.Empty;
                                                            /* TimeZone localZone = TimeZone.CurrentTimeZone;
                                                             string standardName = localZone.DaylightName;*/
                                                            TimeZoneInfo localZone = TimeZoneInfo.Local;
                                                            string standardName = localZone.StandardName;
                                                            _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                            string uploadedDate = _attachment.d.Erfdt;
                                                            uploadedDate = uploadedDate.Replace("’", "");
                                                            uploadedDate = uploadedDate.Replace("‘", "");
                                                            uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                            _attachment.d.Erfdt = uploadedDate;
                                                            _attachment.d.Dotyp = DocTypeString;
                                                            AttachmentsList.results.Add(_attachment.d);
                                                            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
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
                                                                            //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            item.Erfdt = item.Erfdt;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (item.Erfdt != null)
                                                                        {
                                                                            //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                            //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                                                            item.Erfdt = item.Erfdt;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    await Task.Run(() =>
                                                                    {
                                                                        IsLoading = false;
                                                                    });
                                                                }
                                                            }
                                                            AttachmentCount++;
                                                            filterList();
                                                            //CloneAttachmentList(VatAttachmentsListtofilter);
                                                            CloneAttachmentList(VatAttachmentsList);
                                                            // TotalAttachmentSize += AttachmentSize;
                                                            AttachmentName = string.Empty;
                                                        }
                                                        else
                                                        {
                                                            AttachmentName = string.Empty;
                                                            await Task.Run(() =>
                                                            {
                                                                IsLoading = false;
                                                            });

                                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });

                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });

                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });
                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan5MB));

                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        await Task.Run(() =>
                                        {
                                            IsLoading = false;
                                        });

                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTotalFilesizeshouldnotbemorethan300MB));

                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                    });
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                            }
                        }
                    }
                    else
                    {
                        AttachmentName = string.Empty;
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoofallowedattachmentsare40));

                    }
                }
                catch (InternetException ex)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    });
                }
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }


        public async Task DeleteAttachment(bool result, VATAttachment attachment)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(() =>
                {
                    if (result)
                    {
                        // int indexToReduceTheSize = GetDeletedAttachmentIndex(attachment);

                        string APiMethod = "Z_SAVE_ATTACH_SRV";

                        if (IsComeForWhichAttachment == WhichAttachment.VATInstalment)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod12Months || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod2Years || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriodOtherDoc)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VATDeregistration)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.TINDeregistration)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VatReviewAttachments)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VatReviewBankGuranteeAttach)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentBankStatements)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentFinance)
                        {
                            APiMethod = "Z_SAVE_ATTACH_SRV";
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentOne || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentTwo || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentThree ||
                        IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentFive || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentFive)
                        {
                            APiMethod = "ZDP_INDTAX_ATT_SRV";
                        }

                        string results = string.Empty;

                        if (IsComeForWhichAttachment == WhichAttachment.VATDeregistration)
                        {
                            results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(attachment.Filename, returnIdz, APiMethod, attachment.Doguid, attachment.Dotyp);
                        }
                        else
                        {
                            results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(attachment.Filename, returnIdz, APiMethod, attachment.Doguid);
                        }

                        if (results == "X")
                        {

                            Attachment listitem = (from itm in VatAttachmentsList
                                                   where itm.Doguid == attachment.Doguid.ToString()
                                                   select itm)
                                            .FirstOrDefault();

                            VATAttachment listitemTwo = (from itm in AttachmentList
                                                         where itm.Doguid == attachment.Doguid.ToString()
                                                         select itm)
                                            .FirstOrDefault();

                            if (listitem != null)
                                VatAttachmentsList.Remove(listitem);




                            if (listitemTwo != null)
                                AttachmentList.Remove(listitemTwo);

                            AttachmentsList.results.Remove(listitem);


                            //if (indexToReduceTheSize != -1)
                            // ReduceTotalAttachmentSize(indexToReduceTheSize);
                            AttachmentCount--;
                            filterList();
                            CloneAttachmentList(VatAttachmentsListtofilter);
                        }
                        filterList();
                        CloneAttachmentList(VatAttachmentsListtofilter);
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {
                IsLoading = false;
            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public void filterList()
        {
            try
            {
                if (AttachmentsList != null && AttachmentsList.results.Count != 0)
                {
                    List<Attachment> attachmentsList = new List<Attachment>();
                    foreach (var item in VatAttachmentsList)
                    {
                        if (item.Dotyp == DocTypeString)
                        {
                            attachmentsList.Add(item);
                        }
                    }
                    VatAttachmentsList = new ObservableCollection<Attachment>(attachmentsList);
                    // VatAttachmentsListtofilter= new ObservableCollection<Attachment>(attachmentsList); ;
                }
            }
            catch (Exception)
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
                    string APiMethod = "Z_SAVE_ATTACH_SRV";

                    if (IsComeForWhichAttachment == WhichAttachment.VATInstalment)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod12Months || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod2Years || IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriodOtherDoc)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VATDeregistration)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentBankStatements)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentFinance)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.TINDeregistration || IsComeForWhichAttachment == WhichAttachment.TINOutletDeregisterAttachment)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VatReviewAttachments)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                        AttachmentName = AttachmentName.Replace("-", "_").Replace(" ", "");
                        //string attName = "1SpaceAdded-SpaceAdded" + AttachmentName;
                        //AttachmentName = attName;
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VatReviewBankGuranteeAttach)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                        AttachmentName = AttachmentName.Replace("-", "_").Replace(" ", "");
                        //string attName = "1SpaceAdded-SpaceAdded" + AttachmentName;
                        //AttachmentName = attName;
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VatReviewLateFiling)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                        AttachmentName = AttachmentName.Replace("-", "_").Replace(" ", "");
                        //string attName = "1SpaceAdded-SpaceAdded" + AttachmentName;
                        //AttachmentName = attName;
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentOne || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentTwo || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentThree ||
                        IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentFive || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentSix || IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentSeven)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                    }

                    AttachmentRootOject attachment = await UploadAttachementsWebServiceManager.GAZTGenericSaveAttachment(attachmentByteData, AttachmentName, returnIdz, Doctype, contentType, APiMethod, OutletRef);

                    if (attachment != null && attachment.d != null)
                    {
                        attachmentSizeVisibility = true;
                        AttachmentSizeVisibility = attachmentSizeVisibility;

                        if (SizeList != null)
                            SizeList.Add(AttachmentSize);

                        AttachmentUploadedSize = GetAttachMentSize(SizeList);
                        TotalAttachmentSize = AttachmentUploadedSize;
                        if (!string.IsNullOrEmpty(OutletRef))
                        {
                            attachment.d.OutletRef = OutletRef;
                        }
                        _attachment = attachment;
                    }
                    else
                    {
                        _attachment = null;
                    }
                }
                catch (Exception)
                {
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
            return _attachment;

        }

        public decimal GetAttachMentSize(List<decimal> SizeList)
        {
            decimal TotalSize = 0;

            if (SizeList != null && SizeList.Count > 0)
            {
                foreach (decimal attachmentSize in SizeList)
                {
                    TotalSize = TotalSize + attachmentSize;
                }
            }

            return TotalSize;
        }


        public void CloneAttachmentList(ObservableCollection<Attachment> attachmentList)
        {
            if (IsComeForWhichAttachment == WhichAttachment.TINOutletDeregisterAttachment && (App.DeRegRequestStatus.Equals("E0016") || App.DeRegRequestStatus.Equals("IP021")))
            {
                ShowAddAttachments = false;
            }
            else
            {
                ShowAddAttachments = true;
            }
            if (attachmentList != null)
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
                    if (OutletRef == "X" && string.IsNullOrEmpty(vATAttachment.OutletRef))
                    {
                        vATAttachment.ShowDelete = false;
                    }
                    else
                    {
                        if (IsComeForWhichAttachment == WhichAttachment.TINOutletDeregisterAttachment)
                        {
                            var count = App.DeregisterAttachments.Where(x => x.Doguid == vATAttachment.Doguid).ToList().Count();
                            if (count > 0)
                            {
                                vATAttachment.ShowDelete = false;
                            }
                            else
                            {
                                vATAttachment.ShowDelete = true;
                            }
                        }
                        else
                        {
                            vATAttachment.ShowDelete = true;
                        }
                    }


                    list.Add(vATAttachment);
                }

                AttachmentList = list;
                if (AttachmentsList != null && AttachmentsList.results != null)
                    if (AttachmentsList.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results as List<Attachment>);
                        VatAttachmentsList = myCollection;
                    }
            }
        }
    }
}