using System.Collections.ObjectModel;
using Mopups.Animations;
using Mopups.Enums;
using Mopups.Services;
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
            BindingContext = viewModel;
            viewModel.GetDashBoardMenuLst(2);


            Task.Run(async () =>
            {
                try
                {
                    viewModel.IsLoading = true;
                    BillInfo billInfo = new BillInfo();
                    await viewModel.onPageLoad(billInfo);
                    if (viewModel.MyBillsOriginal != null)
                    {
                        viewModel.MyBills = new ObservableCollection<MyBills>(viewModel.MyBillsOriginal.Where(x => x.Status != "P"));
                    }
                    await viewModel.PopulateReturnTypeList();
                    await viewModel.PopulateASFilterData();
                    viewModel.populateStatusChips();
                    viewModel.IsLoading = false;

                }
                catch (Exception)
                {
                }
            });




        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
            });

            MessagingCenter.Subscribe<object, string>(this, "SortingTappedforAcc", (sender, arg) =>
            {
                viewModel.ClickSorted(arg);
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
        }
        async void ClickOnSorting(System.Object sender, System.EventArgs e)
        {
            var pr = new SortingPopupPage();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };

            pr.Animation = scaleAnimation;
            await MopupService.Instance.PushAsync(pr);
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
                if (viewModel.FromStatus == chipModel.ZTSTScts)
                {
                    viewModel.FromStatus = "";

                }
                else
                {
                    viewModel.FromStatus = chipModel.ZTSTScts;
                }


                viewModel.ApplyFilter();
            }

        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var keyword = e.NewTextValue;
                if (keyword.Length >= 1)
                {
                    viewModel.SearchText = keyword;
                    viewModel.FilterIfTypeAndStausFilterSelected(false);
                }
                else
                {
                    viewModel.FilterIfTypeAndStausFilterSelected(false);
                }
            }
            catch (Exception)
            {
            }

        }

        async void LVNormalStatements_ItemTapped(System.Object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as MyBills;
                viewModel.accoungtDetails1 = await viewModel.ObjectBills(item.Opbel, item.Fbnum);

                if (viewModel.accoungtDetails1 == null)
                {
                    viewModel.accoungtDetails1 = new Models.AccountDetails.AccoungtDetails();
                }


                await Application.Current.MainPage.Navigation.PushAsync(new AccountStatementsDetailPageView(item, viewModel.accoungtDetails1));

                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            }
            catch (Exception)
            {
            }

        }
    }
}