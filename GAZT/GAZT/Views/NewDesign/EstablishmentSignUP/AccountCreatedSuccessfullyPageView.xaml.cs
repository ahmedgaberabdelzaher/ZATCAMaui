using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreatedSuccessfullyPageView : ContentPage
    {
        AccountCreatedSuccessfullyPageViewModel viewModel;
        public AccountCreatedSuccessfullyPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AccountCreatedSuccessfullyPageView;
            BindingContext = App.Locator.AccountCreatedSuccessfullyPageView;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void OnGoToLoginClicked(object sender, EventArgs e)
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
    }
}