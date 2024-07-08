using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

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
            SetPickerFont();
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

    }
}