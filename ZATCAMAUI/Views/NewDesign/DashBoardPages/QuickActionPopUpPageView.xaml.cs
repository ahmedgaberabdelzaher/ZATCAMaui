using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
{

    public partial class QuickActionPopUpPageView : PopupPage
    {
        QuickActionPopUpPageViewModel viewModel;
        public QuickActionPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.QuickActionPopUpPageView;
            this.BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.WhenAll(new Task[] { ZAKATReturn.TranslateTo(0, 500, 10), ReadInbox.TranslateTo(0, 500, 10), GetSupport.TranslateTo(0, 500, 10) });

            await ZAKATReturn.TranslateTo(0, 0, 100);
            await ReadInbox.TranslateTo(0, 0, 500);
            await GetSupport.TranslateTo(0, 0, 600);
        }
        private async void OnOverdueReturnClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);
        }

        private async void OnMyReturnsClickedForZAKAT(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 5);
        }


        private async void OnMyReturnsClickedForVATDeclaration(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 6);
        }

        private async void OnCorrespondanceClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.TaxpayersCertificatesPageView);
        }

        private async void OnGetSupportClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.SupportPageView);
        }

        private async void OnCloseTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                ZAKATReturn.TranslateTo(0, 500, 1200);
                ReadInbox.TranslateTo(0, 500, 1200);
                GetSupport.TranslateTo(0, 500, 1200);
            });
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnMyReturnsClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);
        }

        private async void OnMyBillsClicked(object sender, EventArgs e)
        {
            BillInfo billInfo = new BillInfo();
            billInfo.BillTypeName = AppResources.All;
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }
    }
}
