using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Views.NewDesign.MyBillsPages;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.GenericPickers
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

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            TaxTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                            TaxTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            TaxTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            TaxTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TaxTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        TaxTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception)
            {

            }

        }

        private void TaxTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
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

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Dissapear();
        }

        void SwipeGestureRecognizer_Swiped(System.Object sender, Xamarin.Forms.SwipedEventArgs e)
        {
            Dissapear();
        }
    }
}
