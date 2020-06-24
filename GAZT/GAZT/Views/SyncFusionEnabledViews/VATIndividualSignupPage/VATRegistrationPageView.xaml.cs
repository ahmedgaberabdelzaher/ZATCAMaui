using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationPageView : ContentPage
    {
        VATRegistrationPageViewModel viewModel;
        public VATRegistrationPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationPageView;
            this.BindingContext = viewModel;
        }

        private void DpEStartDate_Closed(object sender, EventArgs e)
        {

        }

        private void DpEStartDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DpEStartDate_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DpEStartDate.IsOpen = true;
        }

        private void btnImporter_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {

        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView());
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.VATRegistrationSuccessfullPageView);
        }

        private void DateEntry_Focused(object sender, FocusEventArgs e)
        {

        }

        private void DateEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void DpEStartDate_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void EntryTINNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryTINNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnID_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void EntryIDNo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryFirstName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryLastName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SideMenu_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new VATRegistrationMenuPopUp());
        }

        private void btnContactID_Clicked(object sender, EventArgs e)
        {
            DDlContactIDType.IsOpen = true;
        }
    }
}