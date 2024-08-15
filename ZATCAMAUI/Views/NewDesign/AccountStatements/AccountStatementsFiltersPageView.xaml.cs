
using Mopups.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{
    public partial class AccountStatementsFiltersPageView : PopupPage
    {
        AccountStatementsFiltersPageViewModel viewModel;
        public AccountStatementsFiltersPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AccountStatementsFiltersPageView;
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.PopulateFiltersData();
        }


    }
}
