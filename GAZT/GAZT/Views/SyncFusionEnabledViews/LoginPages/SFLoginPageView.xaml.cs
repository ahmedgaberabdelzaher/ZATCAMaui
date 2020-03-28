using GAZT;
using GAZT.Helper;
using GAZTeServicesApp.ViewModels.LoginPage;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.LoginPage
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLoginPageView
    {
        SFLoginPageViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView()
        {
            SetLTRDirection();
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;
            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            viewModel.TINIndex = 0;
            // ParentContainer.RaiseChild(BusyIndicator);
        }

        public void SetLTRDirection()
        {
            App.IsArabic = false;
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            //InitializeComponent();
           // this.FlowDirection = FlowDirection.LeftToRight;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.TP = null;
            viewModel.CurrentAttempt = 0;
            if (App.CurrentDropdownTIN != null)
                viewModel.SelectedTinId = App.CurrentDropdownTIN;
            if (App.IsSessionExpired)
            {
                await viewModel._dialogService.ShowMessageBox(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
            }
            else
            {

            }
            viewModel.IsVisibleTinIds = false;
        }

        private void OnPasswordVisibilityClicked(object sender, EventArgs e)
        {
            viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        }
    }
}