using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.TaxpayersCertificatesPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayersCertificatesPageView : ContentPage
    {
        TaxpayersCertificatesPageViewModel viewModel;
        public TaxpayersCertificatesPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayersCertificatesPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.PopulateCirtificateTypeList();
            viewModel.IsLoading = true;
            viewModel.OnPageLoad();
            viewModel.IsLoading = false;

            List_Certificate.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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

                            TaxTypePicker.HeaderFontFamily = "Somar-SemiBold";
                            TaxTypePicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            TaxTypePicker.SelectedItemFontFamily = "Somar-SemiBold";
                            TaxTypePicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TaxTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void btn_Clicked(object sender, System.EventArgs e)
        {
            TaxTypePicker.IsOpen = true;
        }
       
        private void TaxTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                TaxTypePicker.SelectedItem = selectedReturntype;
                viewModel.SelectedTaxTypeForFilter = selectedReturntype;
           
            }
            catch (Exception ex)
            {

            }

        }
        //IsLoading = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                TaxTypePicker.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                TaxTypePicker.BackgroundColor =  (Color)Application.Current.Resources["White"];
            }
            viewModel.IsLoading  = false;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                this.FlowDirection = FlowDirection.RightToLeft;

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
            catch (Exception ex)
            {

            }
        }

    }
}