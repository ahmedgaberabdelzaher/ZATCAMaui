using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{
  
    public partial class RegistrationSuccessfulPage : ContentPage
    {
        private RegistrationSuccessfulViewModel viewModel;
        public RegistrationSuccessfulPage(TaxPayerDetails taxpayerProfile)
        {
            InitializeComponent();
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
                            Page pg = Navigation.NavigationStack[indexToRemoveThePage];
                            Navigation.RemovePage(pg);
                            indexToRemoveThePage = Navigation.NavigationStack.Count - 1;
                            Page pg1 = Navigation.NavigationStack[indexToRemoveThePage];
                            Navigation.RemovePage(pg1);
                        }
                        catch (Exception)
                        {


                        }

                    }
                    else
                    {
                       Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(pg);
                        viewModel._navigationService.GoBack();
                    }

                }
            }
            catch (Exception)
            {


            }

        }
    }
}
