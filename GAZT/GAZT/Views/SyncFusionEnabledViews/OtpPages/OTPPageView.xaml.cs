using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.OTPPage_ViewModel;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.OTPPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPPageView : ContentPage
    {
        #region Variable
        OTPPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        double DeviceHeight;
        double DeviceWidth;
        byte[] data;
        #endregion
        #region Constructor
        public OTPPageView(ComingToOTPVerificationScreenFromAndNavigatingTo _ComingToOTPVerificationScreenFromAndNavigatingTo)
        {
            InitializeComponent();
            // TimeZone localZone = TimeZone.CurrentTimeZone;// Got +3
            // string cd = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’ ‘zzz’ ");
            // // dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");
            // DateTimeOffset local_offset = new DateTimeOffset(DateTime.Now.ToLocalTime());
            // DateTimeOffset utc_offset = local_offset.ToUniversalTime();
            // string str = utc_offset.DateTime.ToLongTimeString();
            // string str1 = utc_offset.DateTime.ToShortDateString();
            // const string dataFmt = "{0,-30}{1}";
            // const string timeFmt = "{0,-30}{1:yyyy-MM-dd HH:mm}";
            // TimeZone localZone = TimeZone.CurrentTimeZone;
            // DateTime currentDate = DateTime.Now;
            // int currentYear = currentDate.Year;
            // string standardName = localZone.StandardName;
            // string dayLightTime = localZone.DaylightName;
            // string Currentdateandtime = string.Format("\n" + timeFmt, "Current date and time:",
            // currentDate);
            // string DaylightTime = string.Format(dataFmt, "Daylight saving time?",
            // localZone.IsDaylightSavingTime(currentDate));
            // DateTime currentUTC =
            //localZone.ToUniversalTime(currentDate);
            // TimeSpan currentOffset =
            //     localZone.GetUtcOffset(currentDate);
            // string CoordinatedUniversalTime = string.Format(timeFmt, "Coordinated Universal Time:",
            //     currentUTC);
            // string UTCoffset = string.Format(dataFmt, "UTC offset:", currentOffset);
            //DaylightTime daylight =  localZone.GetDaylightChanges(currentYear);
            viewModel = App.Locator.OTPPageView;
            this.BindingContext = viewModel;
            viewModel.numberOfSeconds = 120;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            ChangeAeroIcon();
          
            viewModel.ComingToOTPVerificationScreenFromAndNavigatingTo = _ComingToOTPVerificationScreenFromAndNavigatingTo;
            try
            {
                if (viewModel.ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsLogin)
                {
                    viewModel.IsNumberOfAttemptTextVisible = true;
                }
                else
                {
                    viewModel.IsNumberOfAttemptTextVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
            NumberOfAttemptsText.Text = viewModel.ShowAccountWIllBeLockedMessage();
            SetLTR();
         
            try
            {
                viewModel.OnPageLoad();
            }
            catch (GAZTException gex)
            {
                if (gex is GAZTMobileNumberInProfileEmptyException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessageBox(AppResources.MobileNumberIsMissingForEnteredTIN, AppResources.Alerts);
                        viewModel._navigationService.GoBack();
                    });
                    return;
                }
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Alerts);
                    viewModel._navigationService.GoBack();
                });
                return;
            }
       
            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            viewModel.IsComingFrom = _ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom;
            if (_ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsMobile)
            {
                if (App.TP != null)
                {
                    var firstDigits = "";
                    var lastDigits = "";
                    if (App.IsArabic)
                    {
                        viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                        viewModel.OTPSentOnThisMobileNumber = App.TP.NewMobile;
                        var MobileNumber = viewModel.OTPSentOnThisMobileNumber;
                        //MobileNumber = viewModel.OTPSentOnThisMobileNumber.Substring(3, 9);
                        firstDigits = MobileNumber.Substring(0, 2);
                        lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                           // MobileNumber = "9665" + MobileNumber + "+";
                        }
                        else
                        {
                            //MobileNumber = "+9665" + MobileNumber;
                        }
                        //string _mobileNumber = App.TP.NewMobile.Substring(8, 4);
                        //viewModel.MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                        var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);
                        var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                        var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    }
                    else
                    {
                        viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                        viewModel.OTPSentOnThisMobileNumber = App.TP.NewMobile;
                        var MobileNumber = viewModel.OTPSentOnThis;
                        MobileNumber = viewModel.OTPSentOnThisMobileNumber.Substring(4, 9);
                        firstDigits = MobileNumber.Substring(1, 2);
                        lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);
                        MobileNumber = "+9665" + MobileNumber;
                        string _mobileNumber = App.TP.NewMobile.Substring(9, 4);
                        viewModel.MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                        var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);
                        var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                        var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    }
                    if (App.IsArabic)
                    {
                        //if (Device.RuntimePlatform == Device.iOS)
                        //{
                        //viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode +" "+ lastDigits + "***" + firstDigits;
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode +" "+ lastDigits + "*****" ;
                        //}
                        //else
                        //{
                        //    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + firstDigits + "***" + lastDigits;
                        //}
                    }
                    else
                    {
                        //viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode +" "+ "5"+firstDigits + "***" + lastDigits;
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode +" "+  "*****" + lastDigits;
                    }
                }
                if (!App.IsArabic)
                {
                    viewModel.AccountWillBeBlocked = "The account will be locked after entering " + App.TP.Attempts + " wrong verification codes";
                }
                else
                {
                    viewModel.AccountWillBeBlocked = "سيتم قفل الحساب بعد إدخال" + " " + UtilityManager.ConvertNumerals(App.TP.Attempts.ToString()) + " " + "رموز تحقق خاطئة";
                }
            }
            else if (_ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsEmail)
            {
                viewModel.EmailOrMobileNumber = AppResources.Email;
                if (App.TP != null)
                {
                    // string _newEmail = App.TP.NewEmail.Substring(App.TP.Mobile.Length - 4);
                    viewModel.MobileNumber = App.TP.NewEmail;// "XXXXXXXXXX" + _mobileNumber;
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCodeForEmail;
                    viewModel.OTPSentOnThisEmail = App.TP.NewEmail;
                    viewModel.OTPSentOnThisText = viewModel.OTPSentOnThisText + " " + viewModel.OTPSentOnThisEmail;
                }
                if (!App.IsArabic)
                {
                    viewModel.AccountWillBeBlocked = "The account will be locked after entering " + App.TP.Attempts + " wrong verification codes";
                }
                else
                {
                    viewModel.AccountWillBeBlocked = "سيتم قفل الحساب بعد إدخال" + " " + UtilityManager.ConvertNumerals(App.TP.Attempts.ToString()) + " " + "رموز تحقق خاطئة";
                }
            }
            else if (_ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsLogin)
            {
                if (App.TP != null)
                {
                    viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode;
                    viewModel.OTPSentOnThisMobileNumber = App.TP.Mobile;
                    viewModel.OTPSentOnThis = viewModel.OTPSentOnThisMobileNumber;
                    var MobileNumber = viewModel.OTPSentOnThis;
                    //if(MobileNumber.Contains("+"))
                    //{
                    //    MobileNumber = MobileNumber.Replace("+", "00");
                    //}
                    //else if(MobileNumber.Substring(0,1)=="0")
                    //{
                    //    MobileNumber = "00" + MobileNumber.Substring(1, MobileNumber.Length - 1);
                    //}
                    //else if(MobileNumber.Substring(0, 2) == "00")
                    //{
                    //}
                    //else
                    //{
                    //    MobileNumber = "00" + MobileNumber;
                    //}
                    if (App.TP != null && !string.IsNullOrEmpty(MobileNumber))
                        MobileNumber = MobileNumber.Substring(MobileNumber.Length - 9);
                    else
                        throw new GAZTMobileNumberInProfileEmptyException();
                    // MobileNumber = MobileNumber.Substring(5, 9);
                    var firstDigits = MobileNumber.Substring(0, 2);
                    var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);
                    var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);
                    string maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                    var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    if (App.IsArabic)
                    {
                        //if (Device.RuntimePlatform == Device.iOS)
                        //{
                        //viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + lastDigits + "***" + firstDigits;
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + lastDigits + "*****" ;
                        //}
                        //else
                        //{
                        //    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                        //}
                    }
                    else
                    {
                       // viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : "+ "*****" + lastDigits;
                    }
                }
                DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
                DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();
            }
            else if (_ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsTes)
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    viewModel.IsLoading = false;
                });

                viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                viewModel.TesReporterMobileNumber = _ComingToOTPVerificationScreenFromAndNavigatingTo.MobileNumber;
                // NumberOfAttemptsText.IsVisible = false;
            }
        }
        #endregion
        #region Method
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                }
                else
                {
                    this.BackgroundImageSource = "sf_LoginBackground.png";
                    //  outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        #endregion
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            if (viewModel.ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsTes)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = false;
                });

                App.IsOTPiew = true;
                viewModel.TimerStart(viewModel.numberOfSeconds);
                viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                viewModel.IsResendOTPEnabled = false;
                viewModel.IsOTPEntryEnable = true;
                viewModel.currentAttempts = 0;
                await Task.Run(() =>
                {
                    Task.Delay(100);
                });
                EnteredOTP.Focus();
            }
            else
            {
                App.IsOTPiew = true;
                viewModel.TimerStart(viewModel.numberOfSeconds);
                viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                viewModel.IsResendOTPEnabled = false; 
                viewModel.IsOTPEntryEnable = true;
                viewModel.currentAttempts = 0;
                await Task.Run(() =>
                {
                    Task.Delay(100);
                });
                EnteredOTP.Focus();
            }
        }
        private async void OnOTPEntered(Object sender, EventArgs e)
        {
            string Otp = EnteredOTP.Text;
            //if(Otp.Length > 0)
            //{
            //    bool isValidNumber = UtilityManager.IsOTPNumberValid(Otp);
            //    if(!isValidNumber)
            //    {
            //        EnteredOTP.Text = Otp.Substring(0, Otp.Length-1);
            //    }
            //}
            //if (Otp.Length > 4)
            //{
            //    EnteredOTP.Text = EnteredOTP.Text.Substring(0, 4);
            //    EnteredOTP.Unfocus();
            //}
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
            viewModel.StopTimer = false;
            App.IsOTPiew = false;
            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var fileData = await CrossFilePicker.Current.PickFile();
                //data = fileData.DataArray;
                //lbl.Text = fileData.FileName;
                //AttachmentRootOject _attachment =  await WebServiceManager.GAZTSaveVATDeclarationAttachment(data);
            }
            catch (Exception ex)
            {
            }
        }
    }
}