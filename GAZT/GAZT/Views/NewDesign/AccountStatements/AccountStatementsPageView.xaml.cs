using System;
using System.Collections.Generic;
using EGAZT.Models.AccountStatements;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    public partial class AccountStatementsPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;

        public AccountStatementsPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementsPageView;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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
            catch (Exception ex)
            {

            }

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
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void btn_Clicked(object sender, System.EventArgs e)
        {
            TaxTypePicker.IsOpen = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.PopulateReturnTypeList();
            viewModel.PopulateASFilterData();
            viewModel.PopulateFiltersData();
        }

        private void TaxTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ASReturnTypes selectedReturntype = (ASReturnTypes)e.NewValue;
                TaxTypePicker.SelectedItem = selectedReturntype;
                viewModel.SelectedTaxTypeForFilter = selectedReturntype;
            }
            catch (Exception ex)
            {

            }
        }

        private async void TransactionTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ASRevenueDropDownSetDataResults selectedReturntype = (ASRevenueDropDownSetDataResults)e.NewValue;
                TransactionTypePicker.SelectedItem = selectedReturntype;
                viewModel.SelectedTransactionTypeFilter = selectedReturntype;

                if(viewModel.SelectedTransactionTypeFilter.StatementFilter != null)
                {
                    await viewModel.PopulateDataInChipsForYears(viewModel.SelectedTaxTypeForFilter.Id, viewModel.SelectedTransactionTypeFilter.StatementFilter);
                }
            }
            catch (Exception ex)
            {

            }
        }

        void searchButtonTapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = false;
            viewModel.IsCloseButtonVisible = true;
        }

        void filterButtonTapped(System.Object sender, System.EventArgs e)
        {
            viewModel.FiltersClicked();
        }

        void CloseSearchButton_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }

        void btnTransactionTypePicker_Clicked(System.Object sender, System.EventArgs e)
        {
            TransactionTypePicker.IsOpen = true;
        }

        private void yearChipGroup_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                ASChipModel selectedReturntype = (ASChipModel)e.AddedItem;
                ChipGroup_Years.SelectedItem = selectedReturntype;
                viewModel.SelectedYear = selectedReturntype;
                viewModel.PopulateStatements(viewModel.SelectedTaxTypeForFilter.Id, viewModel.SelectedTransactionTypeFilter.StatementFilter, viewModel.SelectedYear.Text);
            }
            catch (Exception ex)
            {

            }
        }

        //async void ChipGroup_statusFilter_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        ASChipModel selectedTransactionType = (ASChipModel)e.AddedItem;
        //        ChipGroup_statusFilter.SelectedItem = selectedTransactionType;
        //        viewModel.SelectedTransactionType = selectedTransactionType;

        //        string taxType = string.Empty;
        //        if (viewModel.SelectedTaxTypeForFilter.Id == "00")
        //        {
        //            taxType = "D";
        //        }
        //        else
        //        {
        //            taxType = "I";
        //        }

        //        //await viewModel.PopulateDataInChipsForYears(taxType, selectedTransactionType.StatementFilter);

        //        //viewModel.SelectionColor = Color.AliceBlue;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
