using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATAmendReactivationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATAmendReactivationSuccessfulPageView : ContentPage
    {
        VATAmendReactivationSuccesssulPageViewModel viewModel;
        public VATAmendReactivationSuccessfulPageView(Models.VATRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATAmendReactivationSuccesssulPageView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            if (response != null)
            {
                if (response.d != null)
                {
                    Label_Name.Text = response.d.TinNm;
                    Label_ApplicationNumber.Text = response.d.Fbnumz;
                    string StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + response.d.VatTaxDt + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    viewModel.FBNumber = response.d.Fbnumz;
                    //Label_Date.Text = response.d.GoLiveDt;
                    Label_Date.Text = StartdateToshow;
                }
            }

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            // Added by Divya
            //  viewModel._navigationService.NavigateTo(App.SFLandingPageView);
            //App.TP = null;
            //viewModel.LogOut();
            if (Navigation.NavigationStack.Count > 0)
            {
                //Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 3];
                //Navigation.RemovePage(pg);
                Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
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
                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }

        }
        void SfButton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {

                viewModel.downloadConfirmation();
            }
            catch (Exception ex)
            {

            }
        }
    }
}