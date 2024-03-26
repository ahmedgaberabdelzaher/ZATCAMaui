using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{
    [Preserve(AllMembers = true)]
    public class FilesUploadPopUpViewModel : BaseViewModel
    {
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoButtonClick { get; set; }

        public static Decimal AttachmentUploadedSize = 0;
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
                RaisePropertyChanged("IsImpoterAndExporter");
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
                RaisePropertyChanged("IsEnableSwitchToggled");
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
                RaisePropertyChanged("IsEnableSwitchToggledForButton");
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
                RaisePropertyChanged("TitleOne");
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
                RaisePropertyChanged("TitleTwo");
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
                RaisePropertyChanged("DmsType");
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
                RaisePropertyChanged("VatAttachmentCount");
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
                RaisePropertyChanged("VatAttachmentsListtofilter");
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

        public Attachments _attachments;
        public Attachments AttachmentsList
        {
            get
            {
                return _attachments;
            }
            set
            {
                _attachments = value;
                RaisePropertyChanged("AttachmentsList");
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
                RaisePropertyChanged("DocTypeString");
            }
        }

        public FilesUploadPopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            FileAttachments = new ObservableCollection<string>();
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                IsLoading = true;
                await AddAttachmentEx();
                IsLoading = false;
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


                        filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForAll();

                        if (IsComeForWhichAttachment == WhichAttachment.VATDeregistration ||
                        IsComeForWhichAttachment == WhichAttachment.TINDeregistration)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Where(x => (x.Dotyp == DocTypeString)).ToList().Count();

                                if (count >= 5)
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));

                                    //await PopupNavigation.Instance.PopAsync();
                                    return;
                                }

                            }
                        }
                        else if (IsComeForWhichAttachment == WhichAttachment.VATInstalment)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count();
                                if (count >= 5)
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));

                                    //await PopupNavigation.Instance.PopAsync();
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

                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATReviewAttachmentLimitReached));

                                    //await PopupNavigation.Instance.PopAsync();
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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));

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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.OldZakatInstalmentAttachmentLimitReached));

                                    //await PopupNavigation.Instance.PopAsync();
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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZakatInstalmentAttachmentLimitReached));

                                    //await PopupNavigation.Instance.PopAsync();
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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATReviewAttachmentLimitReached));

                                    //await PopupNavigation.Instance.PopAsync();
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

                                string[] ExtensionArray = fileData.FileName.Split('.');
                                string Extention = ExtensionArray.Last();
                                if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls")
                                {
                                    if (TotalAttachmentSize <= 300)
                                    {
                                        AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);
                                        double fileSize = (attachment.Length / 1024) / 1024.0;

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

                                                        if ((AttachmentName == ItemA.Filename) && (ItemA.Dotyp == DocTypeString))
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
                                                            TimeZone localZone = TimeZone.CurrentTimeZone;
                                                            string standardName = localZone.DaylightName;
                                                            _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                            string uploadedDate = _attachment.d.Erfdt;
                                                            uploadedDate = uploadedDate.Replace("’", "");
                                                            uploadedDate = uploadedDate.Replace("‘", "");
                                                            uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                            _attachment.d.Erfdt = uploadedDate;
                                                            _attachment.d.Dotyp = DocTypeString;
                                                            AttachmentsList.results.Add(_attachment.d);
                                                            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
                                                            Device.BeginInvokeOnMainThread(() =>
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
                                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });
                                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });
                                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });
                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan1MB));

                                            }
                                        }
                                        else if (IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy || IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice || IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment || IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountOne || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountTwo)
                                        {
                                            if (fileSize <= 10)
                                            {
                                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                                {
                                                    bool IsAttachmentPresent = false;
                                                    foreach (Attachment ItemA in AttachmentsList.results)
                                                    {
                                                        var fileName = (WebUtility.UrlEncode(AttachmentName));
                                                        if ((fileName == ItemA.Filename) && (ItemA.Dotyp == DocTypeString))
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
                                                            TimeZone localZone = TimeZone.CurrentTimeZone;
                                                            string standardName = localZone.DaylightName;
                                                            _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                            string uploadedDate = _attachment.d.Erfdt;
                                                            uploadedDate = uploadedDate.Replace("’", "");
                                                            uploadedDate = uploadedDate.Replace("‘", "");
                                                            uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                            _attachment.d.Erfdt = uploadedDate;
                                                            _attachment.d.Dotyp = DocTypeString;
                                                            AttachmentsList.results.Add(_attachment.d);
                                                            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
                                                            Device.BeginInvokeOnMainThread(() =>
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
                                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });
                                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });

                                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });


                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan10MB));

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

                                                        if ((AttachmentName == ItemA.Filename) && (ItemA.Dotyp == DocTypeString))
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
                                                        if (!String.IsNullOrEmpty(ItemA.Filename) && !String.IsNullOrEmpty(AttachmentName) && ItemA.Filename.Replace("+", "").Replace("-", "").Replace("_", "").Replace(" ", "").Replace("1", "") == AttachmentName.Replace("+", "").Replace("-", "").Replace("_", "").Replace(" ", "").Replace("1", "") && (ItemA.Dotyp == DocTypeString))
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
                                                            TimeZone localZone = TimeZone.CurrentTimeZone;
                                                            string standardName = localZone.DaylightName;
                                                            _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                            string uploadedDate = _attachment.d.Erfdt;
                                                            uploadedDate = uploadedDate.Replace("’", "");
                                                            uploadedDate = uploadedDate.Replace("‘", "");
                                                            uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                            _attachment.d.Erfdt = uploadedDate;
                                                            _attachment.d.Dotyp = DocTypeString;
                                                            AttachmentsList.results.Add(_attachment.d);
                                                            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(AttachmentsList.results);
                                                            Device.BeginInvokeOnMainThread(() =>
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

                                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await Task.Run(() =>
                                                        {
                                                            IsLoading = false;
                                                        });

                                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));

                                                    }
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });

                                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                }
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });
                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan5MB));

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

                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTotalFilesizeshouldnotbemorethan300MB));

                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                    });
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoofallowedattachmentsare40));

                    }
                }
                catch (InternetException ex)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                        else if (IsComeForWhichAttachment == WhichAttachment.IBANBankAccountOne || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountTwo)
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
                                            .FirstOrDefault<Attachment>();

                            VATAttachment listitemTwo = (from itm in AttachmentList
                                                         where itm.Doguid == attachment.Doguid.ToString()
                                                         select itm)
                                            .FirstOrDefault<VATAttachment>();

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
                    else if (IsComeForWhichAttachment == WhichAttachment.IBANBankAccountOne || IsComeForWhichAttachment == WhichAttachment.IBANBankAccountTwo)
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
                    else if (IsComeForWhichAttachment == WhichAttachment.TINDeregistration)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VatReviewAttachments)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                        AttachmentName = AttachmentName.Replace("-", "_").Replace(" ", "");
                        string attName = "1SpaceAdded-SpaceAdded" + AttachmentName;
                        AttachmentName = attName;
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.VatReviewBankGuranteeAttach)
                    {
                        APiMethod = "ZDP_INDTAX_ATT_SRV";
                        AttachmentName = AttachmentName.Replace("-", "_").Replace(" ", "");
                        string attName = "1SpaceAdded-SpaceAdded" + AttachmentName;
                        AttachmentName = attName;
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }
                    else if (IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
                    {
                        APiMethod = "Z_SAVE_ATTACH_SRV";
                    }

                    AttachmentRootOject attachment = await UploadAttachementsWebServiceManager.GAZTGenericSaveAttachment(attachmentByteData, AttachmentName, returnIdz, Doctype, contentType, APiMethod);

                    if (attachment != null && attachment.d != null)
                    {
                        attachmentSizeVisibility = true;
                        AttachmentSizeVisibility = attachmentSizeVisibility;

                        if (SizeList != null)
                            SizeList.Add(AttachmentSize);

                        AttachmentUploadedSize = GetAttachMentSize(SizeList);
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