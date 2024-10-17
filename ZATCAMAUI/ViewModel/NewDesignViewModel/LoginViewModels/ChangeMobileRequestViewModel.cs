using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;


using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ErrorMessage;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Core.Interfaces;
using System.Text;
using Microsoft.Maui.Controls.Shapes;
using ZATCAMAUI.Core.CustomControls;
using Syncfusion.Maui.ProgressBar;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels
{
    public class ChangeMobileRequestViewModel : BaseViewModel
    {


        public ICommand ShowIdTypePicker { get; set; }
        public ICommand ContinueBtnTapped { get; set; }
        public ICommand ContinueBtnTapped2 { get; set; }
        public ICommand CancelBtnTapped { get; set; }
        public ICommand SendOTPBtnCliked { get; set; }
        public ICommand VerifyOTPBtnCliked { get; set; }
        public ICommand OnDeleteAttachmentButtonClick { get; set; }
        public ICommand OnTransferCopyOfCRChoiceButtonClick { get; set; }
        public ICommand SubmitBtnClicked { get; set; }
        public ICommand OnResendOTPClicked { get; set; }
        public ICommand NafathClicked { get; set; }
        public ICommand PrintFormClicked { get; set; }

        public ICommand GoToNextEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as GAZTBorderlessEntry;

                        switch (entry.ClassId)
                        {
                            case "2":
                                if (!string.IsNullOrEmpty(OTPFirstDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "3":
                                if (!string.IsNullOrEmpty(OTPSecondDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "4":
                                if (!string.IsNullOrEmpty(OTPThirdDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            default:
                                break;
                        }


                    }
                });
            }
        }
        public ICommand StartTimerCommand
        {
            get
            {
                return new Command(() =>
                {
                    StartOTPTimer();
                });
            }
        }
        public ICommand FocusEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as GAZTBorderlessEntry;

                        entry.Focus();
                    }
                });
            }
        }
        private string Captcha = string.Empty;
        private string GUID = string.Empty;
        public string NafathGUID = string.Empty;
        public bool IsAPICalledSuccessfully = false;
        public int OTPAttemptsCount = 0;



        public System.Timers.Timer otpTimer;
        public int countDownSeconds;

        enum PagesEnum
        {
            ManualTPDetails, //ShowMainForm
            MobileNumber,
            Attachments,
        }

        public bool MarkComplete { get; private set; } = false;

        private int _MaxIndex = 3;

        public int MaxIndex
        {
            get { return _MaxIndex; }
            set
            {
                if (_MaxIndex == value) return;

                _MaxIndex = value;
                OnPropertyChanged("MaxIndex");
            }
        }

        private int _currenrIndex = 0;

        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }

        private bool _showManualTPDetails;
        public bool ShowManualTPDetails
        {
            get
            {
                return _showManualTPDetails;
            }
            set
            {
                if (_showManualTPDetails == value) return;

                _showManualTPDetails = value;
                OnPropertyChanged("ShowManualTPDetails");
            }
        }

        private bool _showAutoTPDetails;
        public bool ShowAutoTPDetails
        {
            get
            {
                return _showAutoTPDetails;
            }
            set
            {
                if (_showAutoTPDetails == value) return;

                _showAutoTPDetails = value;
                OnPropertyChanged("ShowAutoTPDetails");
            }
        }

        private bool _showMobileNumberDetails;
        public bool ShowMobileNumberDetails
        {
            get
            {
                return _showMobileNumberDetails;
            }
            set
            {
                if (_showMobileNumberDetails == value) return;

                _showMobileNumberDetails = value;
                OnPropertyChanged("ShowMobileNumberDetails");
            }
        }

        private bool _showAttachmentDetails = false;
        public bool ShowAttachmentDetails
        {
            get
            {
                return _showAttachmentDetails;
            }
            set
            {
                if (_showAttachmentDetails == value) return;

                _showAttachmentDetails = value;
                OnPropertyChanged("_showAttachmentDetails");
            }
        }

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                OnPropertyChanged("LblCountDownTimer");
            }
        }

        private bool _isTINManual;
        public bool IsTINManual
        {
            get
            {
                return _isTINManual;
            }
            set
            {
                if (_isTINManual == value) return;

                _isTINManual = value;
                OnPropertyChanged("IsTINManual");
            }
        }



        private bool _isResendOTPEnabled;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                if (_isResendOTPEnabled == value) return;

                _isResendOTPEnabled = value;
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }
        private bool _showPrintFormButton = true;
        public bool ShowPrintFormButton
        {
            get
            {
                return _showPrintFormButton;
            }
            set
            {
                if (_showPrintFormButton == value) return;

                _showPrintFormButton = value;
                OnPropertyChanged("ShowPrintFormButton");
            }
        }


        private bool _showContinue2 = false;
        public bool ShowContinue2
        {
            get
            {
                return _showContinue2;
            }
            set
            {
                if (_showContinue2 == value) return;

                _showContinue2 = value;
                OnPropertyChanged("ShowContinue2");
            }
        }
        private string _attachmentLable = AppResources.ReAttachForm;
        public string AttachmentLable
        {
            get
            {
                return _attachmentLable;
            }
            set
            {
                if (_attachmentLable == value) return;

                _attachmentLable = value;
                OnPropertyChanged("AttachmentLable");
            }
        }



        private Color _resendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
        public Color ResendOTPTextColor
        {
            get
            {
                return _resendOTPTextColor;
            }
            set
            {
                if (_resendOTPTextColor == value) return;

                _resendOTPTextColor = value;
                OnPropertyChanged("ResendOTPTextColor");
            }
        }

        private ObservableCollection<InternationalMobileData> _countryCodesList = new ObservableCollection<InternationalMobileData>();
        public ObservableCollection<InternationalMobileData> CountryCodesList
        {
            get
            {
                return _countryCodesList;
            }
            set
            {
                if (_countryCodesList == value) return;

                _countryCodesList = value;
                OnPropertyChanged("CountryCodesList");
            }
        }

        private string _oTPFirstDigit;
        public string OTPFirstDigit
        {
            get
            {
                return _oTPFirstDigit;
            }
            set
            {
                if (_oTPFirstDigit == value) return;

                _oTPFirstDigit = value;
                if (!string.IsNullOrEmpty(OTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFirstDigit = string.Empty;
                    }

                }

                OnPropertyChanged("OTPFirstDigit");
            }
        }

        private string _OTPSecondDigit;
        public string OTPSecondDigit
        {
            get
            {
                return _OTPSecondDigit;
            }
            set
            {
                if (_OTPSecondDigit == value) return;

                _OTPSecondDigit = value;
                if (!string.IsNullOrEmpty(OTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPSecondDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPSecondDigit");
            }
        }

        private string _OTPThirdDigit;
        public string OTPThirdDigit
        {
            get
            {
                return _OTPThirdDigit;
            }
            set
            {
                if (_OTPThirdDigit == value) return;

                _OTPThirdDigit = value;
                if (!string.IsNullOrEmpty(OTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPThirdDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPThirdDigit");
            }
        }

        private string _OTPFourthDigit;
        public string OTPFourthDigit
        {
            get
            {
                return _OTPFourthDigit;
            }
            set
            {
                if (_OTPFourthDigit == value) return;

                _OTPFourthDigit = value;
                if (!string.IsNullOrEmpty(OTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFourthDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPFourthDigit");
            }
        }

        private string _enteredOTP = "";
        public string EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                if (_enteredOTP == value) return;

                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
            }
        }

        private bool _showSendOTP = true;
        public bool ShowSendOTP
        {
            get
            {
                return _showSendOTP;
            }
            set
            {
                if (_showSendOTP == value) return;

                _showSendOTP = value;
                OnPropertyChanged("ShowSendOTP");
            }
        }

        private bool _dissableSendOtp = true;
        public bool DissableSendOtp
        {
            get
            {
                return _dissableSendOtp;
            }
            set
            {
                if (_dissableSendOtp == value) return;

                _dissableSendOtp = value;
                OnPropertyChanged("DissableSendOtp");
            }
        }

        private bool showAttachmentSection = false;
        public bool ShowAttachmentSection
        {
            get
            {
                return showAttachmentSection;
            }
            set
            {
                if (showAttachmentSection == value) return;

                showAttachmentSection = value;
                OnPropertyChanged("ShowAttachmentSection");
            }
        }
        private bool _otpSection1 = false;
        public bool OtpSection1
        {
            get
            {
                return _otpSection1;
            }
            set
            {
                if (_otpSection1 == value) return;

                _otpSection1 = value;
                OnPropertyChanged("OtpSection1");
            }
        }

        private bool showOTPSection = false;
        public bool ShowOTPSection
        {
            get
            {
                return showOTPSection;
            }
            set
            {
                if (showOTPSection == value) return;

                showOTPSection = value;
                OnPropertyChanged("ShowOTPSection");
            }
        }

        private bool showSubmitForAutomatic = false;
        public bool ShowSubmitForAutomatic
        {
            get
            {
                return showSubmitForAutomatic;
            }
            set
            {
                if (showSubmitForAutomatic == value) return;

                showSubmitForAutomatic = value;
                OnPropertyChanged("ShowSubmitForAutomatic");
            }
        }


        private bool showOTPSuccessMessage = false;
        public bool ShowOTPSuccessMessage
        {
            get
            {
                return showOTPSuccessMessage;
            }
            set
            {
                if (showOTPSuccessMessage == value) return;

                showOTPSuccessMessage = value;
                OnPropertyChanged("ShowOTPSuccessMessage");
            }
        }

        private string _changeButtonLabel = "";
        public string ChangeButtonLabel
        {
            get
            {
                return _changeButtonLabel;
            }
            set
            {
                if (_changeButtonLabel == value) return;

                _changeButtonLabel = value;
                OnPropertyChanged("ChangeButtonLabel");
            }
        }

        private bool _enableContinue2 = true;
        public bool EnableContinue2
        {
            get
            {
                return _enableContinue2;
            }
            set
            {
                if (_enableContinue2 == value) return;

                _enableContinue2 = value;
                OnPropertyChanged("EnableContinue2");
            }
        }

        /// <summary>
        /// The Step progress bar item collection.
        /// </summary>
        private ObservableCollection<StepProgressBarItem> stepProgressItem;

        /// <summary>
        /// The Step progress bar item collection.
        /// </summary>
        public ObservableCollection<StepProgressBarItem> StepProgressItem
        {
            get
            {
                return stepProgressItem;
            }
            set
            {
                stepProgressItem = value;
            }
        }

        public ChangeMobileRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            ShowIdTypePicker = new Command(async () =>
            {
                if (ChangeMobModel?.d?.IDTYPSet != null)
                    await ShowIDTypeDialogAsync();
            });

            ContinueBtnTapped = new Command(async () =>
            {
                bool isvalid = await ProceedContinueAsync();
                if (isvalid)
                {
                    await GetCaptchAndGUID("CHMB");
                    await SubmitRequest();
                }
            });

            CancelBtnTapped = new Command(async () =>
            {
                if (NafathGUID.Length > 0)
                {
                    _navigationService.GoBack();
                }
                else
                {
                    ShowMainForm = true;
                    ShowOtpForm = false;
                    TxtMobileNumber = string.Empty;
                    EnteredOTP = string.Empty;
                    OTPFirstDigit = string.Empty;
                    OTPSecondDigit = string.Empty;
                    OTPThirdDigit = string.Empty;
                    OTPFourthDigit = string.Empty;
                    AttachedForms.Clear();
                    ESTLedge = false;
                }
            });

            ContinueBtnTapped2 = new Command(async () =>
            {
                bool isvalid = await IsVaslidTIn();
                if (isvalid)
                {
                    if (EnableContinue2 == false)
                        return;
                    await SubmitRequest();
                }
            });


            SendOTPBtnCliked = new Command(async () =>
            {
                try
                {
                    //if (!DissableSendOtp)
                    //{
                    //    return;
                    //}
                    //ShowSendOTP = false;
                    if (NafathGUID.Length > 0)
                    {
                        //ChangeMobModel.d.Captcha = Captcha;
                        //ChangeMobModel.d.OtpGuid = GUID;
                        if (ChangeMobModel.d.Tintyp.ToUpper() == "N")
                        {
                            FieldText = AppResources.ZTEReportCompanyName;
                            FieldValue = ChangeMobModel.d.Cmpnm;
                        }
                        else if (ChangeMobModel.d.Tintyp.ToUpper() == "A" || ChangeMobModel.d.Tintyp.ToUpper() == "R")
                        {
                            FieldText = AppResources.ZZCRName;
                            FieldValue = ChangeMobModel.d.Crname;
                        }
                    }
                    IsLoading = true;

                    ChangeMobModel.d.Operationz = "85";
                    changeMobModel.d.NewTlnmbr = TxtCountryCode.Replace("+", "00") + TxtMobileNumber;
                    var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);

                    IsLoading = false;
                    if (data.Item1 != null)
                    {
                        if (data.Item1.IsSuccessStatusCode)
                        {
                            if (data.Item2 != null)
                            {
                                ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);
                                ChangeMobModel.d = ChangeMobModel.result;
                                ShowOTPSection = true;
                                // DissableSendOtp = false;

                                StartOTPTimer();
                            }
                            else
                            {
                                await ShowErrorWithQuitAsync(data.Item2);
                            }
                        }
                        else
                        {
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("----->" + ex.StackTrace);
                }

            });
            VerifyOTPBtnCliked = new Command(async () =>
            {
                EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                if (EnteredOTP.Length != 4)
                {
                    await _dialogService.ShowMessage(AppResources.PleaseenterOTP, AppResources.Information);
                    return;
                }
                else
                {


                    ChangeMobModel.d.Operationz = "86";
                    ChangeMobModel.d.Otp = EnteredOTP;

                    //ShowOTPSection = false;
                    //ShowAttachmentSection = true;
                    IsLoading = true;
                    var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);
                    IsLoading = false;
                    if (data.Item1 != null)
                    {
                        if (data.Item1.IsSuccessStatusCode)
                        {
                            if (data.Item2 != null)
                            {
                                ShowOTPSuccessMessage = true;
                                //ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);
                                var response = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);

                                if (response != null && response.result == null)
                                {
                                    await ShowErrorWithQuitAsync(data.Item2);
                                }
                                else
                                {
                                    ChangeMobModel.d = response.result;
                                    ShowOTPSection = false;
                                    if (ChangeMobModel.d.Tintyp != "A")
                                    {
                                        //ChangeButtonLabel = AppResources.Submit;

                                        //ShowAttachmentSection = true;
                                        //ShowSubmitForAutomatic = false;
                                    }
                                    else
                                    {
                                        //ChangeButtonLabel = AppResources.ZVATChangeButton;

                                        //ShowAttachmentSection = false;
                                        //ShowSubmitForAutomatic = true;
                                    }

                                    if (NafathGUID.Length > 0)
                                    {
                                        ShowPrintFormButton = false;
                                        AttachmentLable = AppResources.AttachChamberOfCommerce;
                                    }
                                    ShowAttachmentDetailsPage();
                                }
                            }
                            else
                            {
                                await ShowErrorWithQuitAsync(data.Item2);
                            }
                        }
                        else
                        {
                            EnteredOTP = OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = string.Empty;

                            OTPAttemptsCount = OTPAttemptsCount + 1;
                            if (OTPAttemptsCount >= 3)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.OTPMaxAttempts));
                                TxtMobileNumber = string.Empty;
                                OTPFirstDigit = string.Empty;
                                OTPSecondDigit = string.Empty;
                                OTPThirdDigit = string.Empty;
                                OTPFourthDigit = string.Empty;
                                //DissableSendOtp = true;
                                StopTimer();
                            }
                            else
                            {
                                OTPFirstDigit = string.Empty;
                                OTPSecondDigit = string.Empty;
                                OTPThirdDigit = string.Empty;
                                OTPFourthDigit = string.Empty;
                                await ShowErrorWithQuitAsync(data.Item2);
                            }

                        }
                    }
                }

            });
            OnResendOTPClicked = new Command(async () =>
            {
                ChangeMobModel.d.Operationz = "85";
                changeMobModel.d.NewTlnmbr = TxtCountryCode.Replace("+", "00") + TxtMobileNumber;
                IsLoading = true;
                var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);
                IsLoading = false;
                if (data.Item1 != null)
                {
                    if (data.Item1.IsSuccessStatusCode)
                    {
                        if (data.Item2 != null)
                        {
                            ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);
                            ChangeMobModel.d = ChangeMobModel.result;
                            ShowOTPSection = true;
                            StartOTPTimer();
                        }
                        else
                        {
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                    else
                    {
                        await ShowErrorWithQuitAsync(data.Item2);
                    }
                }
            });
            OnTransferCopyOfCRChoiceButtonClick = new Command(async (type) =>
            {
                var typeValue = type as string;
                if (AttachedForms.Count < 1)
                {
                    if (string.IsNullOrEmpty(NafathGUID))
                    {
                        await AddAttachment("CHM2");
                    }
                    else
                    {
                        //TODO
                        await AddAttachment("CHM1");
                    }

                }

                else if (AttachedForms.Count != 1 || AttachedForms.Count != 1)
                {
                    await _dialogService.ShowError(AppResources.ZMaximumnoof5attachmentscanbeuploaded, AppResources.Information, AppResources.OKText, null);
                }

            });

            SubmitBtnClicked = new Command(async () =>
            {
                if (ShowAttachmentSection == true && AttachedForms.Count != 1)
                {
                    await _dialogService.ShowError(AppResources.AttachmentWarnMsg, AppResources.Information, AppResources.OKText, null);
                    return;
                }
                if (ESTLedge == false && ChangeMobModel.d.Tintyp != "A")
                {
                    await _dialogService.ShowError(AppResources.ESTValidatePledge, AppResources.Information, AppResources.OKText, null);
                    return;
                }
                else
                {
                    changeMobModel.d.Operationz = "01";
                    IsLoading = true;
                    var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);
                    IsLoading = false;
                    if (data.Item1.IsSuccessStatusCode)
                    {
                        ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);
                        ChangeMobModel.d = ChangeMobModel.result;
                        if (ChangeMobModel?.d != null)
                        {
                            await _dialogService.ShowMessage(AppResources.ChangeMobSuccessMsg + " - " + ChangeMobModel.d.Fbnumz, AppResources.Information);
                            _navigationService.GoBack();
                        }
                        else
                        {
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                    else
                    {
                        await ShowErrorWithQuitAsync(data.Item2);
                    }
                }
            });

            NafathClicked = new Command(async () =>
            {
                try
                {
                    App.GUIDFrChangeMob = "";
                    _navigationService.GoBack();
                  await  _navigationService.NavigateTo(App.NafathLoginView, ZATCAConstants.NAFATH_COMPANY_CHANGE_MOBILE_NUMBER);
                }
                catch (Exception)
                {
                }


            });

            PrintFormClicked = new Command( () =>
            {
                try
                {
                    PrintForm(ChangeMobModel.d.Fbnumz);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            });

            OnDeleteAttachmentButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment));
            AddPageIndexes();
        }

        private void AddPageIndexes()
        {
            stepProgressItem = new ObservableCollection<StepProgressBarItem>
            {
                new StepProgressBarItem() { PrimaryText = "" },
                new StepProgressBarItem() { PrimaryText = "" },
                new StepProgressBarItem() { PrimaryText = "" },
                new StepProgressBarItem() { PrimaryText = "" }
            };
        }

        private async Task<bool> IsVaslidTIn()
        {
            if (!string.IsNullOrEmpty(TinNumber))
            {
                if (TinNumber.StartsWith("3"))
                {
                    if (TinNumber.Length == 10)
                    {
                        return true;
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinNumberValidation));
                    }
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZTINnumberhastostartwithnumber3));
                }
            }
            else
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheTINNumber));
            }
            return false;
        }

        private async Task AddAttachment(string docType)
        {
            try
            {
                string[] filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForTaxEvasion();
                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                var attachmentByte = UtilityManager.ReadFully(stream as Stream);

                if (attachmentByte != null)
                {
                    //var attachmentByte = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;
                    bool isFileAlreayUploaded = AttachedForms.Count > 0 ? true : false;
                    if (!isFileAlreayUploaded)
                    {
                        float sizemb = (attachmentByte.Length / 1024f) / 1024f;
                        decimal attachmentSize = 0;
                        attachmentSize = attachmentSize + (Decimal)sizemb;

                        if (fileData.FileName.Contains("."))
                        {
                            string[] ExtentionArray = fileData.FileName.Split('.');
                            string Extention = ExtentionArray.Last();

                            if ((!ShowPrintFormButton && (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")) || (ShowPrintFormButton && Extention.ToLower() == "pdf"))
                            {
                                attachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 2);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 4);
                                if (Convert.ToDecimal(attachmentSize) <= 10)
                                {
                                    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                    {
                                        try
                                        {
                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                            await SaveAttachment(attachmentByte, attachmentName, docType, attachmentType);
                                        }
                                        catch (Exception ex)
                                        {


                                        }
                                    }
                                    else
                                    {
                                        attachmentName = string.Empty;
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
                                        await _dialogService.ShowMessage(AppResources.ESTAttachmentSizeNotfication, AppResources.Information);
                                    });
                                }

                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.AttachmentInstructionPrintForm, AppResources.Information);
                                });
                            }
                        }
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));
                            IsLoading = false;
                        });
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);



            }
        }
        private async Task SaveAttachment(byte[] attachmentByteData, string fileName, string docType, string contentType)
        {
            try
            {
                IsLoading = true;
                string CRLicenseNo = string.Empty;
                Attachment dd = await WebServiceManager.ChangeMobileNumberAttachment(attachmentByteData, fileName, ChangeMobModel?.d.ReturnId, docType, contentType);
                if (dd != null)
                {
                    dd.Dotyp = docType;
                    AttachedForms.Add(dd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);


            }
            finally
            {
                IsLoading = false;
            }
        }
        private async void OnDeleteAttachment(Attachment item, string docType)
        {

        }

        private bool _eSTLedge = false;
        public bool ESTLedge
        {
            get => _eSTLedge;
            set
            {

                if (_eSTLedge == value) return;

                _eSTLedge = value;
                //   IsDeclarationBtnEnabled = _eSTLedge;
                OnPropertyChanged(nameof(ESTLedge));

            }
        }
        private void OnDeleteAttachment(Attachment attachment)
        {

        }

        private async Task SubmitRequest()
        {
            try
            {
                IsLoading = true;
                ChangeMobModel.d.Gpart = TinNumber;
                ChangeMobModel.d.Mgrnm = ManagerName;
                ChangeMobModel.d.OtpGuid = GUID;
                ChangeMobModel.d.Operationz = "07";
                ChangeMobModel.d.Tintyp = "N";
                ChangeMobModel.d.Mgrid = ManagerId;
                ChangeMobModel.d.Langz = UtilityManager.GetLanguageParameter();
                if (NafathGUID.Length > 0)
                {
                    ChangeMobModel.d.Tintyp = "R";
                    ChangeMobModel.d.Idtyp = "";
                }
                else
                {
                    ChangeMobModel.d.Idtyp = GetSelectedIDtype();
                }

                ChangeMobModel.d.Captcha = Captcha;

                ChangeMobModel.d.ATTACHSet = new List<object>();
                ChangeMobModel.d.NOTESSet = new List<object>();
                ChangeMobModel.d.MCERRORSet = new List<ErrorTypes>();
                ChangeMobModel.d.IDTYPSet = new List<IDTypes>();

                var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);
                IsLoading = false;
                if (data.Item1 != null)
                {
                    if (data.Item1.IsSuccessStatusCode)
                    {
                        if (data.Item2 != null)
                        {
                            var resultObj = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);

                            if (resultObj != null && resultObj?.result == null)
                            {
                                await GetCaptchAndGUID("CHMB", GUID, Captcha);
                                await ShowErrorWithQuitAsync(data.Item2);
                            }
                            else
                            {
                                if (resultObj != null && resultObj?.result != null)
                                {
                                    ChangeMobModel = resultObj;

                                    ChangeMobModel.d = ChangeMobModel.result;
                                    ManagerName = ChangeMobModel.d.Mgrnm;
                                    TinNumber = ChangeMobModel.d.Gpart;
                                    ManagerId = ChangeMobModel.d.Mgrid;
                                    ManagerName = ChangeMobModel.d.Mgrnm;

                                    if (ChangeMobModel.d.Tintyp.ToUpper() == "N")
                                    {
                                        FieldText = AppResources.ZTEReportCompanyName;
                                        FieldValue = ChangeMobModel.d.Cmpnm;
                                    }
                                    else if (ChangeMobModel.d.Tintyp.ToUpper() == "A" || ChangeMobModel.d.Tintyp.ToUpper() == "R")
                                    {
                                        FieldText = AppResources.ZZCRName;
                                        FieldValue = ChangeMobModel.d.Crname;
                                    }
                                    if (changeMobModel.d.McErrorFg.Equals("X") && ChangeMobModel.d.MCERRORSet != null && ChangeMobModel.d.MCERRORSet.Count > 0)
                                    {
                                        GUID = changeMobModel.d.OtpGuid;
                                        Captcha = changeMobModel.d.Captcha;
                                        await ShowMCIErrorAsync();
                                    }
                                    else
                                    {
                                        if (NafathGUID.Length > 0)
                                        {
                                            ChangeMobModel.d.Tintyp = "A"; // No attachment required for Automatic
                                        }

                                        ShowMobileDetailsPage();
                                        //ShowOtpForm = true;
                                        //OtpSection1 = true;



                                    }
                                }
                                else
                                {
                                    await GetCaptchAndGUID("CHMB", GUID, Captcha);
                                    await ShowErrorWithQuitAsync(data.Item2);
                                }
                            }
                        }
                        else
                        {
                            await GetCaptchAndGUID("CHMB", GUID, Captcha);
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                    else
                    {
                        await GetCaptchAndGUID("CHMB", GUID, Captcha);
                        await ShowErrorWithQuitAsync(data.Item2);
                    }
                }
                else
                {
                    //TODO
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                });
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ShowMCIErrorAsync()
        {
            try
            {
                string message = AppResources.MCIErrorTitleMsg + "\n";
                foreach (var item in ChangeMobModel.d.MCERRORSet)
                {
                    message += item.Message + "\n";
                }

                message += "\n" + AppResources.MCIErrorConfirmMsg;

                var result = await Application.Current.MainPage.DisplayAlert(AppResources.ZError, message, AppResources.Continue, AppResources.ZZZCancelText);

                if (result)
                {
                    EnableContinue2 = false;
                    ChangeMobModel.d.Operationz = "08";
                    var data = await WebServiceManager.SaveChangeMobileNumberAsync(ChangeMobModel);
                    IsLoading = false;
                    if (data.Item1 != null)
                    {
                        if (data.Item1.IsSuccessStatusCode)
                        {
                            if (data.Item2 != null)
                            {
                                var resultObj = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);

                                if (resultObj != null && resultObj?.result == null)
                                {
                                    await ShowErrorWithQuitAsync(data.Item2);
                                }
                                else
                                {
                                    ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);

                                    ChangeMobModel.d = ChangeMobModel.result;
                                    ShowMobileDetailsPage();
                                    //ShowOtpForm = true;
                                    //OtpSection1 = true;
                                    //ShowMainForm = false;
                                    //EnableContinue2 = false;
                                }
                            }
                        }
                        else
                        {
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        private string GetSelectedIDtype()
        {
            return CopyIDTypes.Where(x => x.Text == SelectedIDType).FirstOrDefault().Idtyp;
        }

        private async Task<bool> ProceedContinueAsync()
        {
            if (!string.IsNullOrEmpty(ManagerName.Trim()))
            {
                if (!string.IsNullOrEmpty(SelectedIDType))
                {
                    if (!string.IsNullOrEmpty(ManagerId))
                    {
                        if (!string.IsNullOrEmpty(TinNumber))
                        {
                            if (TinNumber.StartsWith("3"))
                            {
                                if (TinNumber.Length == 10)
                                {
                                    return true;
                                }
                                else
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinNumberValidation));
                                }
                            }
                            else
                            {

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZTINnumberhastostartwithnumber3));
                            }
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheTINNumber));
                        }
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZPleaseentertheIDNumbervalue));
                    }
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertIDType));
                }
            }
            else
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ManagerNameError));
            }
            return false;
        }

        private bool _isArabic = false;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                if (_isArabic == value) return;
                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }

        private ChangeMobileNumberModel changeMobModel;
        public ChangeMobileNumberModel ChangeMobModel
        {
            get
            {
                return changeMobModel;
            }
            set
            {
                if (changeMobModel == value) return;
                changeMobModel = value;
                OnPropertyChanged("ChangeMobModel");
            }
        }

        private GenericPickerModel _pickerModelIDType { get; set; }
        public GenericPickerModel PickerModelIDType
        {
            get { return _pickerModelIDType; }
            set
            {
                if (_pickerModelIDType == value) return;

                _pickerModelIDType = value;
                OnPropertyChanged("PickerModelIDType");
            }
        }

        private string _selectedIDType { get; set; } = string.Empty;
        public string SelectedIDType
        {
            get { return _selectedIDType; }
            set
            {
                if (_selectedIDType == value) return;
                _selectedIDType = value;
                OnPropertyChanged("SelectedIDType");
            }
        }

        private string _manageName { get; set; } = string.Empty;
        public string ManagerName
        {
            get { return _manageName; }
            set
            {
                if (_manageName == value) return;
                _manageName = value;
                OnPropertyChanged("ManagerName");
            }
        }
        private string _manageId { get; set; } = string.Empty;
        public string ManagerId
        {
            get { return _manageId; }
            set
            {
                if (_manageId == value) return;
                _manageId = value;
                OnPropertyChanged("ManagerId");
            }
        }

        private string _fieldText { get; set; }
        public string FieldText
        {
            get { return _fieldText; }
            set
            {
                if (_fieldText == value) return;
                _fieldText = value;
                OnPropertyChanged("FieldText");
            }
        }
        private string _fieldValue { get; set; }
        public string FieldValue
        {
            get { return _fieldValue; }
            set
            {
                if (_fieldValue == value) return;
                _fieldValue = value;
                OnPropertyChanged("FieldValue");
            }
        }
        private string _tinNumber { get; set; } = string.Empty;//"3300000716";//"3311739388";//
        public string TinNumber
        {
            get { return _tinNumber; }
            set
            {
                if (_tinNumber == value) return;
                _tinNumber = value;
                OnPropertyChanged("TinNumber");
            }
        }


        private bool _showMainForm = false;
        public bool ShowMainForm
        {
            get
            {
                return _showMainForm;
            }
            set
            {
                if (_showMainForm == value) return;
                _showMainForm = value;
                OnPropertyChanged("ShowMainForm");
            }
        }

        private bool _showOtpForm = false;
        public bool ShowOtpForm
        {
            get
            {
                return _showOtpForm;
            }
            set
            {
                if (_showOtpForm == value) return;
                _showOtpForm = value;
                OnPropertyChanged("ShowOtpForm");
            }
        }
        private bool _isAllValidContactDataEnteredMobileNbr = false;
        public bool IsAllValidContactDataEnteredMobileNbr
        {
            get
            {
                return _isAllValidContactDataEnteredMobileNbr;
            }
            set
            {
                _isAllValidContactDataEnteredMobileNbr = value;
                OnPropertyChanged("IsAllValidContactDataEnteredMobileNbr");
            }
        }

        private string _txtMobileNumberwithCountryCode = "";
        public string TxtMobileNumberwithCountryCode
        {
            get
            {
                return _txtMobileNumberwithCountryCode;
            }
            set
            {
                if (_txtMobileNumberwithCountryCode == value) return;

                _txtMobileNumberwithCountryCode = value;
                OnPropertyChanged("TxtMobileNumberwithCountryCode");
            }
        }
        private string _txtMobileNumber = string.Empty;
        public string TxtMobileNumber
        {
            get
            {
                return _txtMobileNumber;
            }
            set
            {
                if (_txtMobileNumber == value) return;

                _txtMobileNumber = value;
                OnPropertyChanged("TxtMobileNumber");
            }
        }

        private string _mobileCountryCode = string.Empty;
        public string MobileCountryCode
        {
            get
            {
                return _mobileCountryCode;
            }
            set
            {
                if (_mobileCountryCode == value) return;

                _mobileCountryCode = value;
                OnPropertyChanged("MobileCountryCode");
            }
        }

        private string _txtCountryCode = "+966";
        public string TxtCountryCode
        {
            get
            {
                return _txtCountryCode;
            }
            set
            {
                if (_txtCountryCode == value) return;

                _txtCountryCode = value;
                if (_txtCountryCode != null)
                {
                    MaxDigids = (14 - _txtCountryCode.Length).ToString();
                }
                else
                {
                    MaxDigids = "15";
                }
                OnPropertyChanged("TxtCountryCode");
            }
        }
        private string _maxDigids = "9";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                if (_maxDigids == value) return;

                _maxDigids = value;
                OnPropertyChanged("MaxDigids");
            }
        }
        private ObservableCollection<Attachment> _attachedForms = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> AttachedForms
        {
            get => _attachedForms;
            set
            {
                if (_attachedForms == value) return;

                if (value != null)
                {
                    _attachedForms = value;
                    OnPropertyChanged(nameof(AttachedForms));
                }
            }
        }

        private ObservableCollection<IDTypes> _copyIDTypes = new ObservableCollection<IDTypes>();
        public ObservableCollection<IDTypes> CopyIDTypes
        {
            get => _copyIDTypes;
            set
            {
                if (_copyIDTypes == value) return;

                if (value != null)
                {
                    _copyIDTypes = value;
                    OnPropertyChanged(nameof(CopyIDTypes));
                }
            }
        }

        public void ShowTpDetailsPage()
        {
            CurrentIndex = 1;
            if (NafathGUID.Length > 0)
            {
                ShowAutoTPDetails = true;
                ShowManualTPDetails = false;
            }
            else
            {
                ShowManualTPDetails = true;
                ShowAutoTPDetails = false;
            }
            ShowMobileNumberDetails = false;
            ShowAttachmentDetails = false;
        }

        public void ShowMobileDetailsPage()
        {
            CurrentIndex = 2;
            ShowManualTPDetails = false;
            ShowAutoTPDetails = false;
            ShowMobileNumberDetails = true;
            ShowAttachmentDetails = false;
        }

        public void ShowAttachmentDetailsPage()
        {
            CurrentIndex = 3;
            ShowManualTPDetails = false;
            ShowAutoTPDetails = false;
            ShowMobileNumberDetails = false;
            if (ChangeMobModel.d.Tintyp != "A")
            {
                ShowAttachmentSection = true;
                ShowSubmitForAutomatic = false;

            }
            else
            {
                ShowAttachmentSection = false;
                ShowSubmitForAutomatic = true;
            }
            //ShowAttachmentDetails = true;
        }

        private async Task ShowIDTypeDialogAsync()
        {
            try
            {
                setIdTypePickerModel();
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelIDType));

            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void setIdTypePickerModel()
        {
            if (PickerModelIDType != null)
            {
                PickerModelIDType = null;
            }
            var list = new List<string>();

            foreach (IDTypes dropdown in ChangeMobModel.d.IDTYPSet)
            {
                try
                {
                    list.Add(dropdown.Text);
                }
                catch (Exception)
                {

                }
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "EntityTypePicker";
            genericPickerModel.PageCode = 1;
            PickerModelIDType = genericPickerModel;
            SelectedIDType = "";
        }

        internal async Task GetIdTypesAsync(string guid, string idNumber = "")
        {
            NafathChangeMobileNumberModelResponse nafathChmbResponse = null;
            try
            {
                IsLoading = true;
                if (string.IsNullOrEmpty(guid))
                {
                    getIdTypesData(guid);
                }
                else
                {
                    NafathChangeMobileNumberModel model = new NafathChangeMobileNumberModel()
                    {
                        Guid = string.Empty,
                        Idnumber = idNumber,
                        Lang = WebServiceManager.GetLangZParameterAREN(),
                        Scrid = string.Empty,
                        Chmb = "X",
                        Str = string.Empty,
                        GuidNf = guid,
                        mobileExtensions = new List<MOB_EXTENSet>(),
                        taxpayerRegistrationTypes = new List<TP_REGTYPSet>()
                    };
                    nafathChmbResponse = await WebServiceManager.NafathChangeMobileNumber(model);
                    IsLoading = false;
                    if (nafathChmbResponse != null && nafathChmbResponse.d != null)
                    {
                        getIdTypesData(nafathChmbResponse?.d?.Guid);
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }


        private async void getIdTypesData(string guidNf)
        {
            IsLoading = true;
            var data = await WebServiceManager.GetIDTypesForChangeMobNumber(guidNf);
            IsLoading = false;
            if (data?.Item1 != null)
            {
                if (data.Item1.IsSuccessStatusCode)
                {
                    if (data.Item2 != null)
                    {
                        ChangeMobModel = JsonConvert.DeserializeObject<ChangeMobileNumberModel>(data.Item2);

                        ManagerName = ChangeMobModel.d.Mgrnm;
                        ManagerId = ChangeMobModel.d.Mgrid;
                        foreach (var item in ChangeMobModel.d.IDTYPSet)
                        {
                            CopyIDTypes.Add(item);
                        }
                    }
                }
                else if (data?.Item1.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await ShowErrorWithQuitAsync(data.Item2);
                }
                else
                {
                    await ShowErrorWithQuitAsync(data.Item2);
                }
            }
        }

        private async Task ShowErrorWithQuitAsync(string item2)
        {
            SignupErrorModelRootObject errorMesg = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(item2);
            StringBuilder Message = new StringBuilder();
            foreach (ErrorDetail itemerror in errorMesg.header.moreInformation.errorDetails)
            {
                Message.Append(itemerror.message);
            }
            String WithReplacedString = Message.ToString().Replace("An exception was raised", string.Empty);
            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WithReplacedString));
        }

        //public async Task GetCaptchAndGUID(string CaptchaRequestCode, string guid = "", string captchaCode = "")
        //{
        //    try
        //    {

        //        IsLoading = true;

        //        string lang = UtilityManager.GetLanguageParameter();
        //        string st = ZATCAConstants.CaptchaAndGUID;
        //        string type = "ZDP_CREATE_CAPTCHA_SRV.Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
        //        GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();
        //        Models.Metadata metadata = new Models.Metadata();
        //        metadata.id = st;
        //        metadata.uri = st;
        //        metadata.type = type;

        //        GetCaptcha d = new GetCaptcha();
        //        d.__metadata = metadata;
        //        d.captchaCode = captchaCode;
        //        d.GUID = guid;
        //        d.taxpayer = "";
        //        d.refresh = "";
        //        d.applicationName = CaptchaRequestCode;

        //        forgotPasswordOTP.result = d;
        //        forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(d);
        //        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

        //        if (forgotPasswordOTP?.result != null && !string.IsNullOrEmpty(forgotPasswordOTP.result.captchaCode))
        //        {
        //            Captcha = forgotPasswordOTP.result.captchaCode;
        //            GUID = forgotPasswordOTP.result.GUID;
        //            IsAPICalledSuccessfully = true;
        //        }
        //        IsLoading = false;
        //    }

        //    catch (InternetException ex)
        //    {
        //        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

        //        //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
        //        await Task.Run(() =>
        //        {
        //            IsLoading = false;
        //            // UserIDLayoutVisibility = true;
        //        });
        //    }
        //    finally
        //    {
        //        IsLoading = false;
        //    }
        //}
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                    await MopupService.Instance.PopAsync();
                });
            }
        }

        internal async void InitCountryCodesAPI()
        {
            CountryCodesList = await WebServiceManager.GAZTGetMobileRegionDropdown();
        }
        private bool CheckOnlyNumber(char letter)
        {
            if ((letter >= 48 && letter <= 57))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            /*if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();*/


            if (countDownSeconds <= 9 && countDownSeconds > 0)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else if (countDownSeconds > 60)
            {
                int countDownSecondsL = countDownSeconds - 60;
                LblCountDownTimer = "1:" + countDownSecondsL.ToString();

                if (countDownSecondsL <= 9)
                    LblCountDownTimer = "1:0" + countDownSecondsL.ToString();
            }
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();


            // Stop timer
            if (countDownSeconds <= 0 && ShowOtpForm)
            {
                //ContinueButtonEnability = false;
                IsResendOTPEnabled = true;
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];

                otpTimer.Stop();
            }
        }

        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;
            LblCountDownTimer = "0." + countDownSeconds.ToString();

            otpTimer.Start();
        }

        public void PrintForm(string fbnumz)
        {
            //https://sapgatewayd.zatca.gov.sa/sap/opu/odata/SAP/Z_DOWN_FORM_EXT_SRV/cover_formSet(Utype='',Fbnum='40000006834')/$value
            String pdfUrl = ZATCAConstants.PrintFormUrl + fbnumz;
            ShowPdf(pdfUrl);
        }
        public void ShowPdf(string pdfUrl)
        {
            try
            {
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
            catch (Exception ex)
            {




            }
        }

        public void OnRentAttachmentDeleteButtonTapped(Attachment obj)
        {
            var delStatus = DeleteAttachment(obj.Filename, obj.RetGuid, obj.Dotyp, obj.Doguid);

            if (delStatus.ToLower() == "delete")
            {
                AttachedForms.Remove(obj);
            }
            else
            {
                ShowValidationPopup(AppResources.Somethingwentwrong);
            }
            //if (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count() == 0)
            //{
            //    IsVisbleRentAttachmentmentList = false;

            //}

        }
        private void ShowValidationPopup(string _message)
        {
            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(_message));
        }
        private string DeleteAttachment(string filename, string retGuid, string dotyp, string doguid)
        {
            return WebServiceManager.ChangeMobDeleteAttachment(filename, retGuid, dotyp, doguid);
        }

        internal void StopTimer()
        {
            if (otpTimer != null)
            {
                otpTimer.Stop();
            }

        }
        public async Task GetCaptchAndGUID(string CaptchaRequestCode, string guid = "", string captchaCode = "")
        {
            try
            {

                IsLoading = true;

                string lang = UtilityManager.GetLanguageParameter();
                string st = ZATCAConstants.CaptchaAndGUID;
                string type = "ZDP_CREATE_CAPTCHA_SRV.Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();
                Models.Metadata metadata = new Models.Metadata();
                metadata.id = st;
                metadata.uri = st;
                metadata.type = type;

                GetCaptcha d = new GetCaptcha();
                d.__metadata = metadata;
                d.captchaCode = captchaCode;
                d.GUID = guid;
                d.taxpayer = "";
                d.refresh = "";
                d.applicationName = CaptchaRequestCode;

                forgotPasswordOTP.result = d;
                forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(d);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (forgotPasswordOTP?.result != null && !string.IsNullOrEmpty(forgotPasswordOTP.result.captchaCode))
                {
                    Captcha = forgotPasswordOTP.result.captchaCode;
                    GUID = forgotPasswordOTP.result.GUID;
                    IsAPICalledSuccessfully = true;
                }
                IsLoading = false;
            }

            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                    // UserIDLayoutVisibility = true;
                });
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

