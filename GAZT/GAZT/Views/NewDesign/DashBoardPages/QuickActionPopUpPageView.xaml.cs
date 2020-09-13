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
            SetLTR();
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

        private async void OnCorrespondanceClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);

        }
    }
}
