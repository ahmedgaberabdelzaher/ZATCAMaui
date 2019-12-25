using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GAZT.Views;
using System.Globalization;
using GAZT.CustomControl;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System.Net.Http;
using GAZT.Views.NewViews;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GAZT
{
    public partial class App : Application
    {
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



        public static TIN CurrentDropdownTIN;

        // public static bool IsArabic = false;
        public static bool IsArabic = true;
        public static bool IsOTPiew = false;
        public static TaxPayerProfile TP = null;
        public static string Token = String.Empty;
        public static string Otp = String.Empty;

        public static double NavigationBarHeightt = 0;
        public static CultureInfo ci;
        public static bool IsComingFromDashboardToLogOff = false;


        //HttpClientHandlerForSSL Certificate Issue
        public static HttpClientHandler httpClientHandler = null;



        public App()
        {
            String langName = "ar-AE";//"en-US";// "ar-AE";
            ci = new CultureInfo(langName);
            AppResources.Culture = ci;

            InitializeComponent();
            CustomNavigation navigationPage = new CustomNavigation(new LogInPageView());

            var navigationService = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.Initialize(navigationPage);
            var dialogService = (DialogService)ServiceLocator.Current.GetInstance<IDialogService>();
            dialogService.Initialize(navigationPage);
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
