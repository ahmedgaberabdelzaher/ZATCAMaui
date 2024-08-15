using AppDynamics.Agent;
using Mopups.Services;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    public class SFLoginPageViewModel : SFLoginViewModel
    {

        #region Fields
        //public string password = "Test@123";
        //private string email = "3102285896";

        //private string email = "3101593128";
        //private string password = "TeUserAccountLockedst@123";
        //private string email = "3102290567";
        //private string email = "3102289204";
        // public string email = "3102292043";
        public string password;
        public string email;
        public int CurrentAttempt = 0;
        private bool loginError = false;
        private string tin;

        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public SFLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        if (NetworkCheck.IsInternet())
                        {
                            VersionTracking.Track();
                            await DependencyService.Get<IForceUpdate>().FetchAndActivateAsync();
                            var hasForceUpdateResult = bool.Parse(DependencyService.Get<IForceUpdate>().GetValue("IsForceUpdate"));

                            switch (Device.RuntimePlatform)
                            {
                                case Device.Android:

                                    var currentAndroidBuild = int.Parse(VersionTracking.CurrentBuild);
                                    var firebaseAndroidBuild = int.Parse(DependencyService.Get<IForceUpdate>().GetValue("BuildNumber_Android"));

                                    if (hasForceUpdateResult && currentAndroidBuild < firebaseAndroidBuild)
                                    {
                                        await Application.Current.MainPage.DisplayAlert(AppResources.TPUpdate, AppResources.ForceUpdateMsg, AppResources.OKText);
                                        await Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.gazt.egazt"));
                                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                                    }
                                    break;


                                case Device.iOS:
                                    var currentiOSBuild = VersionTracking.CurrentBuild;
                                    var currentiOSBuildInt = Array.ConvertAll(currentiOSBuild.Split('.'), int.Parse);

                                    var firebaseiOSBuild = DependencyService.Get<IForceUpdate>().GetValue("BuildNumber_iOS");
                                    var firebaseiOSBuildInt = Array.ConvertAll(firebaseiOSBuild.Split('.'), int.Parse);

                                    if (currentiOSBuild[0] > firebaseiOSBuild[0])
                                        return;

                                    else if (hasForceUpdateResult && (currentiOSBuildInt[0] < firebaseiOSBuildInt[0] ||
                                                 currentiOSBuildInt[1] < firebaseiOSBuildInt[1] ||
                                                 currentiOSBuildInt[2] < firebaseiOSBuildInt[2]))
                                    {
                                        await Application.Current.MainPage.DisplayAlert(AppResources.TPUpdate, AppResources.ForceUpdateMsg, AppResources.OKText);
                                        await Launcher.OpenAsync(new Uri("https://apps.apple.com/sa/app/zatca/id1517289036"));
                                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                                    }


                                    break;

                                default:
                                    break;
                            }
                        }


                    }
                    catch (Exception)
                    {

                    }

                });

            }
            catch (Exception)
            {

            }


            if (App.IsSAMLLoginEnabled == true)
            {
                IsSAMLLoginEnabled = true;
                IsOldLoginHidden = false;
            }
            else
            {
                IsSAMLLoginEnabled = false;
                IsOldLoginHidden = true;
            }

            SignUpCommand = new Command(SignUpClicked);
            ForgotPasswordCommand = new Command(ForgotPasswordClicked);
            SocialMediaLoginCommand = new Command(SocialLoggedIn);
            HamburgerMenuClickedCommand = new Command(HamburgerMenuClicked);
            this.LoginClickedCommand = new Command(async () => await LoginButtonClicked());
            this.ChangeMCommand = new Command(this.ChangeMobileClicked);

            this.WebLoginCommand = new Command(this.WebLoginClicked);
            this.ShowTinsPickerCommand = new Command(async () => await OpenTinsDropdown());


        }
        private void WebLoginClicked(object obj)
        {
            //_navigationService.NavigateTo(App.NFLoginView);

            _navigationService.NavigateTo(App.NafathLoginView, ZATCAConstants.NAFATH_LOGIN);
        }

        private async void ChangeMobileClicked(object obj)
        {
            //  _navigationService.NavigateTo(App.N);
            //_navigationService.NavigateTo(App.NewChaneMobileView);
            //await MopupService.Instance.PushAsync(new NafathPopup());
            await MopupService.Instance.PushAsync(new NafathChangeMobleNumberOptionsView());
        }
        private void ChangeIAMClicked(object obj)
        {
            _navigationService.NavigateTo(App.NafathLoginPageView);
            // _navigationService.NavigateTo(App.NafathPopUpPage);
        }
        #endregion
        #region property
        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                if (_appVersion == value) return;

                _appVersion = value;
                OnPropertyChanged("AppVersion");
            }
        }
        public string DeviceId { get; set; }
        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value)
                {
                    return;
                }

                password = value;
                this.OnPropertyChanged("Password");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                //if (this.email == value)
                //{
                //    return;
                //}
                PreviousUserName = Email;
                email = value;
                if (PreviousUserName != email)
                {
                    IsVisibleTinIds = false;
                }
                if (string.IsNullOrEmpty(email))
                {
                    IsLoginEnabled = false;
                    Password = string.Empty;
                    IsVisibleTinIds = false;
                }
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(Password))
                {
                    IsLoginEnabled = true;
                }
                OnPropertyChanged("Email");
            }
        }
        private bool _ISloadedURL;
        public bool ISloadedURL
        {
            get
            {
                return _ISloadedURL;
            }
            set
            {
                if (_ISloadedURL == value) return;

                //_ISloadedURL = value;
                OnPropertyChanged("ISloadedURL");
            }
        }
        private bool _IsSAMLLoginEnabled;
        public bool IsSAMLLoginEnabled
        {
            get
            {
                return _IsSAMLLoginEnabled;
            }
            set
            {
                if (_IsSAMLLoginEnabled == value) return;
                OnPropertyChanged("IsSAMLLoginEnabled");
            }
        }
        private bool _IsOldLoginHidden;
        public bool IsOldLoginHidden
        {
            get
            {
                return _IsOldLoginHidden;
            }
            set
            {
                if (_IsOldLoginHidden == value) return;

                OnPropertyChanged("IsOldLoginHidden");
            }
        }
        private bool _IsFocused = false;
        public bool IsFocused
        {
            get
            {
                return _IsFocused;
            }
            set
            {
                if (_IsFocused == value) return;

                _IsFocused = value;
                if (_IsFocused == true)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        bool Test = UtilityManager.IsValidEmailAddress(email);
                        if (Test == true)
                        {
                            if (IsVisibleTinIds == false)
                            {
                                IsVisibleTinIds = true;
                            }
                        }
                        else
                        {
                            IsVisibleTinIds = false;
                        }
                    }
                }
                OnPropertyChanged("IsFocused");
            }
        }
        private string _PreviousUserName = string.Empty;
        public string PreviousUserName
        {
            get
            {
                return _PreviousUserName;
            }
            set
            {
                if (_PreviousUserName == value) return;

                _PreviousUserName = value;
            }

        }
        private string _tINID = string.Empty;
        public string TINID
        {
            get
            {
                return _tINID;
            }
            set
            {
                if (_tINID == value) return;

                _tINID = value;
                OnPropertyChanged("TINID");
            }
        }
        private bool _isLoginEnabled = false;
        public bool IsLoginEnabled
        {
            get
            {
                return _isLoginEnabled;
            }
            set
            {
                if (_isLoginEnabled == value) return;

                _isLoginEnabled = value;
                OnPropertyChanged("IsLoginEnabled");
            }
        }
        private bool _isTinDropdownVisible = false;
        public bool IsTinDropdownVisible
        {
            get
            {
                return _isTinDropdownVisible;
            }
            set
            {
                if (_isTinDropdownVisible == value) return;

                _isTinDropdownVisible = value;
                OnPropertyChanged("IsTinDropdownVisible");
            }
        }
        private int _tINIndex = 0;
        public int TINIndex
        {
            get
            {
                return _tINIndex;
            }
            set
            {
                if (_tINIndex == value) return;

                _tINIndex = value;
                OnPropertyChanged("TINIndex");
            }
        }
        private GenericPickerModel _pickerModelTins { get; set; }
        public GenericPickerModel PickerModelTins
        {
            get { return _pickerModelTins; }
            set
            {
                if (_pickerModelTins == value) return;

                _pickerModelTins = value;
                OnPropertyChanged("PickerModelTins");
            }
        }

        private string _selectedTin { get; set; }
        public string SelectedTin
        {
            get { return _selectedTin; }
            set
            {
                if (_selectedTin == value) return;
                _selectedTin = value;
                OnPropertyChanged("SelectedTin");
            }
        }

        private List<TINModel> _tINs;
        public List<TINModel> TINs
        {
            get
            {
                return _tINs;
            }
            set
            {
                if (_tINs == value) return;

                _tINs = value;
                OnPropertyChanged("TINs");
            }
        }
        public string TIN
        {
            get
            {
                return this.tin;
            }
            set
            {
                if (this.tin == value)
                {
                    return;
                }
                this.tin = value;
                this.OnPropertyChanged("TIN");
            }
        }

        public bool LoginError
        {
            get
            {
                return this.loginError;
            }
            set
            {
                if (this.loginError == value)
                {
                    return;
                }
                this.loginError = value;
                this.OnPropertyChanged("LoginError");
            }
        }

        private TINModel _selectedTinId;
        public TINModel SelectedTinId
        {
            get
            {
                return _selectedTinId;
            }
            set
            {
                if (_selectedTinId == value) return;

                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    App.CurrentDropdownTIN = SelectedTinId;
                    TINID = _selectedTinId.TIN;
                    Password = string.Empty;
                }
                OnPropertyChanged("SelectedTinId");
            }
        }
        private TINModel _selectedTinIdPrev;
        public TINModel SelectedTinIdPrev
        {
            get
            {
                return _selectedTinIdPrev;
            }
            set
            {
                if (_selectedTinIdPrev == value) return;

                _selectedTinIdPrev = value;
                OnPropertyChanged("SelectedTinIdPrev");
            }
        }
        private EmailTinsModel _tinsList;
        public EmailTinsModel TinsList
        {
            get
            {
                return _tinsList;
            }
            set
            {
                if (_tinsList == value) return;

                _tinsList = value;
                OnPropertyChanged("TinsList");
            }
        }
        private bool _passwordVisibility = true;
        public bool PasswordVisibility
        {
            get
            {
                return _passwordVisibility;
            }
            set
            {
                if (_passwordVisibility == value) return;

                _passwordVisibility = value;
                OnPropertyChanged("PasswordVisibility");
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
                if (_isVisibleTinIds == value) return;

                _isVisibleTinIds = value;
                if (_isVisibleTinIds == true)
                {
                    TINs = new List<TINModel>();
                    Task.Run(async () =>
                    {
                        try
                        {
                            try
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = true;
                                });
                                TINs = WebServiceManager.SFGAZTGetAllTINs(Email);
                                if (TINs != null && TINs.Count != 0)
                                {
                                    if (SelectedTinId == null)
                                    {
                                        SelectedTinId = TINs[0];
                                    }
                                }
                                else
                                {
                                    MainThread.BeginInvokeOnMainThread(async () =>
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
                            catch (Exception)
                            {
                                IsVisibleTinIds = false;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    IsVisibleTinIds = false;
                                    await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                                });
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                            }
                        }
                        catch (GAZTException gex)
                        {
                            IsLoading = false;
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            });
                        }
                    });
                }
                OnPropertyChanged("IsVisibleTinIds");
            }
        }
        private string _NavigaateToThisService = string.Empty;
        public string NavigateToThisService
        {
            get
            {
                return _NavigaateToThisService;
            }
            set
            {

                if (_NavigaateToThisService == value) return;

                _NavigaateToThisService = value;
            }
        }
        #endregion
        #region Command
        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }
        public Command ChangeMCommand { get; set; }

        public Command ChangeCitizensCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command WebLoginCommand { get; set; }
        public Command SocialMediaLoginCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the Hamburger menu button is clicked.
        /// </summary>
        public ICommand HamburgerMenuClickedCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the Login button is clicked.
        /// </summary>
        public ICommand LoginClickedCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Tin dropdown clicked.
        /// </summary>
        public ICommand ShowTinsPickerCommand { get; set; }


        public static int LoginAttempt = 0;

        #endregion
        #region methods
        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            _navigationService.NavigateTo(App.EstablishmentSignUPPageView);
            // Do something
        }
        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            _navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
        }
        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();

            if (bs.Length == size)
                return bs;

            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");

            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }

        public static RSAParameters ToRSAParameters(RsaKeyParameters rsaKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = rsaKey.Modulus.ToByteArrayUnsigned();
            if (rsaKey.IsPrivate)
                rp.D = ConvertRSAParametersField(rsaKey.Exponent, rp.Modulus.Length);
            else
                rp.Exponent = rsaKey.Exponent.ToByteArrayUnsigned();
            return rp;
        }

        private async Task LoginButtonClicked()
        {
            IsLoading = true;
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFLoginPageView", "LoginButtonClicked", "Anonymous Menu Opened");
            try
            {
                Asn1Object obj = Asn1Object.FromByteArray(Convert.FromBase64String(ZATCAConstants.publicKeyStr));
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                DerSequence publicKeySequence = (DerSequence)obj;

                DerBitString encodedPublicKey = (DerBitString)publicKeySequence[1];
                DerSequence publicKey = (DerSequence)Asn1Object.FromByteArray(encodedPublicKey.GetBytes());

                DerInteger modulus = (DerInteger)publicKey[0];
                DerInteger exponent = (DerInteger)publicKey[1];
                RsaKeyParameters keyParameters = new RsaKeyParameters(false, modulus.PositiveValue, exponent.PositiveValue);
                var plainTextData = this.Password;

                RSAParameters parameters = ToRSAParameters(keyParameters);
                //for encryption, always handle bytes...
                var bytesPlainTextData = Encoding.UTF8.GetBytes(plainTextData);
                rsa.ImportParameters(parameters);

                //apply pkcs#1.5 padding and encrypt our data 
                var bytesCypherText = rsa.Encrypt(bytesPlainTextData, RSAEncryptionPadding.Pkcs1);

                //we might want a string representation of our cypher text... base64 will do
                var cypherText = Convert.ToBase64String(bytesCypherText);
                var lang = UtilityManager.GetLanguageParameter();
                var model = new LoginRequestModel()
                {
                    userId = IsTinDropdownVisible ? this.SelectedTin : this.TIN,
                    password = cypherText,
                    language = lang
                };
                var loginResponse = await WebServiceManager.LoginRequest(model);
                //App.Token = "";
                //App.MobileNumber = "9999971487";
                //App.LoginDataRetrieved = new LoginModel() { TIN = this.TIN };
                //_navigationService.NavigateTo(App.OtpLoginPageView);
                String response = loginResponse.Content.ReadAsStringAsync().Result;
                if (loginResponse != null && loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var result = JsonConvert.DeserializeObject<LoginResponseModel>(response);
                    App.Token = result.Result.Token;
                    App.MobileNumber = result.Result.MobileNumber;
                    App.LoginDataRetrieved = new LoginModel() { TIN = IsTinDropdownVisible ? this.SelectedTin : this.TIN };
                    LoginError = false;
                    _navigationService.NavigateTo(App.OtpLoginPageView);
                }
                else if (loginResponse != null && loginResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    LoginAttempt++;
                    LoginError = true;
                    if (LoginAttempt >= 3)
                    {
                        //_navigationService.GoBack();
                        _navigationService.NavigateTo(App.UnlockAccountTINPageView);
                        LoginAttempt = 0;
                    }
                }
                else if (loginResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // { "httpCode":"500","httpMessage":"Internal Server Error","moreInformation":"Internal Error"}
                    JObject json = JObject.Parse(response);
                    await _dialogService.ShowMessage(json.GetValue("httpMessage").ToString(), AppResources.ZError);
                }
            }
            catch (Exception ex)
            {
                //return String.Empty;
                System.Diagnostics.Debug.WriteLine("err : " + ex.StackTrace);
            }
            finally
            {
                IsLoading = false;
            }
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        internal async Task TinEntryUnfocusedAsync(string text)
        {
            IsLoading = true;
            int return_code = UtilityManager.CheckEmailOrTin(text.Trim());
            switch (return_code)
            {
                case 0:
                    IsTinDropdownVisible = false;
                    SelectedTin = string.Empty;
                    break;
                case 1:
                    IsTinDropdownVisible = false;
                    SelectedTin = string.Empty;
                    break;
                case 2:
                    TinsList = await WebServiceManager.GetTinsBasedOnEmail(text.Trim());

                    if (TinsList.Data != null && TinsList.Data.Count > 0)
                    {
                        SelectedTin = AppResources.PleaseSelectTIN;
                        IsTinDropdownVisible = true;
                    }
                    break;
            }
            IsLoading = false;
        }
        private async Task OpenTinsDropdown()
        {
            if (TinsList.Data != null && TinsList.Data.Count > 0)
            {
                await PrepareTinsDropDown(TinsList.Data);
            }
        }
        private async Task PrepareTinsDropDown(List<Models.Authentication.Datum> data)
        {
            try
            {
                setTinsPickerModel(data);
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelTins));
            }
            catch (GAZTUnlockAccountException)
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

        private void setTinsPickerModel(List<Models.Authentication.Datum> data)
        {
            if (PickerModelTins != null)
            {
                PickerModelTins = null;
            }
            var list = new List<string>();
            list.Add(AppResources.PleaseSelectTIN);
            foreach (Models.Authentication.Datum dropdown in data)
            {
                try
                {
                    list.Add(dropdown.TINNumber);
                }
                catch (Exception)
                {
                }
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "EntityTinPicker";
            genericPickerModel.PageCode = 1;
            PickerModelTins = genericPickerModel;
            SelectedTin = SelectedTin;
        }
        private void HamburgerMenuClicked()
        {

            var callTracker = Instrumentation.BeginCall("SFLoginPageView", "HamburgerMenuClicked", "Anonymous Menu Opened");
            _navigationService.NavigateTo(App.DashboardAnonymousMenuPageView);
            Instrumentation.EndCall(callTracker);
        }
        #endregion
        #region New Authentication

        public string CreateLoginURL(string lang)
        {
            try
            {
                string deviceOs = DeviceInfo.Platform.ToString();
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                return WebServiceManager.CreateSAMLLoginURL("", deviceUdid, "", deviceOs, lang);

            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task LoginCompletedInWebView()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);

            //string lang = "E";
            string language = UtilityManager.GetLanguageParameter();

            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            // * OLD TP PROFILE API
            //TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

            // * NEW TP PROFILE API
            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);

            if (TPProfile != null)
            {


                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.userId = TPProfile.TIN;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.firstName = App.LoginDataRetrieved.NameFirst;
                            App.TP.lastName = App.LoginDataRetrieved.NameLast;
                            App.TP.organizationName = App.LoginDataRetrieved.NameOrg1;
                            App.TP.typeCheck = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception)
                {


                }

            }

            string OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            string OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        App.HasToRefreshLoaderOnDashboard = true;
                    });


                }
                catch (GAZTInternetException)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }

            });

        }

        public async Task LoginCompletedInWebViewForVATRegistrationTestPurpose()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);


            string language = UtilityManager.GetLanguageParameter();



            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);
            if (TPProfile != null)
            {

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.userId = App.LoginDataRetrieved.TIN;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.firstName = App.LoginDataRetrieved.NameFirst;
                            App.TP.lastName = App.LoginDataRetrieved.NameLast;
                            App.TP.organizationName = App.LoginDataRetrieved.NameOrg1;
                            App.TP.typeCheck = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception)
                {


                }
            }

            string OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            string OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView, false);
                        App.HasToRefreshLoaderOnDashboard = true;
                        //TODO for continue work on EST added by ashwini
                        //_navigationService.NavigateTo(App.EstablishmentRegistrationPage);
                    });


                }
                catch (GAZTInternetException)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }

            });

        }

        public async Task Logout()
        {
            await WebServiceManager.GAZTLogOff();
            App.IsLogOut = true;
            App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
        }

        #endregion
    }
}