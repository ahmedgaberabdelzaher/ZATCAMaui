using System;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [Preserve(AllMembers = true)]
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
            try
            {
                if (Navigation.NavigationStack.Count > 0)
                {
                    int PageCount = Navigation.NavigationStack.Count;
                    int indexToRemoveThePage = PageCount - 2;
                    if (!string.IsNullOrEmpty(viewModel.taxPayerDetails?.Fbsta) && viewModel.taxPayerDetails?.Fbsta != "IP011")
                    {
                        try
                        {
                            Xamarin.Forms.Page pg = Navigation.NavigationStack[indexToRemoveThePage];
                            Navigation.RemovePage(pg);
                            indexToRemoveThePage = Navigation.NavigationStack.Count-1;
                            Xamarin.Forms.Page pg1 = Navigation.NavigationStack[indexToRemoveThePage];
                            Navigation.RemovePage(pg1);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    else
                    {
                        Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(pg);
                        viewModel._navigationService.GoBack();
                    }

                }
            }
            catch (Exception ex)
            {

            }

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
