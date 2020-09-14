using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    public partial class QuickActionPopUpPageView :  PopupPage
    {
        QuickActionPopUpPageViewModel viewModel;
        public QuickActionPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.QuickActionPopUpPageView;
            this.BindingContext = viewModel;
            ZAKATReturn.IsVisible = false;
            VATReturn.IsVisible = false;
            ReadInbox.IsVisible = false;
            GetSupport.IsVisible = false;
            CloseIcon.IsVisible = false;

            






            SetLTR();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ZAKATReturn.IsVisible = true;
            await ZAKATReturn.TranslateTo(0, -100, 2000);
            VATReturn.IsVisible = true;
            await VATReturn.TranslateTo(0, -100, 2000);
            ReadInbox.IsVisible = true;
            await ReadInbox.TranslateTo(0, -100, 2000);
            GetSupport.IsVisible = true;
            await GetSupport.TranslateTo(0, -100, 2000);
            CloseIcon.IsVisible = true;
            await CloseIcon.TranslateTo(0, -100, 2000);


        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
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
            viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

        }

        private async void OnGetSupportClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.SupportPageView);
            //viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
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
