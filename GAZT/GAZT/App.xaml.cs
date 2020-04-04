using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using GAZT.CustomControl;
using GAZT.Models;
using GAZTeServicesApp.Views.LandingPage;
using System;
using System.Globalization;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GAZT
{
    public partial class App : Application
    {
        //SYNCFUSION INTEGRATION

        public static string SFLandingPageView = "SFLandingPageView";
        public static string SFOptionsPageView = "SFOptionsPageView";
        public static string SFLoginPageView = "SFLoginPageView";
        public static string SFAnonymousLandingPageView = "SFAnonymousLandingPageView"; 

        //SYNCFUSION INTEGRATION


        public static string LoginView = "LoginView";
        public static string OTPView = "OTPView";
        public static string DashboardView = "DashboardView";
        public static string MyCertificate = "MyCertificate";
        public static string TaxPayerProfileView = "TaxPayerProfileView";
        public static string PdfView = "PdfView";
        public static string UpdateEmailAddress = "UpdateEmailAddress";
        public static string ForgotUsernamePassword = "ForgotUsernamePassword";

        public static string TPProfileView = "TPProfileView";
        public static string VerifyEmailAddressView = "VerifyEmailAddressView";
        public static string ChangeMobileNumberView = "ChangeMobileNumberView";
        public static string ChangePasswordView = "ChangePasswordView";
        public static string MyBillsView = "MyBillsView";
        public static string PdfiOSView = "PdfiOSView";

        public static string LogInPageView = "LogInPageView";
        public static string DashboardPageView = "DashboardPageView";
        public static string TaxPayerProfilePageView = "TaxPayerProfilePageView";
        public static string ChangeMobileNumberPageView = "ChangeMobileNumberPageView";
        public static string ChangeEmailPageView = "ChangeEmailPageView";
        public static string ChangePasswordPageView = "ChangePasswordPageView";
        public static string OTPPageView = "OTPPageView";
        public static string ForgotUsernamePasswordPageView = "ForgotUsernamePasswordPageView";
        public static string VATLookupPageView = "VATLookupPageView";
        public static string ZakatReturnListPageView = "ZakatReturnListPageView";
        public static string ZakatReturnDetailsPageView = "ZakatReturnDetailsPageView";
        public static string BillDetailsPageView = "BillDetailsPageView";
        public static string SalesDetailsPageView = "SalesDetailsPageView";
        public static string AmendSalesDetailsPageView = "AmendSalesDetailsPageView";
        public static string ICRListPageView = "ICRListPageView";
        public static string CheckTINStatusPageView = "CheckTINStatusPageView";
        public static string VATReturnsPageView = "VATReturnsPageView";
        public static string ZakatBillDetailsPageView = "ZakatBillDetailsPageView";
        public static string AAcknowledgementView = "AAcknowledgementView";
        public static string AcknowledgementDetailsPageView = "AcknowledgementDetailsPageView";
        public static string DisplayNotesPageView = "DisplayNotesPageView";
        public static string AttachmentPageView = "AttachmentPageView";
        public static string AddNotePageView = "AddNotePageView";
        public static string AddPopPageView = "AddPopPageView";
        public static string CreditCarriedPageView = "CreditCarriedPageView";
        public static string CorrespondancePageView = "CorrespondancePageView";
        public static string CorrespondenceDetailsPageView = "CorrespondenceDetailsPageView";
        public static string FormBundleStatusPageView = "FormBundleStatusPageView";
        public static string SignUpTAndCViewPage = "SignUpTAndCViewPage";
        public static string SignUpFormPageView = "SignUpFormPageView";
        public static string CreateGaztAccountPageView = "CreateGaztAccountPageView";
        public static string TaxEvasionReportTypePageView = "TaxEvasionReportTypePageView";
        public static string TaxEvasionReportFormPageView = "TaxEvasionReportFormPageView";
        public static string TaxEvasionReportFormAttachmentPageView = "TaxEvasionReportFormAttachmentPageView";
        public static string AccountCreatedPageView = "AccountCreatedPageView";
        public static string TaxEvasionReportListPageView = "TaxEvasionReportListPageView";
        public static string ReturnsPageView = "ReturnsPageView";
        public static string FAQPageView = "FAQPageView";
        public static string AboutUsPageView = "AboutUsPageView";
        public static string PrivacyAndPolicyPageView = "PrivacyAndPolicyPageView";




        public static string fontFamilyBold = null;
        public static string fontFamilyMedium = null;
        public static string fontFamilyLight = null;
        public static string fontFamilyRoman = null;
        public static TIN CurrentDropdownTIN;

        // public static bool IsArabic = false;
        public static bool PreviousIsArabic = false;//true
        public static bool IsArabic = false;//true
        public static bool IsOTPiew = false;
        public static string ICRStatus = String.Empty;
        public static TaxPayerProfile TP = null;
        public static string Token = String.Empty;
        public static string Otp = String.Empty;
        public static bool IsSessionExpired = false;
        public static string AppVersion { get; set; }

        public static double NavigationBarHeightt = 0;
        public static CultureInfo ci;

        public static App appObj;
        public static DateTime TimeAtSleep { get; set; }
        public static DateTime TimeAtResume { get; set; }
        public static double TimeDifference { get; set; }
        public static bool IsComingFromSleepMode { get; set; } = false;

        public static bool IsComingFromDashboardToLogOff = false;

        //HttpClientHandlerForSSL Certificate Issue
        public static HttpClientHandler httpClientHandler = null;
        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIzNTEwQDMxMzcyZTM0MmUzMEJUZG1sRWtvcDRKQTJYUkpTdm5lcXFHbzAzenUvNS81RTZ3SlBwdlN1Njg9");

            AppResources.Culture = CultureInfo.CurrentUICulture;

            InitializeComponent();

            onFontFamilyChanged();

            try
            {
                httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            }
            catch (Exception ex)
            {

            }

            VATDeclaration vAT = null;

            //CustomNavigation navigationPage = new CustomNavigation(new TaxEvasionReportTypePageView()) { BarTextColor = Color.White };
            CustomNavigation navigationPage = new CustomNavigation(new SFAnonymousLandingPageView()) { BarTextColor = Color.White };
            //   new NavigationPage(YouPage) { BarBackgroundColor = Color.White }

            var navigationService = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.Initialize(navigationPage);
            
            var dialogService = (DialogService)ServiceLocator.Current.GetInstance<IDialogService>();
            dialogService.Initialize(navigationPage);


            MainPage = navigationPage;
        }

        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }
        private static App _Instance;
        public static App GetInstance()
        {
            if (_Instance == null)
                _Instance = new App();
            return _Instance;
        }
        public static void changeFontFamily(App app)
        {
            PreviousIsArabic = App.IsArabic;

            app.onFontFamilyChanged();
            App.IsArabic = PreviousIsArabic;
        }

        public void onFontFamilyChanged()
        {
            if (PreviousIsArabic)
            {
                String langName = "ar-AE";//"en-US";// "ar-AE";
                ci = new CultureInfo(langName);
                AppResources.Culture = ci;
            }
            else
            {
                String langName = "en-US";//"en-US";// "ar-AE";
                ci = new CultureInfo(langName);
                AppResources.Culture = ci;
            }
            if (PreviousIsArabic)
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        fontFamilyBold = "GE_SS_Two_Bold";
                        fontFamilyMedium = "GE_SS_Two_Medium";
                        fontFamilyLight = "GE_SS_Two_Light";
                        fontFamilyRoman = "SSTArabic-Roman";
                        break;
                    case Xamarin.Forms.Device.Android:
                        fontFamilyBold = "GE_SS_Two_Bold.ttf#GE_SS_Two_Bold";
                        fontFamilyMedium = "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        fontFamilyLight = "GE_SS_Two_Light.ttf#GE_SS_Two_Light";
                        fontFamilyRoman = "SSTArabic-Roman.ttf#SSTArabic-Roman";
                        break;
                }

            }
            else
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        fontFamilyBold = "SSTArabic-Bold";
                        fontFamilyMedium = "SSTArabic-Medium";
                        fontFamilyLight = "SSTArabic-Light";
                        fontFamilyRoman = "SSTArabic-Roman";
                        break;
                    case Xamarin.Forms.Device.Android:
                        fontFamilyBold = "SSTArabic-Bold.ttf#SSTArabic-Bold";
                        fontFamilyMedium = "SSTArabic-Medium.ttf#SSTArabic-Medium";
                        fontFamilyLight = "SSTArabic-Light.ttf#SSTArabic-Light";
                        fontFamilyRoman = "SSTArabic-Roman.ttf#SSTArabic-Roman";

                        break;
                }
            }

            GAZTTextBoxStyleForEntry.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = "SSTArabic-Bold" });
            GAZTSmallGreenLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });

            MiniGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            ForgotPasswordTextColor.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            InformationRedColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            MandatoryRedColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            InformationGrayColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            SmallWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            SmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            MyBillsSmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            MyBillsMediumMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            MiniGrayLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            MiniBlackLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            PickerStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTSmallGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTGreenLabelStyleForDashboardIcon.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            GAZTCaptionGreenLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            GAZTVerifyButton.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            GAZTGreenLabelStyleForEservicesIcon.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            GAZTGreenLabelStyleForMicro.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTGoldLabelStyleForSmallFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTGoldLabelStyleForCaptionFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTGrayLabelStyleForSmallFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTGrayLabelStyleForCaptionFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTDropdownStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            GAZTSmallGreenLabelStyleForSteps.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            GAZTGreyLabelStyleForOptionMenu.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            TabbedPageSmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            TabbedPageMediumMiniGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
            forBoldLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
            forLightLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
            MicroGrayLabelStyleNew.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyRoman });
            MicroGrayLabelStyleNewEn.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyRoman });
            //SYNCFUSION INTEGRATION

            if (App.IsArabic)
            {
                    Application.Current.Resources["GAZT_FONT_BOLD"] = Application.Current.Resources["GAZT_Arabic_FONT_BOLD"];
                    Application.Current.Resources["GAZT_FONT_MEDIUM"] = Application.Current.Resources["GAZT_Arabic_FONT_MEDIUM"];
                    Application.Current.Resources["GAZT_FONT_REGULAR"] = Application.Current.Resources["GAZT_Arabic_FONT_REGULAR"];
            }
            else
            {
                Application.Current.Resources["GAZT_FONT_BOLD"] = Application.Current.Resources["GAZT_English_FONT_BOLD"];
                Application.Current.Resources["GAZT_FONT_MEDIUM"] = Application.Current.Resources["GAZT_English_FONT_MEDIUM"];
                Application.Current.Resources["GAZT_FONT_REGULAR"] = Application.Current.Resources["GAZT_English_FONT_REGULAR"];
            }


            //SYNCFUSION INTEGRATION

        }

        protected override void OnStart()
        {
            Distribute.ReleaseAvailable = OnReleaseAvailable;

            // Handle when your app starts
            AppCenter.Start("ios=e91bd801-4e1c-4f62-8075-4732d2a1240a;" +
                  "uwp={Your UWP App secret here};" +
                  "android=c4abea0b-7d25-4680-9354-b0c3e4b2fb7a",
                  typeof(Analytics), typeof(Crashes), typeof(Distribute));

         

            try
            {
                Crashes.GenerateTestCrash();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }

        }

        bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user cannot postpone
            if (releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // Note that this method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you are using your own dialog, false otherwise
            return true;
        }

        protected override void OnSleep()
        {
            TimeAtSleep = DateTime.Now;

            //TimeAtSleep = dt.ToLongTimeString();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            TimeAtResume = DateTime.Now;
            TimeDifference = (TimeAtResume - TimeAtSleep).TotalSeconds;
            IsComingFromSleepMode = true;
        }

    }
}
