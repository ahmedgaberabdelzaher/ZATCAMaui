using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
            });
            OnCaptchaRegenerateClicked = new Command(async () =>
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
            });

            OnChangePasswordSubmitClicked = new Command(async () =>
            {
                ChangePassword();
            });

            OnResendOTPClicked = new Command(async () =>
            {
                await SendOTPToRegisterMobileNumber();
            });

            OnValidateOTPClicked = new Command(async () =>
            {
                await ValidateOTP();
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
                new ForgotCredentialType{ id = "2" , CredentialType = AppResources.Password}

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
                //IsForgotPassword = true;
                //IsForgotUserNameWithIndividual = false;
                //IsForgotUserNameWithCorporate = false;
                //});

            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
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
            }
            else
            {
                // _dialogService.ShowMessageBox(AppResources.InvaliedCaptcha, AppResources.Information);

                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
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
                string lang = UtilityManager.GetLanguageParameter();
                forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(lang, IDNumber);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (!string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId))
                {
                    Device.BeginInvokeOnMainThread(() => {

                        // await _dialogService.ShowMessageBox("OTP sent to registered mobile", AppResources.Information);
                        OTPLayoutVisibility = true;
                    });

                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                    });

                }

            });



            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }

        private async Task ValidateOTP()
        {
            if (!string.IsNullOrEmpty(EnteredOTP))
            {
                string lang = UtilityManager.GetLanguageParameter();
                string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
                //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
                string id = st + "'" + IDNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";

                string st1 = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";

                string uri = st1 + "'" + IDNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                string type = "ZDP_FRGT_USRNM_PWD_SRV.Header";
                ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                Metadata metadata = new Metadata();
                metadata.id = id;
                metadata.uri = uri;
                metadata.type = type;

                D d = new D();
                d.__metadata = metadata;
                d.Action = "01";
                d.Tin = IDNumber;
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
                    await _dialogService.ShowMessageBox(AppResources.Pleasechangepassword, AppResources.Information);
                    OTPLayoutVisibility = false;
                    NewPasswordLayoutVisibility = true;
                    MobileNumber = forgotPassword.d.MobileNo;
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.Invalidverificationcodeentered, AppResources.Information);
                    EnteredOTP = null;
                }


            }
            else
            {
                await _dialogService.ShowMessageBox(AppResources.PleaseenterOTP, AppResources.Information);

            }

            // await WebServiceManager.GAZTForgotPasswordValidateOTP(forgotPasswordOTP);

        }


        private async Task SendUserNameToRegidteredEmail()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {

                string lang = UtilityManager.GetLanguageParameter();
                string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
                //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
                string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + IDNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";

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
                d.Idnumber = IDNumber;
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

                if (!string.IsNullOrEmpty(forgotPassword.d.EmailId))
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox(AppResources.Usernamehasbeensenttoregisteredmobilenumber, AppResources.Information);
                        _navigationService.GoBack();
                    });


                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);

                    });
                }
            });


            await Task.Run(async () =>
            {
                IsLoading = false;
            });

        }

        private async void ChangePassword()
        {
            string lang = UtilityManager.GetLanguageParameter();
            string st = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";
            //string str = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3050000029',Langu='EN',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='5866',Dob=datetime'2019-12-21T00%3A00%3A00',NewPwd='',RdBt='P')";
            string id = st + "'" + IDNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";

            string st1 = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin=";

            string uri = st1 + "'" + IDNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
            string type = "ZDP_FRGT_USRNM_PWD_SRV.Header";
            ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
            Metadata metadata = new Metadata();
            metadata.id = id;
            metadata.uri = uri;
            metadata.type = type;

            D d = new D();
            d.__metadata = metadata;
            d.Action = "40";
            d.Tin = IDNumber;
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
                if (!string.IsNullOrEmpty(forgotPassword.d.EmailId))
                {
                    await _dialogService.ShowMessageBox(AppResources.YourPasswordhasbeenChangedsuccessfully, AppResources.Information);
                    NewPasswordLayoutVisibility = false;
                    _navigationService.GoBack();
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                }
            }
            else
            {
                await _dialogService.ShowMessageBox(AppResources.Boththepasswordfieldsshouldmatch, AppResources.Information);
            }

        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        #endregion
    }
}
