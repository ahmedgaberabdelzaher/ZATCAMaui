using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{
   
    public partial class TaxEvasionMyReportsListPageView : ContentPage
    {
        TaxEvasionMyReportsListPageViewModel viewModel;
        public TaxEvasionMyReportsListPageView(string mobno)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionMyReportsListPageView;
            BindingContext = viewModel;

            viewModel.PopulateDataInChips();
            viewModel.SelectedChipFilterItemList = new List<ChipModel>();

            On<iOS>().SetUseSafeArea(true);
            SetLTR();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                ChangeAeroIcon();

                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });

                await viewModel.OnPageLoad();
                viewModel.FilterOnbasisOfChipSelectedItem();

                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
          
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                FlowDirection = FlowDirection.RightToLeft;
            }
        }

        private async void ChipGroup_statusFilter_SelectionChanging(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                ChipModel addeditemtype = (ChipModel)e.AddedItem;
                ChipModel removedItem = (ChipModel)e.RemovedItem;

                if (addeditemtype != null)
                {
                    viewModel.SelectedChipFilterItemList.Add(addeditemtype);
                }
                if (removedItem != null)
                {
                    viewModel.SelectedChipFilterItemList.Remove(removedItem);
                }
                viewModel.FilterOnbasisOfChipSelectedItem();
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }

        private void ListView_TaxEvasionReportList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception)
            {


            }
            return;
        }
        protected override bool OnBackButtonPressed() => true;
        private async void AddReport_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.NewTaxEvasionFormPageView);

            });

        }
    }
}
