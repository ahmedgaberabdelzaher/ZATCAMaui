using System.Text.RegularExpressions;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.Common
{
    /// <summary>
    /// View used to show the email entry with validation status.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailEntry : ContentView
    {
        SFLoginPageViewModel viewModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailEntry" /> class.
        /// </summary>
        public EmailEntry()
        {
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;
            SetPickerFont();
            //TinsPicker

        }
        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        TinsPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                        TinsPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                        TinsPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                        TinsPicker.TextStyle.FontFamily = "Somar-SemiBold";
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        TinsPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TinsPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TinsPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        TinsPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        break;
                }
            }
            catch (Exception)
            {


            }

        }

        private void TINs_Clicked(object sender, EventArgs e)
        {
            TinsPicker.IsOpen = true;
        }
        private void Email_UnFocused(object sender, FocusEventArgs e)
        {
            bool isNumber = false;
            bool isEmailValid = false;
            isNumber = IsEnglishNumber(email.Text);
            if (!isNumber)
            {
                isEmailValid = CheckValidEmail(email.Text);
                if (!isEmailValid)
                {
                    EmailInputLayout.HasError = true;
                    //EmailInputLayout.ShowHint = true;
                }
                else
                {
                    EmailInputLayout.HasError = false;
                    MessagingCenter.Send("TinList", "TinList");
                }
            }
            else
            {
                EmailInputLayout.HasError = false;
            }
        }
        private static bool CheckValidEmail(string email)
        {
            bool isEmailValid = false;
            if (!string.IsNullOrEmpty(email))
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                isEmailValid = regex.IsMatch(email) && !email.EndsWith(".");
            }
            return isEmailValid;
        }
        public static bool IsEnglishNumber(string arText)
        {
            bool isAllNumeric = true;
            if (!string.IsNullOrEmpty(arText))
            {
                foreach (char letter in arText.ToCharArray())
                {
                    if (!(letter >= 48 && letter <= 57))
                    {
                        isAllNumeric = false;
                    }
                }
            }
            return isAllNumeric;
        }
        private void TinsPicker_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {
                TINModel selectedtin = viewModel.TINs[TinsPicker.Columns[0].SelectedIndex];
                //TinsPicker.SelectedItem = selectedtin;//TINID
                viewModel.SelectedTinId = selectedtin;//selectedregion
                viewModel.SelectedTinIdPrev = selectedtin;//selectedregion
                viewModel.TINID = selectedtin.TIN;

            }
            catch (Exception)
            {


            }
        }
        private void TinsPicker_CancelButtonClicked(object sender, EventArgs e)
        {
            // TinsPicker.SelectedItem = viewModel.SelectedTinIdPrev;//TINID
            viewModel.SelectedTinId = viewModel.SelectedTinIdPrev;//selectedregion
            if (viewModel.SelectedTinIdPrev == null)
            {
                viewModel.TINID = string.Empty;
            }
        }
    }
}