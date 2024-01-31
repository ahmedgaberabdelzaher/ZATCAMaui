

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.TaxpayersCertificatesPages
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayersCertificatesPageView : ContentPage
    {
        TaxpayersCertificatesPageViewModel viewModel;
        public TaxpayersCertificatesPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayersCertificatesPageView;
            BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            On<iOS>().SetUseSafeArea(true);
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.PopulateCirtificateTypeList();
            viewModel.IsLoading = true;
            viewModel.OnPageLoad();
            viewModel.IsLoading = false;

            List_Certificate.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {


                            TaxTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Device.Android:                                        // 
                        TaxTypePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TaxTypePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        private void btn_Clicked(object sender, EventArgs e)
        {
            TaxTypePicker.IsOpen = true;
        }

        private void TaxTypePicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                ReturnTypes selectedReturntype = viewModel.TaxTypeForFilter[e.NewValue];
                //ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                //TaxTypePicker.SelectedItem = selectedReturntype;
                viewModel.SelectedTaxTypeForFilter = selectedReturntype;

            }
            catch (Exception)
            {


            }

        }
        //IsLoading = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                TaxTypePicker.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                TaxTypePicker.BackgroundColor = (Color)Application.Current.Resources["White"];
            }
            viewModel.IsLoading = false;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                FlowDirection = FlowDirection.RightToLeft;

            }
        }
        public void ChangeAeroIcon()
        {
            try
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
            catch (Exception)
            {


            }
        }

    }
}