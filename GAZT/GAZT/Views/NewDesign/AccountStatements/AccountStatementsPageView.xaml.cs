using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            ChangeArrowDirection();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        public void ChangeArrowDirection()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
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

            try
            {
                App.DisplayProgressView();
                viewModel.PopulateReturnTypeList();
                viewModel.PopulateASFilterData();
                viewModel.PopulateFiltersData();
                App.HideProgressView();
            }
            catch(Exception ex)
            {
                App.HideProgressView();
            }
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

        void Button_TransactionTypeFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            //viewModel.TransactionDateFilterItem = string.Empty;
            viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
            viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
             viewModel.TaxperiodFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;
            switch (viewModel.TransactionDateFilterItem)
            {
                case  "":
                    viewModel.TransactionDateFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Bldat).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderBy(x => x.Bldat).ToList());
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.TransactionDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderByDescending(x => x.Bldat).ToList());
                    viewModel.TransactionDateFilterItem = "descending";
                    break;
                case "descending":
                    // code block
                    viewModel.TransactionDateFilterItem = string.Empty;
                    break;
                default:
                    viewModel.TransactionDateFilterItem = string.Empty;
                    break;
            }
        }

        void Button_TaxTypeFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Button_FBNumFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Button_SadadBillNumFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }        //async void ChipGroup_statusFilter_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)

        void Button_TaxPeriodFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
            viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
              viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
           // viewModel.TaxperiodFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;

            switch (viewModel.TaxperiodFilterItem)
            {
                case "":
                    viewModel.TaxperiodFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Persl).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderBy(x => x.Persl).ToList());
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.TaxperiodFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderByDescending(x => x.Persl).ToList());
                    viewModel.TaxperiodFilterItem = "descending";
                    break;
                case "descending":
                    // code block
                    viewModel.TaxperiodFilterItem = string.Empty;
                    break;
                default:
                    viewModel.TaxperiodFilterItem = string.Empty;
                    break;
            }
        }        //{

        void Button_DueDateFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
             viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
          //  viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
            viewModel.TaxperiodFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;

            switch (viewModel.DueDateFilterItem)
            {
                case "":
                    viewModel.DueDateFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.PeriodEndDt).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderBy(x => x.PeriodEndDt).ToList());
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.DueDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderByDescending(x => x.PeriodEndDt).ToList());
                    viewModel.DueDateFilterItem = "descending";
                    break;
                case "descending":
                    // code block
                    viewModel.DueDateFilterItem = string.Empty;
                    break;
                default:
                    viewModel.DueDateFilterItem = string.Empty;
                    break;
            }
        }        //    try

        void Button_BillDiscriptionFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
              viewModel.BillAmountFilterItem = string.Empty;
            //viewModel.BillDescriptionFilterItem = string.Empty;
            viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
            viewModel.TaxperiodFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;
            switch (viewModel.BillDescriptionFilterItem)
            {
                case "":
                    viewModel.BillDescriptionFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Desc).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderBy(x => x.Desc).ToList());
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderByDescending(x => x.Desc).ToList());
                    viewModel.BillDescriptionFilterItem = "descending";
                    break;
                case "descending":
                    // code block
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    break;
                default:
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    break;
            }
        }        //    {

        void Button_BillDiscriptionFilter_Clicked_1(System.Object sender, System.EventArgs e)
        {
        }        //        ASChipModel selectedTransactionType = (ASChipModel)e.AddedItem;

        void Button_BillAmountFilter_Clicked(System.Object sender, System.EventArgs e)
        {
           viewModel.TransactionDateFilterItem = string.Empty;
         //  viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
            viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
            viewModel.TaxperiodFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;
            switch (viewModel.BillAmountFilterItem)
            {
                case "":
                    viewModel.BillAmountFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Betrh).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderBy(x => x.Betrh).ToList());
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.BillAmountFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.StatementsLineItems.OrderByDescending(x => x.Betrh).ToList());
                    viewModel.BillAmountFilterItem = "descending";
                    break;
                case "descending":
                    // code block
                    viewModel.BillAmountFilterItem = string.Empty;
                    break;
                default:
                    viewModel.BillAmountFilterItem = string.Empty;
                    break;
            }
        }        //        ChipGroup_statusFilter.SelectedItem = selectedTransactionType;

        void Button_BillStatusFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }        //        viewModel.SelectedTransactionType = selectedTransactionType;

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
