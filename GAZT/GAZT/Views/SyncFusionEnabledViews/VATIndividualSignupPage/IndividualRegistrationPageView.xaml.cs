using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualRegistrationPageView : ContentPage
    {
        private static int CurrentView;
        IndividualRegistrationPageViewModel viewModel;
        public IndividualRegistrationPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.IndividualRegistrationPageView;
            this.BindingContext = viewModel;
        }

        /*        private void OnIDTypeTapped(object sender, EventArgs e)
                {
                    DDlIDType.IsOpen = true;
                }
                private void OnDOBTapped(object sender, EventArgs e)
                {
                    DpDbo.IsOpen = true;
                }
                private void DDlIDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void IDTypePicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void IDTypePicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DpDbo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
                {

                }

                private void DpDbo_Closed(object sender, EventArgs e)
                {

                }*/
        #region
        private void EntryTIN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        {

        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        private void LIssuedBy_Clicked(object sender, EventArgs e)
        {

        }

        private void LIssuedByCity_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSubmitNext_Clicked(object sender, EventArgs e)
        {

        }

        private void DDlIDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DpDbo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedBy_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedBy_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedByCity_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedByCity_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnDateEntryFocussed(object sender, FocusEventArgs e)
        {

        }

        private void DpDbo_Closed(object sender, EventArgs e)
        {

        }
        #endregion
        private void btnContinue_Clicked(object sender, EventArgs e)
        {
/*            if (CurrentView <= 6)
            {
                CurrentView++;
            }
            switch (CurrentView)
            {
                case 1:
                    break;
                case 2:
                    BoxTwo.BackgroundColor = Color.DarkGreen;
                    NationalAddressView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = ContactInformationView.IsVisible =
                    SummeryView.IsVisible = PasswordView.IsVisible = false;
                    //await BoxTwo.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 3:
                    BoxThree.BackgroundColor = Color.DarkGreen;
                    ContactInformationView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = NationalAddressView.IsVisible = 
                    SummeryView.IsVisible = PasswordView.IsVisible = false;
                   // await BoxThree.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 4:
                    BoxFour.BackgroundColor = Color.DarkGreen;
                    btnContinue.Text = "Confirm";
                    SummeryView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = ContactInformationView.IsVisible = NationalAddressView.IsVisible =
                       PasswordView.IsVisible = false;
                 //   await BoxFive.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 5:
                    BoxFive.BackgroundColor = Color.DarkGreen;
                    btnContinue.Text = "Continue";
                    PasswordView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = ContactInformationView.IsVisible = NationalAddressView.IsVisible =
                        SummeryView.IsVisible = false;
                    break;
                case 6:
                    viewModel._navigationService.NavigateTo(App.RegistrationSuccessfulPageView);
                    break;
            }*/
        }
        protected override void OnAppearing()
        {
/*            BoxOne.BackgroundColor = Color.DarkGreen;
            //await BoxOne.TranslateTo(100,0,500,Easing.BounceOut);
            BoxTwo.BackgroundColor = BoxThree.BackgroundColor = BoxFour.BackgroundColor = BoxFive.BackgroundColor = Color.LightGray;
            IndividualRegistrationView.IsVisible = true;
            NationalAddressView.IsVisible = ContactInformationView.IsVisible =
            SummeryView.IsVisible = PasswordView.IsVisible = false;
            CurrentView = 1;*/
        }
    }
}