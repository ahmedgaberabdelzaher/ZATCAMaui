using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.MyBillsPages;

namespace ZATCAMAUI.Views.NewDesign.GenericPickers
{
    public partial class NewPopupPageView : PopupPage
    {
        public ReturnTypes sSelectedTaxTypeForFilter { get; }
        public ReturnTypes TaxTypePickerSelectedItem { get; set; }
        public List<ReturnTypes> taxTypeForFilter { get; set; }
        public NewPopupPageView(List<ReturnTypes> taxTypeForFilter, ReturnTypes selectedTaxTypeForFilter)
        {
            InitializeComponent();
            //SetPickerFont();
            CloseWhenBackgroundIsClicked = false;
            TaxTypePicker.Columns[0].ItemsSource = taxTypeForFilter;
            this.taxTypeForFilter = taxTypeForFilter;
            sSelectedTaxTypeForFilter = selectedTaxTypeForFilter;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var items = TaxTypePicker.Columns[0].ItemsSource as List<ReturnTypes>;
                var selectedItem = items.FirstOrDefault(x => x.Id == sSelectedTaxTypeForFilter.Id && x.TaxType == sSelectedTaxTypeForFilter.TaxType);
                TaxTypePickerSelectedItem = selectedItem;
                //TaxTypePicker.SelectedItem = selectedItem;
            }
            catch (Exception)
            {


            }
            await picker.TranslateTo(0, 500, 0);
            await picker.TranslateTo(0, 0, 250);
        }

        private async void Dissapear()
        {
            await picker.TranslateTo(0, 500, 250);
            await MopupService.Instance.PopAsync(false);
            MessagingCenter.Send(new GAZTNewDesignMyBillsPageView(), "pickerNew", TaxTypePickerSelectedItem);
        }

        private async void PopupClose_Clicked(object sender, EventArgs e)
        {

            await picker.TranslateTo(0, 500, 250);
            await MopupService.Instance.PopAsync(false);
            MessagingCenter.Send(new GAZTNewDesignMyBillsPageView(), "pickerNew", TaxTypePickerSelectedItem);
        }


        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            TaxTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            TaxTypePicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        {
                            TaxTypePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            TaxTypePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            TaxTypePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            TaxTypePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
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
                ReturnTypes selectedReturntype = this.taxTypeForFilter[e.NewValue];
                //TaxTypePicker.SelectedItem = selectedReturntype;
                TaxTypePickerSelectedItem = selectedReturntype;
            }
            catch (Exception)
            {


            }

        }


        void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Dissapear();
        }
    }
}
