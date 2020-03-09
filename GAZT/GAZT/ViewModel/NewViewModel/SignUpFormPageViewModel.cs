using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class SignUpFormPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        public ICommand OnNextClicked { get; set; }
        #endregion


        #region Properties 
        private SignUpUsing _selectedSignUpUsing = null;
        public SignUpUsing SelectedSignUpUsing
        {
            get
            {
                return _selectedSignUpUsing;
            }
            set
            {
                _selectedSignUpUsing = value;
                if(_selectedSignUpUsing != null)
                {
                    if(_selectedSignUpUsing.ID==1)
                    {
                        MaxLengthID = 10;
                    }
                    else if(_selectedSignUpUsing.ID==2)
                    {
                        MaxLengthID = 10;
                    }
                    else if (_selectedSignUpUsing.ID == 3)
                    {
                        MaxLengthID = 15;
                    }
                }
                RaisePropertyChanged("SelectedSignUpUsing");
            }
        }
        private List<SignUpUsing> _signUpUsingList = null;
        public List<SignUpUsing> SignUpUsingList
        {
            get
            {
                return _signUpUsingList;
            }
            set
            {
                _signUpUsingList = value;
                RaisePropertyChanged("SignUpUsingList");
            }
        }

        private LicenseOrCRModel _SelectedLCType = null;
        public LicenseOrCRModel SelectLCType
        {
            get
            {
                return _SelectedLCType;
            }
            set
            {
                _SelectedLCType = value;
                if(_SelectedLCType.ID==2)
                {
                    IsLicenseVisible = false;
                    IsCRVisible = true;
                }
                else if(_SelectedLCType.ID == 1)
                {
                    IsLicenseVisible = true;
                    IsCRVisible = false;
                }
                RaisePropertyChanged("SelectLCType");
            }
        }

        private List<LicenseOrCRModel> _lcTypeList = null;
        public List<LicenseOrCRModel> LcTypeList
        {
            get
            {
                return _lcTypeList;
            }
            set
            {
                _lcTypeList = value;
                RaisePropertyChanged("LcTypeList");
            }
        }


        private SignupCityResult _selectCityList = null;
        public SignupCityResult SelectCityList
        {
            get
            {
                return _selectCityList;
            }
            set
            {
                _selectCityList = value;
                RaisePropertyChanged("SelectCityList");
            }
        }

        private List<SignupCityResult> _cityList = null;
        public List<SignupCityResult> CityList
        {
            get
            {
                return _cityList;
            }
            set
            {
                _cityList = value;
                RaisePropertyChanged("CityList");
            }
        }

        private bool _isCRVisible = false;
        public bool IsCRVisible
        {
            get
            {
                return _isCRVisible;
            }
            set
            {
                _isCRVisible = value;
                RaisePropertyChanged("IsCRVisible");
            }
        }

        private bool _isLicenseVisible = false;
        public bool IsLicenseVisible
        {
            get
            {
                return _isLicenseVisible;
            }
            set
            {
                _isLicenseVisible = value;
                RaisePropertyChanged("IsLicenseVisible");
            }
        }
        private bool _isTIN = false;
        public bool IsTIN
        {
            get
            {
                return _isTIN;
            }
            set
            {
                _isTIN = value;
                if(_isTIN==true)
                {
                    IsTINVisible = true;
                }
                else
                {
                    IsTINVisible = false;
                }
                RaisePropertyChanged("IsTIN");
            }
        }
        private bool _isTINVisible = false;
        public bool IsTINVisible
        {
            get
            {
                return _isTINVisible;
            }
            set
            {
                _isTINVisible = value;
                RaisePropertyChanged("IsTINVisible");
            }
        }
        private IssuedByResponse _selectedIssuedBy = null;
        public IssuedByResponse SelectedIssuedBy
        {
            get
            {
                return _selectedIssuedBy;
            }
            set
            {
                _selectedIssuedBy = value;
                RaisePropertyChanged("SelectedIssuedBy");
            }
        }
        private List<IssuedByResponse> _issuedByList = null;
        public List<IssuedByResponse> IssuedByList
        {
            get
            {
                return _issuedByList;
            }
            set
            {
                _issuedByList = value;
                RaisePropertyChanged("IssuedByList");
            }
        }

        private string _txtTIN = string.Empty;
        public string TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                RaisePropertyChanged("TxtTIN");
            }
        }

        private string _txtIDNumber = string.Empty;
        public string TxtIDNumber
        {
            get
            {
                return _txtIDNumber;
            }
            set
            {
                _txtIDNumber = value;
                RaisePropertyChanged("TxtIDNumber");
            }
        }

        private string _txtName = string.Empty;
        public string TxtName
        {
            get
            {
                return _txtName;
            }
            set
            {
                _txtName = value;
                RaisePropertyChanged("TxtName");
            }
        }

        private string _txtCRNumber = string.Empty;
        public string TxtCRNumber
        {
            get
            {
                return _txtCRNumber;
            }
            set
            {
                _txtCRNumber = value;
                RaisePropertyChanged("TxtCRNumber");
            }
        }

        private string _txtLicenseNumber = string.Empty;
        public string TxtLicenseNumber
        {
            get
            {
                return _txtLicenseNumber;
            }
            set
            {
                _txtLicenseNumber = value;
                RaisePropertyChanged("TxtLicenseNumber");
            }
        }

        private string _txtEmailAddress = string.Empty;
        public string TxtEmailAddress
        {
            get
            {
                return _txtEmailAddress;
            }
            set
            {
                _txtEmailAddress = value;
                RaisePropertyChanged("TxtEmailAddress");
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
                _txtMobileNumber = value;
                RaisePropertyChanged("TxtMobileNumber");
            }
        }

        private string _txtPhoneNumber = string.Empty;
        public string TxtPhoneNumber
        {
            get
            {
                return _txtPhoneNumber;
            }
            set
            {
                _txtPhoneNumber = value;
                RaisePropertyChanged("TxtPhoneNumber");
            }
        }

        private string _enteredCaptchaValue = string.Empty;
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

        private string _captcha = string.Empty;
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

        private DateTime? _pkrDBO = new DateTime(1953, 11, 24);

        public DateTime? PkrDBO {
            get
            {
                return _pkrDBO;
            }
            set
            {
                _pkrDBO = value;
                RaisePropertyChanged("PkrDBO");
            }
        }

        private int _maxLengthID = 10;
        public int MaxLengthID
        {
            get
            {
                return _maxLengthID;
            }
            set
            {
                _maxLengthID = value;
                RaisePropertyChanged("MaxLengthID");
            }
        }

        private IDTypeModelRootObject _iDTypeModelRootObject = null;
        public IDTypeModelRootObject IDTypeModelRootObject
        {
            get
            {
                return _iDTypeModelRootObject;
            }
            set
            {
                _iDTypeModelRootObject = value;
                RaisePropertyChanged("IDTypeModelRootObject");
            }
        }
        #endregion

        #region Constructor
        public SignUpFormPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            try
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

                OnCaptchaRegenerateClicked = new Xamarin.Forms.Command(() =>
                {
                   
                        StringBuilder captcha = GetCaptcha();
                        Captcha = captcha.ToString();
                        EnteredCaptchaValue = string.Empty;
                    

                });

                OnNextClicked = new Xamarin.Forms.Command(() =>
                {
                    _navigationService.NavigateTo(App.CreateGaztAccountPageView);

                });
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Methods 

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
        public void OnPageLoad()
        {
            try
            {
                SignUpUsingList = null;
                IsCRVisible = true;
                IsLicenseVisible = false;
                List<SignUpUsing> ListSignUpUsing = new List<SignUpUsing>();
                ListSignUpUsing.Add(new SignUpUsing { ID = 1, SUType = AppResources.ZZNationalID });
                ListSignUpUsing.Add(new SignUpUsing { ID = 2, SUType = AppResources.ZZIqamaID });
                ListSignUpUsing.Add(new SignUpUsing { ID = 3, SUType = AppResources.ZZGCCID });
                SignUpUsingList = ListSignUpUsing;
                SelectedSignUpUsing.ID = 1;
                LcTypeList = null;
                List<LicenseOrCRModel> LIstLcType = new List<LicenseOrCRModel>();
                LIstLcType.Add(new LicenseOrCRModel { ID = 1, LCType = AppResources.ZZLicenseNumber });
                LIstLcType.Add(new LicenseOrCRModel { ID = 2, LCType = AppResources.ZZCRNumber });
                LcTypeList = LIstLcType;
                SelectLCType.ID = 2;
                List<IssuedByResponse> IssuedByResponseList = new List<IssuedByResponse>();
                IssuedByResponseList = WebServiceManager.GAZTGetIssuedByList();
                IssuedByList = IssuedByResponseList;
                SignupCityRootObject CityListSignup =  WebServiceManager.GAZTGetCityListForSignup();
                CityList = CityListSignup.d.city_dropdownSet.results;
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                IDTypeModelRootObject = null;
               

            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}
