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
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel= App.Locator.SignUpForEstablishmentPageView;
        public SignUpForEstablishmentPageView()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void OTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void OTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnNewPasswordTapped(object sender, EventArgs e)
        {

        }

        private void NewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnConfirmNewPasswordTapped(object sender, EventArgs e)
        {

        }
    }
}