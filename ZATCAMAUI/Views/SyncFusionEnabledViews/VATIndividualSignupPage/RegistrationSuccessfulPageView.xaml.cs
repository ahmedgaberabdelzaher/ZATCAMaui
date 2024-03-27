using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessfulPageView : ContentPage
    {
        RegistrationSuccessfulPageViewModel viewModel;
        public RegistrationSuccessfulPageView(string TIN)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfulPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.TINnumber = TIN;
            //App.IsArabic = false;
            if (App.successMsg == true)
            {
                viewModel.IsGulf = true;
                viewModel.IsCitizen = false;
            }
            else
            {
                viewModel.IsCitizen = true;
                viewModel.IsGulf = false;
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
                copyLabel.IsVisible = true;
                await Task.Delay(2000); // Delay for 2 seconds
                copyLabel.IsVisible = false;
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = displayText;
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }
                headerWithInfos.Add(headerAmountInfo);
                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.Copied;
                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
        }
        protected override bool OnBackButtonPressed() => true;

    }
}