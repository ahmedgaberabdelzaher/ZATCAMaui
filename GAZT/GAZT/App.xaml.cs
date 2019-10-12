using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GAZT.Views;
using System.Globalization;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GAZT
{
    public partial class App : Application
    {
        public static bool IsArabic = false;
        public static CultureInfo ci;
        public App()
        {
            String langName = "ar-AE";// "ar-AE";
            ci = new CultureInfo(langName);
            AppResources.Culture = ci;

            InitializeComponent();

            MainPage = new DashboardView();
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
