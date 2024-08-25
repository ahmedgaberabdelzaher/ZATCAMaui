using Newtonsoft.Json;
using System.Globalization;
using Page = Microsoft.Maui.Controls.Page;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using Mopups.Services;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationSuccessfullPageView : ContentPage
    {
        VATRegistrationSuccessfullPageViewModel viewModel;
        public VATRegistrationSuccessfullPageView(VATRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfullPageView;
            BindingContext = viewModel;
            if (response != null)
            {
                if (response.d != null)
                {
                    Label_Name.Text = response.d.TinNm;
                    Label_ApplicationNumber.Text = response.d.Fbnumz;
                    Label_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

        }
        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg1);
            }
            viewModel._navigationService.GoBack();
        }

        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(Label_ApplicationNumber.Text);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                var displayText = AppResources.VATRSAppNumber + " " + text;
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = displayText;
              
                headerWithInfos.Add(headerAmountInfo);
                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.Copied;
                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }

        }
    }
}