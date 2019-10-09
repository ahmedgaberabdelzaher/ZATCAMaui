using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GAZT.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GAZT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LogInView();
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
