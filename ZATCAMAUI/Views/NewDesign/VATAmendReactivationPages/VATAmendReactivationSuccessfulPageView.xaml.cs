using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

namespace ZATCAMAUI.Views.NewDesign.VATAmendReactivationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATAmendReactivationSuccessfulPageView : ContentPage
    {
        VATAmendReactivationSuccesssulPageViewModel viewModel;
        public VATAmendReactivationSuccessfulPageView(Models.VATRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATAmendReactivationSuccesssulPageView;
            BindingContext = viewModel;
            On<iOS>().SetUseSafeArea(true);
            if (response != null)
            {
                if (response.d != null)
                {
                    Label_Name.Text = response.d.TinNm;
                    Label_ApplicationNumber.Text = response.d.Fbnumz;
                    // string StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + response.d.VatTaxDt + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    viewModel.FBNumber = response.d.Fbnumz;
                    string StartdateToshow = DateTime.Today.Date.ToString("dd/MM/yyyy").Replace('-', '/');
                    Label_Date.Text = StartdateToshow;
                    //Label_Date.Text = response.d.GoLiveDt;
                }
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }
        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            int dashboard = 0, stackCount = Navigation.NavigationStack.Count;

            for (int i = 0; i < stackCount; i++)
            {
                if (Navigation.NavigationStack[i].GetType().Name.Equals("GAZTNewDesignDashBoardPageView"))
                {
                    dashboard = i;
                    break;
                }
            }
            for (int i = stackCount - 2; i > dashboard; i--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);
            }

            viewModel._navigationService.GoBack();
        }

        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(Label_ApplicationNumber.Text);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                var displayText = AppResources.VATRSAppNumber + " " + text;
                //  viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
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
                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }

        }
        void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception)
            {

            }
        }
    }
}