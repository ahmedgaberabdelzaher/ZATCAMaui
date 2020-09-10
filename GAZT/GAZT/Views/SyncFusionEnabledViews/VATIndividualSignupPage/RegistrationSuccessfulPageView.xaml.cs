using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessfulPageView : ContentPage
    {
        RegistrationSuccessfulPageViewModel viewModel;
        public RegistrationSuccessfulPageView(string TIN)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfulPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.TINnumber = TIN;
            //App.IsArabic = false;
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
            
        }

        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(Label_Tin.Text);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                var displayText = AppResources.TINS + " " + text;
                viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
            }
        }
        protected override bool OnBackButtonPressed() => true;

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            try
            {
                // viewModel._navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                //  viewModel._navigationService.NavigateTo(App.SFLoginPageView);

                if (Navigation.NavigationStack.Count > 0)
                {
                    Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 3];
                    Navigation.RemovePage(pg);
                    Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(pg1);
                }
                viewModel._navigationService.GoBack();

                //    viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            }
            catch(Exception ex)
            {

            }

        }


    }
}