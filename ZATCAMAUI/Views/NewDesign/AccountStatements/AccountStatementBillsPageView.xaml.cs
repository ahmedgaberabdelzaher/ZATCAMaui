using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountStatementBillsPageView : ContentPage
    {
        AccountStatementBillsPageViewModel viewModel;
        public AccountStatementBillsPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementBillsPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.GetDashBoardMenuLst(2);
            ChangeAeroIcon();
            ChangeArrowDirection();


            Task.Run(async () =>
            {


                try
                {
                    BillInfo billInfo = new BillInfo();
                    viewModel.onPageLoad(billInfo);
                    if (viewModel.MyBillsOriginal != null)
                    {
                        viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal.Where(x => x.Status != "P"));
                    }



                }
                catch (Exception)
                {
                }

                try
                {
                    await viewModel.PopulateReturnTypeList();
                    await viewModel.PopulateASFilterData();
                    viewModel.populateStatusChips();


                }
                catch (Exception)
                {
                }
            });




        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["Back"];
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
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["Back"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
            });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

        }
        void searchButtonTapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = false;
            viewModel.IsCloseButtonVisible = true;
        }

        void filterButtonTapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.FiltersClicked();
            if (viewModel.MyBillsOriginal != null)
            {
                viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal);
            }
            /*viewModel.IsVisible_SearchList = false;*/
        }

        void CloseSearchButton_Tapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.SearchText = "";
            viewModel.FilterIfTypeAndStausFilterSelected(false);
        }

        private void btn_Clicked(object sender, EventArgs e)
        {


            viewModel.showPickerDialog();

           
        }

        private void ChipsData_Tapped(object sender, EventArgs e)
        {
            Grid chipGrid = sender as Grid;
            ChipModel chipModel = (ChipModel)chipGrid.BindingContext;
            if (chipModel != null)
            {
                if (viewModel.FromStatus == chipModel.Text)
                {
                    viewModel.FromStatus = "";

                }
                else
                {
                    viewModel.FromStatus = chipModel.Text;
                }


                viewModel.ApplyFilter();
            }

        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            if (keyword.Length >= 1)
            {
                try
                {
                    viewModel.SearchText = keyword;
                    viewModel.FilterIfTypeAndStausFilterSelected(false);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                viewModel.FilterIfTypeAndStausFilterSelected(false);
            }
        }

        async void LVNormalStatements_ItemTapped(object sender,ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as MyBills;
                await Application.Current.MainPage.Navigation.PushAsync(new AccountStatementsDetailPageView(item));

                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            }
            catch (Exception)
            {
            }

        }
    }
}