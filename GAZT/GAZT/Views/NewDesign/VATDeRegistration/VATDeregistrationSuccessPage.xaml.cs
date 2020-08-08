using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    public partial class VATDeregistrationSuccessPage : ContentPage
    {
        VATDeregistrationSuccessPageViewModel viewModel;
        public VATDeregistrationSuccessPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationSuccessPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);

        }

   
        protected override bool OnBackButtonPressed() => true;

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            // viewModel._navigationService.NavigateTo(App.SFAnonymousLandingPageView);
            //  viewModel._navigationService.NavigateTo(App.SFLoginPageView);

            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);


        }

    }
}
