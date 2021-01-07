using EGAZT.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage_ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.AccountCreated
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreatedPageView : ContentPage
    {
        AccountCreatedPageViewModel viewModel;
        public AccountCreatedPageView()
        {
            viewModel = App.Locator.AccountCreatedPageView;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void Checked(object sender, EventArgs e)
        {
            //viewModel._navigationService.NavigateTo(App.SFLoginPageView);
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            // viewModel._navigationService.NavigateTo(App.SFAnonymousLandingPageView);
        }
    }
}