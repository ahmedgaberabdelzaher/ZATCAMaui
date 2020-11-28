using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using System.Linq;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;



namespace EGAZT.Views.NewDesign.VATLookUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
        bool isMandatoryDataEntered = true;
        VATLookUpNewPageViewModel viewModel;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            this.BindingContext = viewModel;

            SetPickerFont();
            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.ResetFormData();
            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;

            ZXingScannerPage scanPage;
            btnScan.Clicked += async (a,e) =>
            {
                viewModel.SelectedParameterType = viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () => {
                        MessagingCenter.Send(this, "ScanData", result.Text);
                        await Navigation.PopAsync();
                    });
                };
                await Navigation.PushAsync(scanPage);
            };
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            PPicker.HeaderFontFamily = "SSTArabic-Medium";
                            PPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            PPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            PPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        PPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        PPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        PPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        PPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                PPicker.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                PPicker.BackgroundColor = Color.FromHex("#FFFFFF");
            }
            
                        MessagingCenter.Send(this, "ScanData", "abc");
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

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        
        void PPicker_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            //if (viewModel.IsNameVisible)
            //    return;
            viewModel.IsNameVisible = false;
            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
                //PopUp popUp = new PopUp();//SetPlaceholderText();
                //popUp.Message = viewModel.VATACCOrCRNOOrVATCER;
                //if (App.IsArabic)
                //{
                //    popUp.FlowDirections = "RightToLeft";
                //}
                //else
                //{
                //    popUp.FlowDirections = "LeftToRight";
                //}
                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(viewModel.VATACCOrCRNOOrVATCER));

            }
        }

        void PPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
        }

        private void BorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
          viewModel.IsNameVisible = false);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.onDissapear();
        }
    }
}
