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
            App.IsArabic = true;
            this.BindingContext = viewModel;
           // viewModel.PasswordVisibility = true;
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
            if (App.CurrentDropdownTIN != null)
                viewModel.SelectedTinId = App.CurrentDropdownTIN;
            //viewModel.UserName = String.Empty;
            //viewModel.Password = String.Empty;
            //  await WebServiceManager.GetAllGAZTCertificate("EN", "");
            if (App.IsComingFromDashboardToLogOff && !App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Alert!", AppResources.LogoutConfirmationMessage, "Yes", "No");
                    if (!result)
                    {
                        viewModel._navigationService.NavigateTo(App.DashboardPageView);
                    }
                    else
                    {
                        App.TP = null;
                        viewModel.UserName = string.Empty;
                        viewModel.Password = string.Empty;
                        viewModel.IsVisibleTinIds = false;
                    }
                });
            }
            else if (App.IsSessionExpired)
            {
                await viewModel._dialogService.ShowMessageBox("Your Session has expired,Please Login again", AppResources.Information);
            }
            else
            {

            }
            //App.TP = null;
            //viewModel.UserName = string.Empty;
            //viewModel.Password = string.Empty;
            //viewModel.IsVisibleTinIds = false;
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
    }
}