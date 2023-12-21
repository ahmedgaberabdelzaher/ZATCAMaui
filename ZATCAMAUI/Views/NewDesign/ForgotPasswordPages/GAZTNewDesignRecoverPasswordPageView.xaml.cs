using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.ForgotPasswordPages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignRecoverPasswordPageView : ContentPage
    {
        GAZTNewDesignRecoverPasswordPageViewModel viewModel;
        public GAZTNewDesignRecoverPasswordPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignRecoverPasswordPageViewModel;
            BindingContext = viewModel;
            NavigationPage.SetHasBackButton(this, false);

        }

        private async void OnLogInClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}