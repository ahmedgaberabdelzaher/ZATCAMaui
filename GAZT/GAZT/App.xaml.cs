using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GAZT.Views;
using System.Globalization;
using GAZT.CustomControl;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
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

        

        // public static bool IsArabic = false;
        public static bool IsArabic = true;
        public static bool IsOTPiew = false;
        public static double NavigationBarHeightt = 0;
        public static CultureInfo ci;
        public App()
        {
            String langName = "ar-AE";//"en-US";// "ar-AE";
            ci = new CultureInfo(langName);
            AppResources.Culture = ci;

            InitializeComponent();
            CustomNavigation navigationPage = new CustomNavigation(new LogInView());

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
