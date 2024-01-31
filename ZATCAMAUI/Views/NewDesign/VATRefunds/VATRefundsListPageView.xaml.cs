using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;
using SearchBar = Microsoft.Maui.Controls.SearchBar;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{
    
    public partial class VATRefundsListPageView : ContentPage
    {
        VATRefundListPageViewModel viewModel;
        SearchBar searchBar = null;

        public VATRefundsListPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsListPageView;
            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                viewModel.PopulateVATRefundsList();
                ChangeArrowDirection();
               MessagingCenter.Subscribe<object, string>(this, "InstructionsConfirmed", (message, arg) =>
                {
                    if (arg == "NavigateToNewRequestPageView")
                    {
                        viewModel._navigationService.NavigateTo(App.VATRefundsNewRequestPageView);
                    }
                });
            }
            catch (Exception)
            {

            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "InstructionsConfirmed");

        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
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
        }

        void vatRefundDetailsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                VatRefHeaderSetResult selectedItem = e.AddedItems[0] as VatRefHeaderSetResult;
                viewModel.SelectionChanged(selectedItem);

                if (selectedItem.Status == AppResources.VATRefundsStatusDraft)
                {
                    viewModel.VatRefundsListResultModel.IsEditable = true;
                    viewModel._navigationService.NavigateTo(App.VATRefundsNewRequestPageView, viewModel.VatRefundsListResultModel);
                }
                else
                {
                    viewModel.VatRefundsListResultModel.IsEditable = false;
                    Navigation.PushAsync(new VATRefundDetailsPageView(viewModel.VatRefundsListResultModel));
                }

                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception)
            {


            }
        }

        public async void NewRefundRequest_Tapped(object sender, EventArgs e)
        {
            try
            {
                //await viewModel.ReloadData();
                await PopupNavigation.Instance.PushAsync(new VATRefundsInstructionsPageView());

            }
            catch (Exception)
            {


            }
        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = sender as SearchBar;
            if (vatRefundsListView.DataSource != null)
            {
                this.vatRefundsListView.DataSource.Filter = FilterRefundsList;
                this.vatRefundsListView.DataSource.RefreshFilter();
            }
        }

        void SearchButton_Tapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = false;
            viewModel.IsCloseButtonVisible = true;

        }

        void CloseSearchButton_Tapped(object sender, EventArgs e)
        {
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
        }

        private bool FilterRefundsList(object obj)
        {
            try
            {
                if (searchBar == null || searchBar.Text == null)
                    return true;

                var vatRefHeaderSetResult = obj as VatRefHeaderSetResult;
                if (vatRefHeaderSetResult.RefundFbnum.ToLower().Contains(searchBar.Text.ToLower())
                     || vatRefHeaderSetResult.RefundFbnum.ToLower().Contains(searchBar.Text.ToLower()))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;


            }
        }
    }
}
