using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    [Preserve(AllMembers = true)]
    public partial class VATRefundsInstructionsPageView : PopupPage
    {
        VATRefundsInstructionsPageViewModel viewModel;

        public VATRefundsInstructionsPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRefundsInstructionsPageView;
            this.BindingContext = viewModel;
            SetLTR();
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            await viewModel.ReloadData();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                btnConfirmVATRefundInstructions.FontSize = 15;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void VATRefundRequestInstructions_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                viewModel.VATRefundInstructionsConfirmedBtnTapped();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
