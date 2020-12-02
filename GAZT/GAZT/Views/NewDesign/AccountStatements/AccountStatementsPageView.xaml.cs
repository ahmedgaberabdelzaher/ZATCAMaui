using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.AccountStatements;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
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

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //LvwContacts.On<iOS>().SetGroupHeaderStyle(GroupHeaderStyle.Grouped);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            //SetPickerFont();
            ChangeArrowDirection();
            Task.Run(async() =>
            {
                try
                {
                   await viewModel.PopulateReturnTypeList();
                   await viewModel.PopulateASFilterData();
                   viewModel.PopulateFiltersData();
                }
                catch (Exception ex)
                {

                }
            });
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


        /*public void SetPickerFont()
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

        }*/
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                LvwContacts.FlowDirection = FlowDirection.LeftToRight;
                (LvwContacts.Header as StackLayout).FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                LvwContacts.FlowDirection = FlowDirection.RightToLeft;
                LvwContacts.FlowDirection = FlowDirection.RightToLeft;
                (LvwContacts.Header as StackLayout).FlowDirection = FlowDirection.RightToLeft;
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
            //TaxTypePicker.IsOpen = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Device.BeginInvokeOnMainThread(() => SetLTR());

        }

        //private void TaxTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        ASReturnTypes selectedReturntype = (ASReturnTypes)e.NewValue;
        //        //  TaxTypePicker.SelectedItem = selectedReturntype;
        //        viewModel.SelectedTaxTypeForFilter = selectedReturntype;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void TransactionTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        TaxRelationSetResult selectedReturntype = (TaxRelationSetResult)e.NewValue;
        //        TransactionTypePicker.SelectedItem = selectedReturntype;
        //        viewModel.SelectedTransactionTypeFilter = selectedReturntype;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        void searchButtonTapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = false;
            viewModel.IsCloseButtonVisible = true;
        }

        void filterButtonTapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.FiltersClicked();
            viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results);
            viewModel.IsVisible_SearchList = false;
        }

        void CloseSearchButton_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            //dummySearchList.IsVisible = false;
            viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results);
            viewModel.IsVisible_SearchList = false;
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            viewModel.IsSortByVisible = false;
            var keyword = e.NewTextValue;
            if (keyword.Length >= 1)
            {
                try
                {
                    var suggestion = viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.Where(c => c.Desc.ToLower().Contains(keyword.ToLower()) || c.PeriodTxt.ToLower().Contains(keyword.ToLower())
                    || c.FormattedBldat2.ToLower().Contains(keyword.ToLower()) || c.FormattedBldat.ToLower().Contains(keyword.ToLower())).ToList();
                    // viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(suggestion);
                    //viewModel.SearchBarListItemSource = viewModel.StatementsLineItems
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(suggestion);
                    //viewModel.IsVisible_SearchList = true;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(suggestion);

                }
                catch (Exception ex)
                {
                    //  viewModel.IsVisible_SearchList = false;
                }
            }
            else
            {
                viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results);
                //viewModel.IsVisible_SearchList = false;
            }
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
                viewModel.IsOpeningBalanceVisible = true;
                viewModel.PopulateStatements(viewModel.SelectedTransactionTypeFilter.TaxType, viewModel.SelectedTransactionTypeFilter.StatementFilter, viewModel.SelectedYear.Text);
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
                case "":
                    viewModel.TransactionDateFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Bldat).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Bldat).ToList());
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.TransactionDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Bldat).ToList());
                    viewModel.TransactionDateFilterItem = "descending";
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "descending":
                    // code block
                    viewModel.TransactionDateFilterItem = string.Empty;
                    //viewModel.IsVisible_SearchList = false;
                    //dummySearchList.IsVisible = false;

                    break;
                default:
                    viewModel.TransactionDateFilterItem = string.Empty;
                    //viewModel.IsVisible_SearchList = false;

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
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Persl).ToList());
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.TaxperiodFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Persl).ToList());
                    viewModel.TaxperiodFilterItem = "descending";
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "descending":
                    // code block
                    viewModel.TaxperiodFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;
                    break;
                default:
                    viewModel.TaxperiodFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;
                    break;
            }


        }

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
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Bldat2).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Bldat2).ToList());
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.DueDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Bldat2).ToList());
                    viewModel.DueDateFilterItem = "descending";
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "descending":
                    // code block
                    viewModel.DueDateFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;

                    break;
                default:
                    viewModel.DueDateFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;

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
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Desc).ToList());
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Desc).ToList());
                    viewModel.BillDescriptionFilterItem = "descending";
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "descending":
                    // code block
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;
                    break;
                default:
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;
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
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Betrh).ToList());
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "ascending":
                    // code blockDescending
                    viewModel.BillAmountFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Betrh).ToList());
                    viewModel.BillAmountFilterItem = "descending";
                    //viewModel.SearchBarListItemSource = new ObservableCollection<ASResult>(viewModel.StatementsLineItems);
                    //viewModel.IsVisible_SearchList = true;
                    //dummySearchList.IsVisible = true;
                    break;
                case "descending":
                    // code block
                    viewModel.BillAmountFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;

                    break;
                default:
                    viewModel.BillAmountFilterItem = string.Empty;
                    //dummySearchList.IsVisible = false;

                    break;
            }


        }        //        ChipGroup_statusFilter.SelectedItem = selectedTransactionType;

        void Button_BillStatusFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }        //        viewModel.SelectedTransactionType = selectedTransactionType;

        void Bills_ScrollToRequested(System.Object sender, Xamarin.Forms.ScrollToRequestEventArgs e)
        {

        }

        void LvwContacts_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            LvwContacts.SelectedItem = null;
        }        //        string taxType = string.Empty;
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
