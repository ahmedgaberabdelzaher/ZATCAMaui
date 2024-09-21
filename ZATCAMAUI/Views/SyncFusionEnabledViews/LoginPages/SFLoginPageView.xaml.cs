using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLoginPageView : ContentPage
    {
        SFLoginPageViewModel viewModel;


        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(string strNavigateToThisService)
        {
            try
            {
                InitializeComponent();

                App.VATType = PageExecutionType.Register;
                App.ZAKATType = PageExecutionType.Register;
                viewModel = App.Locator.SFLoginPageView;

                this.BindingContext = viewModel;
                viewModel.CurrentTab = 1;
                Preferences.Set("first_TimeLoging_key", "False");
                viewModel.NavigateToThisService = strNavigateToThisService;

                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;
                viewModel.TINIndex = 0;

            }
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                MessagingCenter.Subscribe<string>(this, "TinList", message =>
                {
                    viewModel.IsVisibleTinIds = true;
                });
                InitPopups();
                viewModel.Password = string.Empty;
                viewModel.Email = string.Empty;
                LoginTin.Text = string.Empty;
                LoginPassword.Text = string.Empty;
                viewModel.SelectedTin = string.Empty;
                viewModel.IsTinDropdownVisible = false;

                App.TP = null;
                App.HasToRefreshLoaderOnDashboard = true;
                viewModel.CurrentAttempt = 0;
                if (App.CurrentDropdownTIN != null)
                    viewModel.SelectedTinId = App.CurrentDropdownTIN;

                viewModel.IsVisibleTinIds = false;

                loginLabel.Text = AppResources.LoginText;
                WelcomeLabel.Text = AppResources.WelcomeLine;
                forgotPasswordLabel.Text = AppResources.LoginForgotPassword;
                changeMobileNumberLabel.Text = AppResources.LoginChangeMoblNum;
                loginLineLabel.Text = AppResources.LoginLine;
                loginBtn.Text = AppResources.LoginText;
                LoginTin.Placeholder = AppResources.TINPlaceholder;
                LoginPassword.Placeholder = AppResources.PasswordPlaceholder;
                registerLabel.Text = AppResources.LoginRegister;

            }
            catch (Exception )
            {
            }
        }

        public SFLoginPageView()
        {
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;

            viewModel.CurrentTab = 1;
            viewModel.IsLoading = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.password = string.Empty;
            viewModel.email = string.Empty;
            viewModel.Password = string.Empty;
            viewModel.Email = string.Empty;
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");
        }


      async  void LoginTin_Unfocused(System.Object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
           await viewModel.TinEntryUnfocusedAsync(entry.Text);

            //var x = UtilityManager.CheckEmailOrTin(entry.Text);
        }

        private void InitPopups()
        {
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected", (sender, arg) =>
            {
                var selectedType = string.Empty;
                string SelectedIDTypeValue = string.Empty;
                if (arg.PickerId == "EntityTinPicker")
                {
                    viewModel.SelectedTin = arg.SelectedValue;
                }
            });
        }

        void LoginTin_Focused(System.Object sender, FocusEventArgs e)
        {
            viewModel.IsTinDropdownVisible = false;
            viewModel.SelectedTin = string.Empty;
        }

        private void ImageSeePassword_Tapped(object sender, TappedEventArgs e)
        {
            if (viewModel.IsPasswordEncripted)
            {
                viewModel.IsPasswordEncripted = false;
                ImageSeePassword.Source = "showPassword";
            }
            else
            {
                viewModel.IsPasswordEncripted = true;
                ImageSeePassword.Source = "hidePassword";
            }
        }



    }
}