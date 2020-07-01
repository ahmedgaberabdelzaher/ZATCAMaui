using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessfulPageView : ContentPage
    {
        RegistrationSuccessfulPageViewModel viewModel;
        public RegistrationSuccessfulPageView(string TIN)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfulPageView;
            this.BindingContext = viewModel;
            viewModel.TINnumber = TIN;
        }

        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {

        }

        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            Clipboard.SetTextAsync(Label_Tin.Text);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                var displayText = AppResources.TINS + " " + text;
                viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
            }
        }
        protected override bool OnBackButtonPressed() => true;
    }
}