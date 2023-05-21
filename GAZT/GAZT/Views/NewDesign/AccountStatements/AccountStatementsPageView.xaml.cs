using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static EGAZT.ViewModel.NewDesignViewModel.AccountStatements.AccountStatementsPageViewModel;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [Preserve(AllMembers = true)]
    public partial class AccountStatementsPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;
        public AccountStatementsPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            ChangeArrowDirection();
            Task.Run(async () =>
            {
                try
                {
                    await viewModel.PopulateReturnTypeList();
                    await viewModel.PopulateASFilterData();
                    viewModel.PopulateFiltersData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;


            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();

             
               
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

        }

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
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(suggestion);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            }
            else
            {
                viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results);
            }
        }

        void btnTransactionTypePicker_Clicked(System.Object sender, System.EventArgs e)
        {
            //TransactionTypePicker.IsOpen = true;
            viewModel.showPickerDialog();
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
                Console.WriteLine(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        void Button_TransactionTypeFilter_Clicked(System.Object sender, System.EventArgs e)
        {
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
                    break;
                case "ascending":
                    viewModel.TransactionDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Bldat).ToList());
                    viewModel.TransactionDateFilterItem = "descending";
                    break;
                case "descending":
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
        }

        void Button_TaxPeriodFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
            viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
            viewModel.DueDateFilterItem = string.Empty;
            viewModel.FBNumFilterItem = string.Empty;
            viewModel.SadadBillNumberFilterItem = string.Empty;
            viewModel.StatusFilterItem = string.Empty;
            viewModel.TaxTypeFilterItem = string.Empty;

            switch (viewModel.TaxperiodFilterItem)
            {
                case "":
                    viewModel.TaxperiodFilterItem = "ascending";
                    var ListSorted = viewModel.StatementsLineItems.OrderBy(x => x.Persl).ToList();
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderBy(x => x.Persl).ToList());
                    break;
                case "ascending":
                    viewModel.TaxperiodFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Persl).ToList());
                    viewModel.TaxperiodFilterItem = "descending";
                    break;
                case "descending":
                    viewModel.TaxperiodFilterItem = string.Empty;
                    break;
                default:
                    viewModel.TaxperiodFilterItem = string.Empty;
                    break;
            }
        }

        void Button_DueDateFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
            viewModel.BillAmountFilterItem = string.Empty;
            viewModel.BillDescriptionFilterItem = string.Empty;
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
                    break;
                case "ascending":
                    viewModel.DueDateFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Bldat2).ToList());
                    viewModel.DueDateFilterItem = "descending";
                    break;
                case "descending":
                    viewModel.DueDateFilterItem = string.Empty;
                    break;
                default:
                    viewModel.DueDateFilterItem = string.Empty;
                    break;
            }
        }

        void Button_BillDiscriptionFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
            viewModel.BillAmountFilterItem = string.Empty;
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
                    break;
                case "ascending":
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Desc).ToList());
                    viewModel.BillDescriptionFilterItem = "descending";
                    break;
                case "descending":
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    break;
                default:
                    viewModel.BillDescriptionFilterItem = string.Empty;
                    break;
            }
        }

        void Button_BillDiscriptionFilter_Clicked_1(System.Object sender, System.EventArgs e)
        {
        }

        void Button_BillAmountFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.TransactionDateFilterItem = string.Empty;
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
                    break;
                case "ascending":
                    viewModel.BillAmountFilterItem = string.Empty;
                    viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results.OrderByDescending(x => x.Betrh).ToList());
                    viewModel.BillAmountFilterItem = "descending";
                    break;
                case "descending":
                    viewModel.BillAmountFilterItem = string.Empty;
                    break;
                default:
                    viewModel.BillAmountFilterItem = string.Empty;
                    break;
            }
        }

        void Button_BillStatusFilter_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Bills_ScrollToRequested(System.Object sender, Xamarin.Forms.ScrollToRequestEventArgs e)
        {

        }

        async void LvwContacts_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            /*var item = e.Item as ASResult;

            await Application.Current.MainPage.Navigation.PushAsync(new AccountStatementsDetailPageView(item));*/
            try { 
            if (e.Item == null) return;
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        async void LVNormalStatements_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            /*var item = e.Item as ASResult;
            await Application.Current.MainPage.Navigation.PushAsync(new AccountStatementsDetailPageView(item));*/
            try { 
            if (e.Item == null) return;
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        /*     private void StatusList_Tapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
             {
                 var item = e.ItemData as ASResult;

                 if (item != null)
                 {
                     viewModel.FromStatus = item.StatusDesc;
                     viewModel.ApplyFilter();
                 }
             }*/

        private void ChipsData_Tapped(object sender, EventArgs e)
        {
            try { 
            Grid chipGrid = sender as Grid;
            ASResult chipModel = (ASResult)chipGrid.BindingContext;
            if (chipModel != null)
            {
                viewModel.FromStatus = chipModel.StatusDesc;
                viewModel.ApplyFilter();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

    }
}
