using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReturnNewSuccessPageView : ContentPage
    {
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel viewModel;
        public VatReturnNewSuccessPageView(String refNum)
        {
            InitializeComponent();

            viewModel = App.Locator.GAZTNewDesignVATReturnUpdatedUIPageView;
            viewModel.ReferenceNumber = refNum;
            viewModel.TaxablePeriod = viewModel.VATDeclarationData.d.Fbnum;
            this.BindingContext = viewModel;
            SetLTR();
            ChangeAeroIcon();
            //  On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;

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
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        
   
        //private void GoToDashboardClicked(object sender, EventArgs e)
        //{
        //    if (Navigation.NavigationStack.Count > 0)
        //    {
        //        Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 3];
        //        Navigation.RemovePage(pg);
        //    }
        //    viewModel._navigationService.GoBack();
        //}

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

        private void GotodashboardClicked(System.Object sender, System.EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 3];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }
    }
}