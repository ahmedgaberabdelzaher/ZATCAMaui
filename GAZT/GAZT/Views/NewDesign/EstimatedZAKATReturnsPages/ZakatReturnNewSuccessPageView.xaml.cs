using System;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Internals;
using EGAZT.ViewModel.NewDesignViewModel;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnNewSuccessPageView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;

        public ZakatReturnNewSuccessPageView(string CaseGuild)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;

            // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
           
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
            ChangeAeroIcon();

            viewModel.ReferenceNumber = CaseGuild;
            viewModel.TaxablePeriod = viewModel.FromDate;
            
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private async void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            
        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            //Navigation.RemovePage(pg);
        }
        private void GoToDashboardClicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 3];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
        }

        public async void OnCopyReferenceNumberButtonClicked(object sender, EventArgs args)
        {
            await Clipboard.SetTextAsync(viewModel.ReferenceNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDReferenceNumber + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }
        }

    }
}