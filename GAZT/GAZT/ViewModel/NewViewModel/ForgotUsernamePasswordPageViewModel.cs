using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace GAZT.ViewModel.NewViewModel
{
    public class ForgotUsernamePasswordPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        public ICommand OnChangePasswordSubmitClicked { get; set; }
        public ICommand OnResendOTPClicked { get; set; }
        public ICommand OnValidateOTPClicked { get; set; }
        public ICommand OnLoginPageLinkClicked { get; set; }

        ForgotPasswordOTP forgotPasswordOTP { get; set; }

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
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private ForgotUserNamePassword _selectedTaxPayerType;
        public ForgotUserNamePassword SelectedTaxPayerType
        {
            get
            {
                return _selectedTaxPayerType;
            }
            set
            {
                _selectedTaxPayerType = value;
                RaisePropertyChanged("SelectedTaxPayerType");

                if (SelectedTaxPayerType != null)
                {
                    SetLayoutVisibilityForSelectedTaxpayerType();
                }
            }
        }

        private List<ForgotCredentialType> _forgotTypeList;
        public List<ForgotCredentialType> ForgotTypeList
        {
            get
            {
                return _forgotTypeList;
            }
            set
            {
                _forgotTypeList = value;
                RaisePropertyChanged("ForgotTypeList");


            }
        }

        private List<TIN> _tINs;
        public List<TIN> TINs
        {
            get
            {
                return _tINs;
            }
            set
            {
                _tINs = value;
                RaisePropertyChanged("TINs");
            }
        }


        private TIN _selectedTinId;
        public TIN SelectedTinId
        {
            get
            {
                return _selectedTinId;
            }
            set
            {
                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    App.CurrentDropdownTIN = SelectedTinId;
                    // Password = string.Empty;
                }
                RaisePropertyChanged("SelectedTinId");
            }
        }

        private bool _isVisibleTinIds = false;
        public bool IsVisibleTinIds
        {
            get
            {
                return _isVisibleTinIds;
            }
            set
            {
                _isVisibleTinIds = value;

                RaisePropertyChanged("IsVisibleTinIds");
            }
        }


        private ForgotCredentialType _selectedForgotType;
        public ForgotCredentialType SelectedForgotType
        {
            get
            {
                return _selectedForgotType;
            }
            set
            {
                _selectedForgotType = value;
                RaisePropertyChanged("_selectedForgotType");
                if (SelectedForgotType != null)
                {
                    IsTaxPayerTypeEnable = true;
                    SetLayoutVisibilityForSelectedForgotType();
                }
                else
                {
                    IsTaxPayerTypeEnable = false;
                }


            }
        }



        private ForgotUserNamePassword _forgotCredentialType;
        public ForgotUserNamePassword ForgotCredentialType
        {
            get
            {
                return _forgotCredentialType;
            }
            set
            {
                _forgotCredentialType = value;
                RaisePropertyChanged("ForgotCredentialType");
            }
        }

        private List<ForgotUserNamePassword> _taxpayerTypeList;
        public List<ForgotUserNamePassword> TaxpayerTypeList
        {
            get
            {
                return _taxpayerTypeList;
            }
            set
            {
                _taxpayerTypeList = value;
                RaisePropertyChanged("TaxpayerTypeList");
            }
        }

        private string _iDNumber;
        public string IDNumber
        {
            get
            {
                return _iDNumber;
            }
            set
            {
                _iDNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private string _enteredCaptchaValue;
        public string EnteredCaptchaValue
        {
            get
            {
                return _enteredCaptchaValue;
            }
            set
            {
                _enteredCaptchaValue = value;
                RaisePropertyChanged("EnteredCaptchaValue");
            }
        }

        private string _corporateID;
        public string CorporateID
        {
            get
            {
                return _corporateID;
            }
            set
            {
                _corporateID = value;
                RaisePropertyChanged("CorporateID");
            }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private string _oTPValidDuration;
        public string OTPValidDuration
        {
            get
            {
                return _oTPValidDuration;
            }
            set
            {
                _oTPValidDuration = value;
                if(_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor = Color.FromHex("#005e4b");
                    IsResendOTPEnabled = true;
                }
               
                RaisePropertyChanged("OTPValidDuration");
            }
        }



        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private bool _isForgotUserNameWithIndividual = true;
        public bool IsForgotUserNameWithIndividual
        {
            get
            {
                return _isForgotUserNameWithIndividual;
            }
            set
            {
                _isForgotUserNameWithIndividual = value;
                RaisePropertyChanged("IsForgotUserName");
            }
        }

        private bool _isForgotUserNameWithCorporate = false;
        public bool IsForgotUserNameWithCorporate
        {
            get
            {
                return _isForgotUserNameWithCorporate;
            }
            set
            {
                _isForgotUserNameWithCorporate = value;
                RaisePropertyChanged("IsForgotUserNameWithCorporate");
            }
        }

        private bool _isForgotPassword = false;
        public bool IsForgotPassword
        {
            get
            {
                return _isForgotPassword;
            }
            set
            {
                _isForgotPassword = value;
                RaisePropertyChanged("IsForgotPassword");
            }
        }

        private bool _isTaxPayerTypeEnable = false;
        public bool IsTaxPayerTypeEnable
        {
            get
            {
                return _isTaxPayerTypeEnable;
            }
            set
            {
                _isTaxPayerTypeEnable = value;
                RaisePropertyChanged("IsTaxPayerTypeEnable");
            }
        }

        private bool _newPasswordLayoutVisibility = false;
        public bool NewPasswordLayoutVisibility
        {
            get
            {
                return _newPasswordLayoutVisibility;
            }
            set
            {
                _newPasswordLayoutVisibility = value;
                RaisePropertyChanged("NewPasswordLayoutVisibility");
            }
        }

        private bool _oTPLayoutVisibility = false;
        public bool OTPLayoutVisibility
        {
            get
            {
                return _oTPLayoutVisibility;
            }
            set
            {
                _oTPLayoutVisibility = value;
                RaisePropertyChanged("OTPLayoutVisibility");
            }
        }

        private bool _navigateToLoginLinkVisibility = false;
        public bool NavigateToLoginLinkVisibility
        {
            get
            {
                return _navigateToLoginLinkVisibility;
            }
            set
            {
                _navigateToLoginLinkVisibility = value;
                RaisePropertyChanged("NavigateToLoginLinkVisibility");
            }
        }


        private string _newPassword = "";
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        private string _confirmPassword = "";
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged("ConfirmPassword");
            }
        }



        private string _iDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
        public string IDNumberOrCorporateIDOrUserName
        {
            get
            {
                return _iDNumberOrCorporateIDOrUserName;
            }
            set
            {
                _iDNumberOrCorporateIDOrUserName = value;
                RaisePropertyChanged("IDNumberOrCorporateIDOrUserName");
            }
        }

        private string _captcha;
        public string Captcha
        {
            get
            {
                return _captcha;
            }
            set
            {
                _captcha = value;
                RaisePropertyChanged("Captcha");
            }
        }

        private string _forgotPasswordUserNameChangedMessage;
        public string ForgotPasswordUserNameChangedMessage
        {
            get
            {
                return _forgotPasswordUserNameChangedMessage;
            }
            set
            {
                _forgotPasswordUserNameChangedMessage = value;
                RaisePropertyChanged("ForgotPasswordUserNameChangedMessage");
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
                _enteredOTP = value;
                RaisePropertyChanged("EnteredOTP");
            }
        }

        private bool _isIDTypeVisible = false;
        public bool IsIDTypeVisible
        {
            get
            {
                return _isIDTypeVisible;
            }
            set
            {
                _isIDTypeVisible = value;
                RaisePropertyChanged("IsIDTypeVisible");
            }
        }

        private bool _newPasswordVisibility = false;
        public bool NewPasswordVisibility
        {
            get
            {
                return _newPasswordVisibility;
            }
            set
            {
                _newPasswordVisibility = value;
                RaisePropertyChanged("NewPasswordVisibility");
            }
        }
        private Color _buttonDisableColor =Color.FromHex("#9EA4A9") ;
        public Color ButtonDisableColor
        {
            get
            {
                return _buttonDisableColor;
            }
            set
            {
                _buttonDisableColor = value;
                RaisePropertyChanged("ButtonDisableColor");
            }
        }

        

        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                _isResendOTPEnabled = value;
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }

        
        private bool _confirmPasswordVisibility = false;
        public bool ConfirmPasswordVisibility
        {
            get
            {
                return _confirmPasswordVisibility;
            }
            set
            {
                _confirmPasswordVisibility = value;
                RaisePropertyChanged("ConfirmPasswordVisibility");
            }
        }


        private bool _isSubmitEnabled = false;
        public bool IsSubmitEnabled
        {
            get
            {
                return _isSubmitEnabled;
            }
            set
            {
                _isSubmitEnabled = value;
                RaisePropertyChanged("IsSubmitEnabled");
            }
        }
        private Color _submitDisableButtonColor = Color.FromHex("#9EA4A9");
        public Color SubmitDisableButtonColor
        {
            get
            {
                return _submitDisableButtonColor;
            }
            set
            {
                _submitDisableButtonColor = value;
                RaisePropertyChanged("SubmitDisableButtonColor");
            }
        }

        #endregion

        #region Constructor

        public ForgotUsernamePasswordPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnSubmitClicked = new Command(async () =>
            {
              bool _isAllFormDataAvailable =  ValidateForms();
                if(_isAllFormDataAvailable)
                {
                    bool isValiedCaptcha = ValidateCaptcha();
                    if (isValiedCaptcha)
                    {
                        if (SelectedForgotType.id.Equals("1") && ((SelectedTaxPayerType.id.Equals("1")) || (SelectedTaxPayerType.id.Equals("2"))) && !(string.IsNullOrEmpty(IDNumber)))
                        {
                            await SendUserNameToRegidteredEmail();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  }
                        else
                        {
                            if (!String.IsNullOrEmpty(IDNumber))
                            {
                                await SendOTPToRegisterMobileNumber();
                            }
                            else
                            {
                                _dialogService.ShowMessageBox(AppResources.PleaseenterUsername, AppResources.Information);

                            }
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.enteredcaptchacodeisincorrect, AppResources.Information);
                    }
                }
                else
                {
                    await _dialogService.ShowMessageBox("Enter the required field", AppResources.Information);
                }

            });
            OnCaptchaRegenerateClicked = new Command(async () =>
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
            });

            OnChangePasswordSubmitClicked = new Command(async () =>
            {
                await ChangePassword();
            });

            OnResendOTPClicked = new Command(async () =>
            {
                await SendOTPToRegisterMobileNumber();
            });

            OnValidateOTPClicked = new Command(async () =>
            {
                await ValidateOTP();
            });

            OnLoginPageLinkClicked = new Command(() =>
           {
               _navigationService.GoBack();
           });


        }
        #endregion Constructor
        #region Method
        public async Task OnPageLoad()
        {
            string lang = UtilityManager.GetLanguageParameter();
            try
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                List<ForgotUserNamePassword> list = new List<ForgotUserNamePassword>
            {
                new ForgotUserNamePassword{ id = "1" , TaxPayerType = AppResources.Individual},
                new ForgotUserNamePassword{ id = "2" , TaxPayerType = AppResources.Company}

            };
                TaxpayerTypeList = list;

                List<ForgotCredentialType> forgotCredentialListlist = new List<ForgotCredentialType>
            {
                new ForgotCredentialType{ id = "1" , CredentialType = AppResources.ForgotUsername},
                new ForgotCredentialType{ id = "2" , CredentialType = AppResources.ForgotPassword}

            };
                ForgotTypeList = forgotCredentialListlist;
            }
            catch (Exception ex)
            {

            }


        }

        private void SetLayoutVisibilityForSelectedForgotType()
        {
            if (SelectedForgotType.id.Equals("2"))
            {
                IDNumberOrCorporateIDOrUserName = AppResources.UserName;
                //Device.BeginInvokeOnMainThread(() => {

                IsTaxPayerTypeEnable = false;
                SelectedTaxPayerType = null;
                IsIDTypeVisible = false;
                //IsForgotPassword = true;
                //IsForgotUserNameWithIndividual = false;
                //IsForgotUserNameWithCorporate = false;
                //});

            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                IsIDTypeVisible = true;
                //IsTaxPayerTypeEnable = true;
                //IsForgotPassword = false;
                //IsForgotUserNameWithIndividual = true;
                //IsForgotUserNameWithCorporate = false;
            }

        }

        private void SetLayoutVisibilityForSelectedTaxpayerType()
        {
            if (SelectedTaxPayerType.id.Equals("1"))
            {
                IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                //IsForgotUserNameWithIndividual = true;
                //IsForgotUserNameWithCorporate = false;
            }
            else
            {
                //IsForgotUserNameWithIndividual = false;
                //IsForgotUserNameWithCorporate = true;
                IDNumberOrCorporateIDOrUserName = AppResources.CorportaeID;
            }
        }

        private bool  ValidateForms()
        {
            bool IsAllDataAvailable = false;
            if(SelectedForgotType != null && SelectedForgotType.id.Equals("2"))
            {
                if(!string.IsNullOrEmpty(IDNumber))
                {
                    bool IsEmailUserName = false;
                    IsEmailUserName = UtilityManager.IsValidEmailAddress(IDNumber);

                    if (IsEmailUserName)
                    {
                        if (SelectedTinId != null && !string.IsNullOrEmpty(EnteredCaptchaValue))
                        {
                            IsAllDataAvailable = true;
                           
                        }
                        else
                        {
                            IsAllDataAvailable = false; 
                           
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(EnteredCaptchaValue))
                        {
                            IsAllDataAvailable = true;
                        }
                        else
                        {
                            IsAllDataAvailable = false; 
                        }
                    }
                    

                }
                else
                {
                    IsAllDataAvailable = false; 
                }

            }
            else if (SelectedTaxPayerType != null && (SelectedTaxPayerType.id.Equals("1") || SelectedTaxPayerType.id.Equals("2")))
            {
                if (SelectedTaxPayerType != null && !string.IsNullOrEmpty(IDNumber) && !string.IsNullOrEmpty(EnteredCaptchaValue))
                {
                    IsAllDataAvailable = true;
                }
                else
                {
                    IsAllDataAvailable = false;
                }
            }
            else
            {
                IsAllDataAvailable = false;
            }

            return IsAllDataAvailable;
        }

        public StringBuilder GetCaptcha()
        {
            StringBuilder Captcha;

            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                //Session["captcha"] = captcha.ToString();
                //imgCaptcha.ImageUrl = "~/Captcha/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                Captcha = captcha;
            }
            catch
            {
                throw;
            }
            return Captcha;

        }

        public bool ValidateCaptcha()
        {
            bool isValidCaptcha = false;
            isValidCaptcha = EnteredCaptchaValue.Equals(Captcha);
            if (EnteredCaptchaValue.Equals(Captcha))
            {
                isValidCaptcha = true;
                EnteredCaptchaValue = string.Empty;
            }
            else
            {
                // _dialogService.ShowMessageBox(AppResources.InvaliedCaptcha, AppResources.Information);

                isValidCaptcha = false;
            }
            return isValidCaptcha;
        }

        private async Task SendOTPToRegisterMobileNumber()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                try
                {
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(lang, idNumber);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPasswordOTP.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId))
                    {
                        Device.BeginInvokeOnMainThread(() => {

                            // await _dialogService.ShowMessageBox("OTP sent to registered mobile", AppResources.Information);
                            OTPLayoutVisibility = true;
                            ButtonDisableColor = Color.FromHex("#9EA4A9");
                            IsResendOTPEnabled = false;

                            string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                            MobileNumber = "XXXXXXXXXX" + _mobileNumber;

                            TimerStart();
                        });

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.Information);
                        });

                    }
                }
                catch(Exception ex)
                {

                }

                

            });



            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }

        private async Task ValidateOTP()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(EnteredOTP))
                {
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
                    //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";

                    string st1 = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";

                    string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                    string type = "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;

                    D d = new D();
                    d.__metadata = metadata;
                    d.Action = "01";
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = 0;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "ZS001";
                    d.Idnumber = "";
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    forgotPassword = await WebServiceManager.GAZTForgotPasswordValidateOTP(forgotPassword);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPassword.d != null && !(string.IsNullOrEmpty(forgotPassword.d.Tin)))
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(AppResources.Pleasechangepassword, AppResources.Information);
                        });

                        OTPLayoutVisibility = false;
                        NewPasswordLayoutVisibility = true;
                        MobileNumber = forgotPassword.d.MobileNo;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(AppResources.Invalidverificationcodeentered, AppResources.Information);
                        });
                        EnteredOTP = null;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox(AppResources.PleaseenterOTP, AppResources.Information);
                    });


                }
            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });




        }


        private async Task SendUserNameToRegidteredEmail()
        {
            string idNumber = GetTinId();
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {

                string lang = UtilityManager.GetLanguageParameter();
                string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
                //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
                string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + idNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";

                string st1 = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";

                string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "1" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + IDNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                string type = "ZDP_FRGT_USRNM_PWD_SRV.Header";
                ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                Metadata metadata = new Metadata();
                metadata.id = id;
                metadata.uri = uri;
                metadata.type = type;

                D d = new D();
                d.__metadata = metadata;
                d.Action = "40";
                d.Tin = "";
                d.Langu = UtilityManager.GetLanguageParameter();
                d.CurrAttmps = 0;
                d.EmailId = "";
                d.TpType = "1";
                d.MobileNo = "";
                d.SubType = "ZS001";
                d.Idnumber = idNumber;
                d.Otp = EnteredOTP;
                d.Minutes = 0;
                d.Name = "";
                d.Attempts = 0;
                // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                d.NewPwd = NewPassword;
                d.CnfPwd = ConfirmPassword;
                d.RdBt = "U";
                d.Hyperlink = "";
                forgotPassword.d = d;
                forgotPassword = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (forgotPassword.d != null && !string.IsNullOrEmpty(forgotPassword.d.EmailId))
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        //await _dialogService.ShowMessageBox(AppResources.Usernamehasbeensenttoregisteredmobilenumber, AppResources.Information);
                        // _navigationService.GoBack();
                        NewPasswordLayoutVisibility = false;
                        OTPLayoutVisibility = false;
                        NavigateToLoginLinkVisibility = true;
                        ForgotPasswordUserNameChangedMessage = AppResources.Usernamehasbeensenttoregisteredmobilenumber;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.Information);

                    });
                }
            });


            await Task.Run(async () =>
            {
                IsLoading = false;
            });

        }

        private async Task ChangePassword()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                bool isNewPasswordValid = UtilityManager.IsPasswordValid(NewPassword);
                bool isConfirmPasswordValid = UtilityManager.IsPasswordValid(ConfirmPassword);
                if (isNewPasswordValid && isConfirmPasswordValid)
                {
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
                    //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";

                    string st1 = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";

                    string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
                    string type = "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;

                    D d = new D();
                    d.__metadata = metadata;
                    d.Action = "40";
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = 0;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "";
                    d.Idnumber = "";
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                    d.NewPwd = NewPassword;
                    d.CnfPwd = ConfirmPassword;
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    if (NewPassword.Equals(ConfirmPassword))
                    {
                        forgotPassword = await WebServiceManager.GAZTChangePassword(forgotPassword);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (forgotPassword!=null && !string.IsNullOrEmpty(forgotPassword.d.EmailId))
                        {
                            // await _dialogService.ShowMessageBox(AppResources.YourPasswordhasbeenChangedsuccessfully, AppResources.Information);

                            NewPasswordLayoutVisibility = false;
                            OTPLayoutVisibility = false;
                            NavigateToLoginLinkVisibility = true;
                            ForgotPasswordUserNameChangedMessage = AppResources.YourPasswordhasbeenChangedsuccessfully;

                            //  _navigationService.GoBack();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.Boththepasswordfieldsshouldmatch, AppResources.Information);
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PasswordGuidelineText, AppResources.Alerts);
                    });
                }
            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }













        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        public async Task SetTinsListLayoutVisibility(bool IsEmailUserName)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
            if (IsEmailUserName)
            {
                TINs = new List<TIN>();

                
                    try
                    {
                       

                        TINs = await WebServiceManager.GAZTGetAllTins(IDNumber);
                        if (TINs.Count != 0 && SelectedTinId == null)
                        {
                            IsVisibleTinIds = true;
                            SelectedTinId = TINs[0];
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsVisibleTinIds = false;
                                await _dialogService.ShowMessageBox(AppResources.NoTINsAvailable, AppResources.Information);
                            });
                            IsVisibleTinIds = false;
                        }
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    }
                    catch (Exception e)
                    {
                        IsVisibleTinIds = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsVisibleTinIds = false;
                            await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                        });

                        
                    }

                   
               
            }
            else
            {
                IsVisibleTinIds = false;
            }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });


        }

        private string GetTinId()
        {
            string tinId = "";
            bool isEmailUser = UtilityManager.IsValidEmailAddress(IDNumber);
            if (isEmailUser)
            {
                tinId = SelectedTinId.Tin;
            }
            else
            {
                tinId = IDNumber;
            }
            return tinId;
        }





        private void TimerStart()
        {
            CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();
           
            int TotalSec = 120;

            CancellationTokenSource CTS = _CancellationTokenSource;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (CTS.IsCancellationRequested)
                {
                    return false;
                }
                else
                {
                    if (TotalSec == 0)
                    {
                        return false;
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TotalSec = TotalSec - 1;
                        TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                        OTPValidDuration =" " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
                    });
                    return true;
                }
            });
        }

    
        #endregion
    }
}
