using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPageView : ContentPage
    {
        LogInPageViewModel viewModel;
        int LanguageToolBarCount = 0;
        public LogInPageView()
        {
            viewModel = App.Locator.LogInPageView;

            InitializeComponent();
            GetDeviceID();
            NavigationPage.SetBackButtonTitle(this, "");
            //long number = 1000000000000;
            //string whatYouWant = number.ToString("#,##0");
            NavigationPage.SetBackButtonTitle(this,"");
            App.IsArabic = true;
            this.BindingContext = viewModel;
            App.IsComingFromDashboardToLogOff = false;
           viewModel.PasswordVisibility = true;

            ToolbarItem AnnonymousService = new ToolbarItem
            {
                Icon = "ic_Paid.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                })
            };
            this.ToolbarItems.Add(AnnonymousService);
            ToolbarItem toolbarItem1 = new ToolbarItem
            {
                Icon = "ic_language.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    if (App.IsArabic)
                    {
                        App.IsArabic = false;
                        SetLTRDirection();
                    }
                    else
                    {
                        App.IsArabic = true;
                        SetRTLDirection();
                    }
                })
            };
            if (LanguageToolBarCount == 0)
            {
                LanguageToolBarCount = 1;
                this.ToolbarItems.Add(toolbarItem1);
            }


          
            
        }

       
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.TP = null;
            viewModel.CurrentAttempt = 0;
            if (App.CurrentDropdownTIN != null)
                viewModel.SelectedTinId = App.CurrentDropdownTIN;
            //viewModel.UserName = String.Empty;
            //viewModel.Password = String.Empty;
            //  await WebServiceManager.GetAllGAZTCertificate("EN", "");
            if (App.IsSessionExpired)
            {
                await viewModel._dialogService.ShowMessageBox(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
            }
            else
            {

            }
            viewModel.UserName = string.Empty;
            viewModel.Password = string.Empty;
            viewModel.IsVisibleTinIds = false;

        }
        public void OnPasswordVisibilityClicked(object sender, EventArgs args)
        {
            viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        }
        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();

            this.FlowDirection = FlowDirection.RightToLeft;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            InitializeComponent();
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void GetDeviceID()
        {
          string  deviceId = System.Guid.NewGuid().ToString();
            viewModel.DeviceId = deviceId;
        }
    }
}