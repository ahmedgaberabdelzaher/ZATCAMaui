
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{
    public partial class RegisterZATCAUserPage : ContentPage
    {
        RegisterZATCAUserViewModel viewModel;
        public RegisterZATCAUserPage(int CommingFrom)
        {

            InitializeComponent();
            viewModel = App.Locator.RegisterZATCAUserViewModel;

            viewModel.CommingFrom = CommingFrom;

            var unRegisteredUser = (ZATCAUserRegisterModel)App.Locator.StateManager.GetItem("UnRegisteredUser");

            if (unRegisteredUser != null)
                viewModel.UnRegisteredUser = unRegisteredUser;

            BindingContext = viewModel;

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}

