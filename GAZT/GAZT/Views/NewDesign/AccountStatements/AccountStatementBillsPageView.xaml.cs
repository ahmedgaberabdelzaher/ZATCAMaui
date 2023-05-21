using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.MyBillsPages;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class AccountStatementBillsPageView : ContentPage
    {
        AccountStatementBillsPageViewModel viewModel;
        public AccountStatementBillsPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementBillsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.GetDashBoardMenuLst(2);
            ChangeAeroIcon();
            SetLTR();
            ChangeArrowDirection();


            Task.Run(async () =>
            {


                try
                {
                    BillInfo billInfo = new BillInfo();
                    viewModel.onPageLoad(billInfo);
                    //viewModel.PopulateReturnTypeList();

                    //                viewModel.PopulateDataInChips();
                    //viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);
                    if (viewModel.MyBillsOriginal != null)
                    {
                        viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal.Where(x => x.Status != "P"));
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    await viewModel.PopulateReturnTypeList();
                    await viewModel.PopulateASFilterData();
                    viewModel.populateStatusChips();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());


                }
            });

            


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
            if(viewModel.MyBillsOriginal != null)
            {
                viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);
            }
            /*viewModel.IsVisible_SearchList = false;*/
        }

        void CloseSearchButton_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.SearchText = "";
            /*viewModel.StatementsLineItems = new ObservableCollection<ASResult>(viewModel.HeaderSet.D.StatmenetLineItemsSet.Results);
            viewModel.IsVisible_SearchList = false;*/
            viewModel.FilterIfTypeAndStausFilterSelected(false);
        }

        private void btn_Clicked(object sender, System.EventArgs e)
        {


            viewModel.showPickerDialog();

            //MessagingCenter.Subscribe<GAZTNewDesignMyBillsPageView, ReturnTypes>(this, "pickerNew", (a, arg) =>
            //{
            //    viewModel.SelectedTaxTypeForFilter = arg;
            //    MessagingCenter.Unsubscribe<GAZTNewDesignMyBillsPageView, ReturnTypes>(this, "pickerNew");
            //});
            //PopupNavigation.Instance.PushAsync(new NewPopupPageView(viewModel.TaxTypeForFilter, viewModel.SelectedTaxTypeForFilter), false);
        }

        private void ChipsData_Tapped(object sender, EventArgs e)
        {
            Grid chipGrid = sender as Grid;
            ChipModel chipModel = (ChipModel)chipGrid.BindingContext;
            if (chipModel != null)
            {
                if(viewModel.FromStatus == chipModel.Text) {
                    viewModel.FromStatus = "";

                }
                else {
                    viewModel.FromStatus = chipModel.Text;
                }

                
                viewModel.ApplyFilter();
            }

        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            if (keyword.Length >= 1)
            {
                try
                {
                    viewModel.SearchText = keyword;
                    viewModel.FilterIfTypeAndStausFilterSelected(false);
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
                viewModel.FilterIfTypeAndStausFilterSelected(false);
            }
        }

        async void LVNormalStatements_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try { 
            var item = e.Item as MyBills;
            await Application.Current.MainPage.Navigation.PushAsync(new AccountStatementsDetailPageView(item));

            if (e.Item == null) return;
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

        }
    }
}