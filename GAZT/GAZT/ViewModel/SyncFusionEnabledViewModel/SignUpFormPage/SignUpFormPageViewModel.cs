using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SignUpFormPage_ViewModel
{
    public class SignUpFormPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        public ICommand OnNextClicked { get; set; }
        public int DefaultMonth;
        public ICommand GoBackClick { get; set; }
        #endregion
        #region Properties 
        private bool _isCRChecked = false;
        public bool IsCRChecked
        {
            get
            {
                return _isCRChecked;
            }
            set
            {
                _isCRChecked = value;
                if (_isCRChecked == true)
                {
                    IsCRVisible = true;
                }
                else
                {
                    IsCRVisible = false;
                }
                RaisePropertyChanged("IsCRChecked");
            }
        }
        private bool _isLNChecked = false;
        public bool IsLNChecked
        {
            get
            {
                return _isLNChecked;
            }
            set
            {
                _isLNChecked = value;
                if(_isLNChecked==true)
                {
                    IsLicenseVisible = true;
                }
                else
                {
                    IsLicenseVisible = false;
                }
                RaisePropertyChanged("IsLNChecked");
            }
        }
        private int _iDTypeIndex = 0;
        public int IDTypeIndex
        {
            get
            {
                return _iDTypeIndex;
            }
            set
            {
                _iDTypeIndex = value;
                RaisePropertyChanged("IDTypeIndex");
            }
        }
        private ObservableCollection<object> _todayDate;
        public ObservableCollection<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                _todayDate = value;
                RaisePropertyChanged("TodayDate");
            }
        }
        private int _selectedLOrC = 1;
        public int SelectedLOrC
        {
            get
            {
                return _selectedLOrC;
            }
            set
            {
                _selectedLOrC = value;
                RaisePropertyChanged("SelectedLOrC");
            }
        }
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
        private SignUpUsing _selectedSignUpUsingSetForCancle = null;
        public SignUpUsing SelectedSignUpUsingSetForCancle
        {
            get
            {
                return _selectedSignUpUsingSetForCancle;
            }
            set
            {
                _selectedSignUpUsingSetForCancle = value;
                RaisePropertyChanged("SelectedSignUpUsingSetForCancle");
            }
        }
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
                    try
                    {
                        if (_selectedSignUpUsing.ID == 1)
                        {
                            MaxLengthID = 10;
                        }
                        else if (_selectedSignUpUsing.ID == 2)
                        {
                            MaxLengthID = 10;
                        }
                        else if (_selectedSignUpUsing.ID == 3)
                        {
                            MaxLengthID = 15;
                        }
                        TxtIDType = _selectedSignUpUsing.SUType;
                    }
                    catch(Exception Ex)
                    {
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
                if (_SelectedLCType != null)
                {
                    if (_SelectedLCType.ID == 2)
                    {
                        IsLicenseVisible = false;
                        IsCRVisible = true;
                    }
                    else if (_SelectedLCType.ID == 1)
                    {
                        IsLicenseVisible = true;
                        IsCRVisible = false;
                    }
                    TxtLOrC = _SelectedLCType.LCType;
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
                if(_selectCityList!=null)
                {
                    TxtLOrCIssuedByCity = _selectCityList.CityName;
                }
                RaisePropertyChanged("SelectCityList");
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
                _maxDigids = value;
                RaisePropertyChanged("MaxDigids");
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
                _mobileCountryCode = value;
         
                RaisePropertyChanged("MobileCountryCode");
            }
        }

        private SignupCityResult _selectCityListPrev = null;
        public SignupCityResult SelectCityListPrev
        {
            get
            {
                return _selectCityListPrev;
            }
            set
            {
                _selectCityListPrev = value;
                RaisePropertyChanged("SelectCityListPrev");
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
                    TxtTIN = string.Empty;
                }
                else
                {
                    IsTINVisible = false;
                    TxtTIN = string.Empty;
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
                if (_selectedIssuedBy != null)
                {
                    TxtLOrCIssuedBy = _selectedIssuedBy.txt50;
                }
                RaisePropertyChanged("SelectedIssuedBy");
            }
        }
        private IssuedByResponse _selectedIssuedByPrev = null;
        public IssuedByResponse SelectedIssuedByPrev
        {
            get
            {
                return _selectedIssuedByPrev;
            }
            set
            {
                _selectedIssuedByPrev = value;
                RaisePropertyChanged("SelectedIssuedByPrev");
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
        private string _txtCountryCode = string.Empty;
        public string TxtCountryCode
        {
            get
            {
                return _txtCountryCode;
            }
            set
            {

                _txtCountryCode = value;
                if (_txtCountryCode != null)
                {
                    MaxDigids = (14 - _txtCountryCode.Length).ToString();
                    TxtMobileNumber = string.Empty;
                }
                else
                {
                    MaxDigids = "15";
                }

                RaisePropertyChanged("TxtCountryCode");
            }
        }
        private bool _BtnEnableFlag;
        public bool BtnEnableFlag
        {
            get { return _BtnEnableFlag; }
            set
            {
                _BtnEnableFlag = value;
                RaisePropertyChanged("BtnEnableFlag");
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
                BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;
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
        //private string _enteredCaptchaValue = string.Empty;
        //public string EnteredCaptchaValue
        //{
        //    get
        //    {
        //        return _enteredCaptchaValue;
        //    }
        //    set
        //    {
        //        _enteredCaptchaValue = value;
        //        RaisePropertyChanged("EnteredCaptchaValue");
        //    }
        //}
        //private string _captcha = string.Empty;
        //public string Captcha
        //{
        //    get
        //    {
        //        return _captcha;
        //    }
        //    set
        //    {
        //        _captcha = value;
        //        RaisePropertyChanged("Captcha");
        //    }
        //}
        private string _pkrDBO = string.Empty;
        public string PkrDBO {
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
        private string _pkrDBOPrev = string.Empty;
        public string PkrDBOPrev
        {
            get
            {
                return _pkrDBOPrev;
            }
            set
            {
                _pkrDBOPrev = value;
                RaisePropertyChanged("PkrDBOPrev");
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
        private SignUpModelRootObject _signUpFirstSubmitModel = null;
        public SignUpModelRootObject SignUpFirstSubmitModel
        {
            get
            {
                return _signUpFirstSubmitModel;
            }
            set
            {
                _signUpFirstSubmitModel = value;
                RaisePropertyChanged("SignUpFirstSubmitModel");
            }
        }
        private DateTime _maximumxD = DateTime.Now;
        public DateTime MaximumxD
        {
            get
            {
                return _maximumxD;
            }
            set
            {
                _maximumxD = value;
                RaisePropertyChanged("MaximumxD");
            }
        }
        private string _txtIDType = string.Empty;
        public string TxtIDType
        {
            get
            {
                return _txtIDType;
            }
            set
            {
                _txtIDType = value;
                RaisePropertyChanged("TxtIDType");
            }
        }
        private string _txtLOrC = string.Empty;
        public string TxtLOrC
        {
            get
            {
                return _txtLOrC;
            }
            set
            {
                _txtLOrC = value;
                RaisePropertyChanged("TxtLOrC");
            }
        }
        private string _txtLOrCIssuedBy = string.Empty;
        public string TxtLOrCIssuedBy
        {
            get
            {
                return _txtLOrCIssuedBy;
            }
            set
            {
                _txtLOrCIssuedBy = value;
                RaisePropertyChanged("TxtLOrCIssuedBy");
            }
        }
        private string _txtLOrCIssuedByCity = string.Empty;
        public string TxtLOrCIssuedByCity
        {
            get
            {
                return _txtLOrCIssuedByCity;
            }
            set
            {
                _txtLOrCIssuedByCity = value;
                RaisePropertyChanged("TxtLOrCIssuedByCity");
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
                GoBackClick = new Command(async () =>
                {
                    _navigationService.GoBack();
                });
                //OnCaptchaRegenerateClicked = new Xamarin.Forms.Command(() =>
                //{
                //        StringBuilder captcha = GetCaptcha();
                //        Captcha = captcha.ToString();
                //        EnteredCaptchaValue = string.Empty;
                //});
                OnNextClicked = new Xamarin.Forms.Command(() =>
                {
                    _navigationService.NavigateTo(App.CreateGaztAccountPageView);
                });
            }
            catch (Exception ex)
            {
            }
        }
        //private string NewMobileNumberFormate(string mobileNumber)
        //{
        //    string formatedCountryCode = MobileCountryCode.Replace("+", "");
        //    return formatedCountryCode + TxtMobileNumber;
        //}
        #endregion
        #region Methods 
        //public bool ValidateCaptcha()
        //{
        //    bool isValidCaptcha = false;
        //    isValidCaptcha = EnteredCaptchaValue.Equals(Captcha);
        //    if (EnteredCaptchaValue.Equals(Captcha))
        //    {
        //        isValidCaptcha = true;
        //        EnteredCaptchaValue = string.Empty;
        //    }
        //    else
        //    {
        //        // _dialogService.ShowMessageBox(AppResources.InvaliedCaptcha, AppResources.Information);
        //        isValidCaptcha = false;
        //    }
        //    return isValidCaptcha;
        //}
        //public StringBuilder GetCaptcha()
        //{
        //    StringBuilder Captcha;
        //    try
        //    {
        //        Random random = new Random();
        //        string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //        StringBuilder captcha = new StringBuilder();
        //        for (int i = 0; i < 6; i++)
        //            captcha.Append(combination[random.Next(combination.Length)]);
        //        //Session["captcha"] = captcha.ToString();
        //        //imgCaptcha.ImageUrl = "~/Captcha/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
        //        Captcha = captcha;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return Captcha;
        //}
        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
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
                    TxtIDType = AppResources.ZZNationalID;
                    SignUpUsing SignUpUsingM = new SignUpUsing();
                    SignUpUsingM.ID = 1;
                    SignUpUsingM.SUType = AppResources.ZZNationalID;
                    SelectedSignUpUsing = SignUpUsingM;
                    //LcTypeList = null;
                    //List<LicenseOrCRModel> LIstLcType = new List<LicenseOrCRModel>();
                    //LIstLcType.Add(new LicenseOrCRModel { ID = 1, LCType = AppResources.ZZLicenseNumber });
                    //LIstLcType.Add(new LicenseOrCRModel { ID = 2, LCType = AppResources.ZZCRNumber });
                    //LcTypeList = LIstLcType;
                    LicenseOrCRModel LicenseOrCRModelM = new LicenseOrCRModel();
                    LicenseOrCRModelM.ID = 2;
                    LicenseOrCRModelM.LCType = AppResources.ZZCRNumber;
                    SelectLCType = LicenseOrCRModelM;
                                       //StringBuilder captcha = GetCaptcha();
                    //Captcha = captcha.ToString();
                    IDTypeModelRootObject = null;
                    IDTypeIndex = 0;
                    SelectedLOrC = 1;
                }
                catch(Exception ex)
                {
                }
               // PkrDBO = null;
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public async Task SetIssueIdList()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            try
            {
                IssuedByList = null;
                List<IssuedByResponse> IssuedByResponseList = new List<IssuedByResponse>();
                var IssuedBy = await WebServiceManager.GAZTGetIssuedByList();
                IssuedByList = new List<IssuedByResponse>(IssuedBy);
               
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                     await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }

            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                     IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }
        public async Task SetCityList()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsLoading = true;

            });

            try
            {
                CityList = null;
                SignupCityRootObject CityListSignup = await WebServiceManager.GAZTGetCityListForSignup();
                List<SignupCityResult> CityR = new List<SignupCityResult>();
                IsCRChecked = true;
                IsLNChecked = false;
                CityR = CityListSignup.d.city_dropdownSet.results;
                CityList = CityR;
            }
            catch (GAZTException gex)
            {
                
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsLoading = false;

            });
        }
        public async Task  SetDefaultDate()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            //Select today dates

            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            if (DateTime.Now.Date.Month < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Month);
            else
                todaycollection.Add(DateTime.Now.Date.Month.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());
            TodayDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;
        }
        #endregion
    }
}
