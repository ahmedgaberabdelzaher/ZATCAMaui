using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [Preserve(AllMembers = true)]
    public partial class QuickActionPopUpPageView : PopupPage
    {
        QuickActionPopUpPageViewModel viewModel;
        public QuickActionPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.QuickActionPopUpPageView;
            this.BindingContext = viewModel;
            SetLTR();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ZAKATReturn.TranslateTo(0, 500, 10);
            await ReadInbox.TranslateTo(0, 500, 10);
            await GetSupport.TranslateTo(0, 500, 10);

            ZAKATReturn.TranslateTo(0, 0, 800);
            ReadInbox.TranslateTo(0, 0, 1000);
            GetSupport.TranslateTo(0, 0, 1200);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
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
            await Task.Run(async () =>
            {
                ZAKATReturn.TranslateTo(0, 500, 1200);
                ReadInbox.TranslateTo(0, 500, 1200);
                GetSupport.TranslateTo(0, 500, 1200);
            });
            PopupNavigation.Instance.PopAsync();
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
