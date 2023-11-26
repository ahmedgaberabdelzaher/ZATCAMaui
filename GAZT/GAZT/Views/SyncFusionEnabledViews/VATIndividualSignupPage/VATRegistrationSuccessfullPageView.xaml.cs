using EGAZT.Models;
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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationSuccessfullPageView : ContentPage
    {
        VATRegistrationSuccessfullPageViewModel viewModel;
        public VATRegistrationSuccessfullPageView(VATRegistrationDetails response)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfullPageView;
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
                    //Label_Date.Text = response.d.GoLiveDt;
                 //   Label_Date.Text = StartdateToshow;
                    Label_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            if (Navigation.NavigationStack.Count > 0)
            {
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
    }
}