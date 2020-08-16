using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    
    public partial class NewTaxEvasionFormSuccessPaveView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;
        public NewTaxEvasionFormSuccessPaveView()
        {
            InitializeComponent();
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {

            //await Task.Run(() =>
            //{
            //    viewModel.IsLoading = true;

            //});
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);

            });

        }


        protected override bool OnBackButtonPressed() => true;

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {

            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
        }
    }
}
