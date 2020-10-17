using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class FileAttachmentPopUpPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        public ICommand GoButtonClick { get; set; }

        public static Decimal AttachmentUploadedSize = 0;
        public static bool IsToBeFilled = false;
        public bool isImporter = false;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        byte[] attachment;
        public int NumberOfAttachmentComingFromServer = 0;

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

        private bool _isSwitchToggled = false;
        public bool IsSwitchToggled
        {
            get
            {
                return _isSwitchToggled;
            }
            set
            {
                _isSwitchToggled = value;
                if (IsImpoterAndExporter)
                {
                    if (_isSwitchToggled)
                    {
                      DocTypeString = "ZVTC";
                      filterList();
                        CloneAttachmentList(VatAttachmentsList);

                    }
                    else
                    {
                        DocTypeString = "ZVTB";
                        filterList();
                        CloneAttachmentList(VatAttachmentsList);

                    }
                }
                
                RaisePropertyChanged("IsSwitchToggled");
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

        public VATRegistrationDetails _vATRegistrationDetailsForAttach;
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

        public IsComeFromForAttachment _isComeFromForAttachment;
        public IsComeFromForAttachment IsComeFromForAttachment
        {
            get
            {
                return _isComeFromForAttachment;
            }
            set
            {
                _isComeFromForAttachment = value;
                RaisePropertyChanged("IsComeFromForAttachment");
            }
        }

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
   


        #endregion
        public FileAttachmentPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

                Debug.WriteLine("File = "+fname +"Attachment ="+attachment);
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
                        
                        var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                        if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                        {
                            if (VatAttachmentsList != null)
                            {
                                int count = VatAttachmentsList.Count;
                                if(count > 0)
                                {               
                                    if(isImporter)
                                    {

                                        count = VatAttachmentsList.Where(x => x.Dotyp.Equals("ZVTB")).Count();
                                    }
                                    else
                                    {
                                        count = VatAttachmentsList.Where(x => x.Dotyp.Equals("ZVTC")).Count();

                                    }
                                }
                               
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
                                                    if ((AttachmentName == ItemA.Filename) && (ItemA.Dotyp == DocTypeString))
                                                    {
                                                        IsAttachmentPresent = true;
                                                    }
                                                }
                                                if (IsAttachmentPresent == false)
                                                {
                                                    string attachmentType = UtilityManager.GetContentType(Extention);
                                                    AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType,DocTypeString);
                                                    
                                                    // await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationDataForAttch.d.ReturnIdz, "VTA0");
                                                    
                                                    PopToRootPage();
                                                    
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
                                                        //await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
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
                                                    //await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
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
                                                //await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
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
                                            if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                                            {
                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATAmendAttachmentSizeError));

                                            }
                                            else
                                            {
                                                //await _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);
                                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZFilesizeshouldnotbemorethan20MB));
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
                                        //await _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
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
                                    //await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
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
                                //await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
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
                        //await _dialogService.ShowMessage(AppResources.ZMaximumnoofallowedattachmentsare40, AppResources.Information);
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
                        //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    });
                }
            }
            catch (Exception ex)
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

        public void filterList()
        {
            try
            {
                if (VATRegistrationDetailsForAttach != null && VATRegistrationDetailsForAttach.d != null && VATRegistrationDetailsForAttach.d.ATTDETSet != null && VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
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
            catch (Exception ex)
            {

            }
        }

        private async Task<AttachmentRootOject> SaveAttachment(byte[] attachmentByteData, string contentType,string Doctype)
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
                    if (IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                    {
                        AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachmentForFD(attachmentByteData, AttachmentName, VATRegistrationDetailsForAttach.d.ReturnIdz, Doctype, contentType);

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
                    else
                    {
                        AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachmentByteData, AttachmentName, VATRegistrationDetailsForAttach.d.ReturnIdz, Doctype, contentType);

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
        public decimal GetAttachMentSize(List<decimal> SizeList)
        {
            decimal TotalSize = 0;

            if (SizeList!=null && SizeList.Count>0)
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
                if (VATRegistrationDetailsForAttach != null && VATRegistrationDetailsForAttach.d.ATTDETSet != null && VATRegistrationDetailsForAttach.d.ATTDETSet.results != null)
                    if (VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
                        VatAttachmentsList = myCollection;
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
    }
}
