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
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATAmendReactivationPages
{
    [Preserve(AllMembers = true)]
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
            this.Padding = safeInsets;
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
            int dashboard = 0, stackCount= Navigation.NavigationStack.Count;
            
            for (int i = 0; i < stackCount; i++)
            {
                if (Navigation.NavigationStack[i].GetType().Name.Equals("GAZTNewDesignDashBoardPageView"))
                {
                    dashboard = i;
                    break;
                }
            }
            for(int i= stackCount-2; i > dashboard; i--)
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
                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }

        }
        void SfButton_Clicked(System.Object sender, System.EventArgs e)
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