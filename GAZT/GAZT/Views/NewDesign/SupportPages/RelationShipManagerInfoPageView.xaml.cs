using EGAZT.ViewModel.NewDesignViewModel.SupportPageVM;
using GAZT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.SupportPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelationShipManagerInfoPageView : ContentPage
    {
        RelationShipManagerInfoPageViewModel viewModel;
        public RelationShipManagerInfoPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.RelationShipManagerInfoPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;

            ChangeAeroIcon();
            SetLTR();
            //  SetLanguage();
            //loadingIndicator.IsVisible = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            await viewModel.GetRmContactDetailsOnPageLoad();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void TOnBackButtonClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        public async void submitComplaintToRelationShipManager(object sender, EventArgs e)
        {
            try {
                await Task.Run(() => { Device.OpenUri(new Uri(Constants.ComplaintsUrl)); });
            }
            catch(Exception)
            {

            }
        }
    }
}