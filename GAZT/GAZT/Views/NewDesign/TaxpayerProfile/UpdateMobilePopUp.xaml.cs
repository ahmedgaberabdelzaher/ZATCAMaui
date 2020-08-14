using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMobilePopUp : PopupPage
    {
        public UpdateMobilePopUp()
        {
            InitializeComponent();
        }

        private void UpdatedClicked(object sender, EventArgs e)
        {
            if (btn.Text == "Update")
            {
                UpdateMobile.IsVisible = false;
                VerificationView.IsVisible = true;
                btn.Text = "Verify";
            }
        }

        private void OTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}