using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.MyBillsPages;

namespace ZATCAMAUI.Views.NewDesign.GenericPickers
{
    public partial class NewPopupPageView : PopupPage
    {
        public ReturnTypes sSelectedTaxTypeForFilter { get; }

        public NewPopupPageView(List<ReturnTypes> taxTypeForFilter, ReturnTypes selectedTaxTypeForFilter)
        {
            InitializeComponent();
            //SetPickerFont();
            CloseWhenBackgroundIsClicked = false;
            TaxTypePicker.ItemsSource = taxTypeForFilter;
            sSelectedTaxTypeForFilter = selectedTaxTypeForFilter;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var items = TaxTypePicker.ItemsSource as List<ReturnTypes>;
                var selectedItem = items.FirstOrDefault(x => x.Id == sSelectedTaxTypeForFilter.Id && x.TaxType == sSelectedTaxTypeForFilter.TaxType);
                TaxTypePicker.SelectedItem = selectedItem;
            }
            catch (Exception)
            {


            }
            await picker.TranslateTo(0, 500, 0);
            await picker.TranslateTo(0, 0, 250);
        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    TaxTypePicker.SelectedIndex = null;
        //    TaxTypePicker.ItemsSource = null;
        //}

        private async void Dissapear()
        {
            await picker.TranslateTo(0, 500, 250);
            await PopupNavigation.Instance.PopAsync(false);
            MessagingCenter.Send(new GAZTNewDesignMyBillsPageView(), "pickerNew", (ReturnTypes)TaxTypePicker.SelectedItem);
        }

        private async void PopupClose_Clicked(object sender, EventArgs e)
        {

            await picker.TranslateTo(0, 500, 250);
            await PopupNavigation.Instance.PopAsync(false);
            MessagingCenter.Send(new GAZTNewDesignMyBillsPageView(), "pickerNew", (ReturnTypes)TaxTypePicker.SelectedItem);
        }


        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {

                            TaxTypePicker.HeaderFontFamily = "Somar-SemiBold";
                            TaxTypePicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            TaxTypePicker.SelectedItemFontFamily = "Somar-SemiBold";
                            TaxTypePicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Device.Android:

                        TaxTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TaxTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception)
            {


            }

        }

        private void TaxTypePicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                TaxTypePicker.SelectedItem = selectedReturntype;
            }
            catch (Exception)
            {


            }

        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Dissapear();
        }

        void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Dissapear();
        }
    }
}
