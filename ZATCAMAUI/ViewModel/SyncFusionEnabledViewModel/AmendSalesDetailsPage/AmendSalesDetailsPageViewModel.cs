

using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Behaviors;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AmendSalesDetailsPage
{

    public class AmendSalesDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        public static SalesDetails SelectedSalesDetails = new SalesDetails();
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public Command OnZakatReturnDataUpdateClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnDeleteAttachmentClickedTapped { get; set; }
        public static bool IsSaveButtonPressed = false;
        public static string fbNum;

        public RootObject rootObject { get; set; }
        public bool isOnLoad = false;
        int attachmentCount = 0;
        string newValue = "";
        string changeReason = "";
        byte[] attachment;
        //DateTime U
        #endregion
        #region Property
        private ObservableCollection<ZakatAttachment> _zakatReturnAttachmentsList;
        public ObservableCollection<ZakatAttachment> ZakatReturnAttachmentsList
        {
            get
            {
                return _zakatReturnAttachmentsList;
            }
            set
            {
                _zakatReturnAttachmentsList = value;
                //if(ZakatReturnAttachmentsList != null)
                //{
                //    SetSaveButtonVisibility();
                //}
                OnPropertyChanged("ZakatReturnAttachmentsList");
            }
        }

        private bool _isSaveButtonEnable = false;
        public bool IsSaveButtonEnable
        {
            get
            {
                return _isSaveButtonEnable;
            }
            set
            {
                _isSaveButtonEnable = value;
                OnZakatReturnDataUpdateClicked.ChangeCanExecute();
                OnPropertyChanged("IsSaveButtonEnable");
            }
        }
        private string _newValue = string.Empty;
        public string NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                _newValue = value;
                IsValueChanged();
                SetSaveButtonVisibility();
                if (!string.IsNullOrEmpty(NewValue) && ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber == true)
                {
                    SelectedSalesDetails.NewValue = NewValue;
                }
                OnPropertyChanged("NewValue");
            }
        }
        private string _oldValue = string.Empty;
        public string OldValue
        {
            get
            {
                return _oldValue;
            }
            set
            {
                _oldValue = value;
                if (!string.IsNullOrEmpty(OldValue))
                {
                    SelectedSalesDetails.OldValue = _oldValue;
                }
                OnPropertyChanged("OldValue");
            }
        }
        private string _changeReason = "";
        public string ChangeReason
        {
            get
            {
                return _changeReason;
            }
            set
            {
                _changeReason = value;
                IsValueChanged();
                SetSaveButtonVisibility();
                if (!string.IsNullOrEmpty(NewValue))
                {
                    SelectedSalesDetails.ChangeReason = ChangeReason;
                    if (SelectedSalesDetails.IsReasonRequird)
                    {
                        SelectedSalesDetails.IsReasonRequird = false;
                    }
                }
                OnPropertyChanged("ChangeReason");
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
                if (AttachmentName != null)
                {
                    SelectedSalesDetails.AttchamentName = AttachmentName;
                    if (SelectedSalesDetails.IsAttachmentRequired)
                    {
                        SelectedSalesDetails.IsAttachmentRequired = false;
                    }
                }
                OnPropertyChanged("AttachmentName");
            }
        }
        private string _attachmentNumber = "";
        public string AttachmentNumber
        {
            get
            {
                return _attachmentNumber;
            }
            set
            {
                _attachmentNumber = value;
                if (_attachmentNumber != null)
                {
                    SelectedSalesDetails.AttchamentNumber = AttachmentNumber;
                }
                OnPropertyChanged("AttachmentNumber");
            }
        }
        private string _salesType = "";
        public string SalesType
        {
            get
            {
                return _salesType;
            }
            set
            {
                _salesType = value;
                if (_attachmentNumber != null)
                    OnPropertyChanged("SalesType");
            }
        }
        private Color _buttonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color ButtonBackgroundColor
        {
            get
            {
                return _buttonBackgroundColor;
            }
            set
            {
                _buttonBackgroundColor = value;
                OnPropertyChanged("ButtonBackgroundColor");
            }
        }
        #endregion
        #region Constructor
        public AmendSalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnZakatReturnDataUpdateClicked = new Command(ExecuteSaveClickCommand, CanExecuteSaveClickCommand);

            OnAttachmentClick = new Command(async () =>
            {
                try
                {
                    string[] filetypes;

                    filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentTypeStringForZakat();


                    PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);

                    var fileData = await FilePicker.PickAsync(options);
                    var stream = await fileData.OpenReadAsync();
                    var attachment = UtilityManager.ReadFully(stream as Stream);

                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await Task.Run(async () =>
                    {
                        try
                        {
                            AttachmentName = fileData.FileName;
                            if (fileData.FileName.Contains("."))
                            {
                                string[] ExtentionArray = AttachmentName.Split('.');
                                string Extention = ExtentionArray.Last();

                                string ContentType = UtilityManager.GetContentType(Extention);
                                bool isFileAlreayUploaded = IsFileAlreadyAttached(AttachmentName);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);

                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                {
                                    if (!isFileAlreayUploaded)
                                    {
                                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls")
                                        {
                                            if (attachment.Length < 5242880)
                                            {
                                                if (ZakatReturnAttachmentsList.Count < 5)
                                                {
                                                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(stream, AttachmentName, SalesDetailsPageViewModel.RetGuid, "Z12L", ContentType);
                                                    PopToRootPage();
                                                    if (_attachment != null && _attachment.d != null)
                                                    {
                                                        AttachmentName = string.Empty;
                                                        EstimateZakatAttachment _estimateZakatAttachment = new EstimateZakatAttachment();
                                                        _estimateZakatAttachment.Doguid = _attachment.d.Doguid;
                                                        _estimateZakatAttachment.Seqno = string.Empty;
                                                        _estimateZakatAttachment.SchGuid = string.Empty;
                                                        _estimateZakatAttachment.AttBy = string.Empty;// DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’");// string.Empty;
                                                        _estimateZakatAttachment.FileExtn = string.Empty;
                                                        _estimateZakatAttachment.ByPusr = string.Empty;
                                                        _estimateZakatAttachment.OutletRef = string.Empty;
                                                        _estimateZakatAttachment.Filename = _attachment.d.Filename;
                                                        _estimateZakatAttachment.RetGuid = _attachment.d.RetGuid;
                                                        _estimateZakatAttachment.Dotyp = "FZ01";
                                                        _estimateZakatAttachment.Mimetype = string.Empty;
                                                        _estimateZakatAttachment.DocUrl = _attachment.d.DocUrl;
                                                        _estimateZakatAttachment.DataVersion = string.Empty;
                                                        DateTime currentDate = DateTime.Now.ToLocalTime();
                                                        long ticks = currentDate.Ticks;
                                                        //_estimateZakatAttachment.UploadedDate = currentDate.ToString();
                                                        TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                                        string unixTime = span.TotalSeconds.ToString("N0");
                                                        unixTime = unixTime.Replace(",", "");
                                                        _estimateZakatAttachment.Erfdt = "" + "/Date(" + unixTime + ")/";// need to
                                                                                                                         //_estimateZakatAttachment.Erfdt = "/Date(" + unixTime + ")/";// need to
                                                        SelectedSalesDetails.estimateZakatAttachment.Add(_estimateZakatAttachment);
                                                        ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SelectedSalesDetails.estimateZakatAttachment);
                                                        IsValueChanged();// 1584987294.32348//1584987210.06955
                                                                         // ZakatReturnAttachmentsList.Add(_estimateZakatAttachment);
                                                    }
                                                    else
                                                    {
                                                        MainThread.BeginInvokeOnMainThread(async () =>
                                                        {
                                                            AttachmentName = string.Empty;
                                                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                            IsLoading = false;
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    MainThread.BeginInvokeOnMainThread(async () =>
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await _dialogService.ShowMessage(AppResources.ZZYoucannotuploadmorethan5attachment, AppResources.Alerts);
                                                        IsLoading = false;
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                MainThread.BeginInvokeOnMainThread(async () =>
                                                {
                                                    AttachmentName = string.Empty;
                                                    await _dialogService.ShowMessage(AppResources.ZZFilesizemustbelessthan5MB, AppResources.Alerts);
                                                    IsLoading = false;
                                                });
                                            }
                                        }
                                        else
                                        {
                                            MainThread.BeginInvokeOnMainThread(async () =>
                                            {
                                                AttachmentName = string.Empty;
                                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                                IsLoading = false;
                                            });
                                        }
                                    }
                                    else
                                    {
                                        MainThread.BeginInvokeOnMainThread(async () =>
                                        {
                                            await _dialogService.ShowMessage(AppResources.ZZFileWithTheSameNameAlreadyExists, AppResources.Alerts);
                                            IsLoading = false;
                                            AttachmentName = string.Empty;
                                        });
                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    IsLoading = false;
                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    });


                                }

                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    AttachmentName = string.Empty;
                                    await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                    IsLoading = false;
                                });
                            }
                        }
                        catch (InternetException ex)
                        {
                            IsLoading = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }
                    });
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
                catch (Exception)
                {


                }
            });
        }
        #endregion
        #region Method
        public bool CanExecuteSaveClickCommand(object obj)
        {
            return _isSaveButtonEnable;
        }
        public void ExecuteSaveClickCommand(object obj)
        {
            try
            {
                IsSaveButtonPressed = true;
                _navigationService.GoBack();
            }
            catch (Exception)
            {


            }
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
            SalesType = string.Empty;
            OldValue = string.Empty;
            NewValue = string.Empty;
            ChangeReason = string.Empty;
            IsLoading = false;
            AttachmentName = "";
            ZakatReturnAttachmentsList = new ObservableCollection<ZakatAttachment>();
            ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber = true;
        }
        public void OnLoad()
        {
            try
            {
                if (SelectedSalesDetails != null)
                {
                    IsSaveButtonEnable = false;
                    // IsValueChanged();
                    if (SelectedSalesDetails.estimateZakatAttachment != null)
                    {
                        attachmentCount = SelectedSalesDetails.estimateZakatAttachment.Count;
                    }
                    else
                    {
                        attachmentCount = 0;
                    }
                    SalesType = SelectedSalesDetails.SalesType;
                    OldValue = SelectedSalesDetails.InformationFromPartie;
                    if (!SelectedSalesDetails.InformationFromPartieToCompare.Equals(SelectedSalesDetails.InformationFromPartie))
                    {
                        NewValue = SelectedSalesDetails.InformationFromPartieToCompare;
                    }
                    else
                    {
                        NewValue = "";
                    }
                    newValue = SelectedSalesDetails.InformationFromPartieToCompare;
                    changeReason = SelectedSalesDetails.ChangeReason;
                    ChangeReason = SelectedSalesDetails.ChangeReason;
                    ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SelectedSalesDetails.estimateZakatAttachment);// SelectedSalesDetails.estimateZakatAttachment;// SetAttachmentListData(SelectedSalesDetails.estimateZakatAttachment);//
                }
                isOnLoad = false;
            }
            catch (Exception)
            {


            }

        }
        public async Task DeleteSelectedAttachment(string filename, string dougUD)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(() =>
            {
                try
                {
                    string res = WebServiceManager.GAZTDeleteEstimatedZAKATRAttachment(filename, dougUD);
                    PopToRootPage();
                    if (res.Equals("X") && ZakatReturnAttachmentsList.Count > 0)
                    {
                        for (int i = 0; i < ZakatReturnAttachmentsList.Count; i++)
                        {
                            if (ZakatReturnAttachmentsList[i].Doguid.Equals(dougUD))
                            {
                                ZakatReturnAttachmentsList.RemoveAt(i);
                                SelectedSalesDetails.estimateZakatAttachment.RemoveAt(i);
                            }
                        }
                        IsValueChanged();
                        SetSaveButtonVisibility();
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        private bool IsFileAlreadyAttached(string FileName)
        {
            bool isFileAlreadyAttached = false;
            if (SelectedSalesDetails.estimateZakatAttachment != null)
            {
                for (int i = 0; i < SelectedSalesDetails.estimateZakatAttachment.Count; i++)
                {
                    if (SelectedSalesDetails.estimateZakatAttachment[i].Filename.Equals(FileName))
                        isFileAlreadyAttached = true;
                    else
                        isFileAlreadyAttached = false;
                    if (isFileAlreadyAttached)
                        break;
                }
            }
            return isFileAlreadyAttached;
        }
        private void IsValueChanged()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (attachmentCount != SelectedSalesDetails.estimateZakatAttachment.Count)
                {
                    IsSaveButtonEnable = true;
                    ButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
                }
                else if (newValue != NewValue && !isOnLoad)
                {
                    IsSaveButtonEnable = true;
                    ButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
                }
                else if (changeReason != ChangeReason && !isOnLoad)// && !string.IsNullOrEmpty(ChangeReason)
                {
                    IsSaveButtonEnable = true;
                    ButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
                }
                else
                {
                    IsSaveButtonEnable = false;
                    ButtonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
            });
        }
        private ObservableCollection<ZakatAttachment> CloneAttachmmentListInLocalList(ObservableCollection<EstimateZakatAttachment> estimateZakatAttachment)
        {
            ObservableCollection<ZakatAttachment> _estimateZakatAttachment = new ObservableCollection<ZakatAttachment>();
            foreach (EstimateZakatAttachment obj in estimateZakatAttachment)
            {
                ZakatAttachment _zakatAttachment = new ZakatAttachment();
                try
                {
                    //  public Metadata3 __metadata { get; set; }
                    _zakatAttachment.RetGuid = obj.RetGuid;
                    _zakatAttachment.Seqno = obj.Seqno;
                    _zakatAttachment.Dotyp = obj.Dotyp;
                    _zakatAttachment.Doguid = obj.Doguid;
                    _zakatAttachment.AttBy = obj.AttBy;
                    _zakatAttachment.Filename = obj.Filename;
                    _zakatAttachment.FileExtn = obj.FileExtn;
                    _zakatAttachment.Mimetype = obj.Mimetype;
                    _zakatAttachment.ByPusr = obj.ByPusr;
                    _zakatAttachment.Erfdt = obj.Erfdt;
                    _zakatAttachment.DataVersion = obj.
                        DataVersion;
                    _zakatAttachment.DocUrl = obj.DocUrl;
                    _zakatAttachment.OutletRef = obj.OutletRef;
                    string unixDate = GetUnixDate(_zakatAttachment.Erfdt);
                    double unixTime = Convert.ToDouble(unixDate);
                    DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
                    DateTime dt = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    string standardName = localZone.DaylightName;
                    _zakatAttachment.UploadededDateToShow = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                    string uploadedDate = _zakatAttachment.UploadededDateToShow;
                    uploadedDate = uploadedDate.Replace("’", "");
                    uploadedDate = uploadedDate.Replace("‘", "");
                    uploadedDate = uploadedDate.Replace("UTC", "GMT");
                    _zakatAttachment.UploadededDateToShow = uploadedDate;
                    _estimateZakatAttachment.Add(_zakatAttachment);
                }
                catch (Exception)
                {
                }
            }
            return _estimateZakatAttachment;
        }
        private string GetUnixDate(string _erfdt)
        {
            int startIndex = 6;
            int lengthOfCharacter = _erfdt.Length - 8;
            string unixDateTime = _erfdt.Substring(startIndex, lengthOfCharacter);
            return unixDateTime;
        }
        private void SetSaveButtonVisibility()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (ZakatReturnAttachmentsList != null && ZakatReturnAttachmentsList.Count == 0 && string.IsNullOrEmpty(NewValue) && string.IsNullOrEmpty(ChangeReason))
                    {
                        ButtonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];
                        IsSaveButtonEnable = false;
                    }
                    else if (ZakatReturnAttachmentsList != null && ZakatReturnAttachmentsList.Count == 0 && string.IsNullOrEmpty(NewValue) && string.IsNullOrEmpty(ChangeReason))
                    {
                        ButtonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];
                        IsSaveButtonEnable = false;
                    }
                    else if (ZakatReturnAttachmentsList != null && ZakatReturnAttachmentsList.Count != 0 || !string.IsNullOrEmpty(NewValue) && NewValue.Equals(OldValue) || !string.IsNullOrEmpty(ChangeReason))
                    {
                        ButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
                        IsSaveButtonEnable = true;
                    }
                    //else if(NewValue.Length)


                    if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber == false)
                    {
                        ButtonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];
                        IsSaveButtonEnable = false;
                    }
                });
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
