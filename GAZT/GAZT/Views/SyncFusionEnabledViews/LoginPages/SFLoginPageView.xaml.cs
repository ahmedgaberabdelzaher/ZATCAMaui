using GAZT;
using GAZT.Helper;
using GAZTeServicesApp.ViewModels.LoginPage;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
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
        private string strNavigaateToThisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(String strNavigateToThisService)
        {
            SetLTRDirection();
            InitializeComponent();
            
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;
            viewModel.NavigateToThisService = strNavigateToThisService;
            if (App.IsArabic)
            {

                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.TestPicker", Application.Current.GetType().Assembly);

            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Application.Current.GetType().Assembly);
            }
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