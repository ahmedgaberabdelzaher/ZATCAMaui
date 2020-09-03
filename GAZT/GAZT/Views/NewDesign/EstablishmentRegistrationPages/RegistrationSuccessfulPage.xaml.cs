using System;
using System.Collections.Generic;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class RegistrationSuccessfulPage : ContentPage
    {
        private RegistrationSuccessfulViewModel viewModel;
        public RegistrationSuccessfulPage(TaxPayerDetails taxpayerProfile)
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.RegistrationSuccessfulPage;
            viewModel.taxPayerDetails = taxpayerProfile;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
        }

        
       private void GoToDashBoardButtonClick(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
                Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg1);
            }
            viewModel._navigationService.GoBack();
        }

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
    }
}
