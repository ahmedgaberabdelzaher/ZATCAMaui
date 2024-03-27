using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Page = Microsoft.Maui.Controls.Page;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnDetailsSuccessfullPageView : ContentPage
    {
        ZakatReturnDetailsSuccessfullPageViewModel viewModel;
        ZakatReturnDetailsD _zakatReturnDetail;
        public ZakatReturnDetailsSuccessfullPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnDetailsSuccessfullPageView;

            // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            _zakatReturnDetail = ZakatReturnDetail;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            viewModel.TaxablePeriod = ZakatReturnDetail.Persl;
            ChangeAeroIcon();
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            _ = viewModel.OnPageLoad(ZakatReturnDetail);

            ToolbarItem Refresh = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    //  await OnRefreshButtonClicked();
                    // viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                })
            };
            ToolbarItems.Add(Refresh);
            //  Refresh.SetBinding(ToolbarItem.IconImageSourceProperty, new Binding("RefreshIconImageSource"));


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
            try
            {
                if (viewModel.IsrefreshEnabled)
                {
                    await viewModel.OnPageLoad(_zakatReturnDetail);
                }
                else
                {
                    // put Mesage already latest SADADID available
                }
            }
            catch (Exception)
            {

            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", async (sender, arg) =>
                {
                    viewModel.MadaPaymentSelectedAsync();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", async (sender, arg) =>
                {
                    viewModel.ApplePaySelected();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {
                    viewModel.gotoSuccessPage();
                });
            }
            catch (Exception)
            {


            }

            try
            {
                MessagingCenter.Subscribe<App, string>(this, "ApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();

                    await viewModel.UpdateApplePayPaymentGuid();


                });

            }
            catch (Exception)
            {


            }

        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, string>(this, "Card_Payment");
            MessagingCenter.Unsubscribe<object, string>(this, "Apple_Pay");
            MessagingCenter.Unsubscribe<object, string>(this, "SADAD");
            MessagingCenter.Unsubscribe<App, string>(this, "ApplePayData");


        }
        private void OnReturnClicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
               Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }

        public async void OnCopySadadNumberButtonClicked(object sender, EventArgs args)
        {
            await Clipboard.SetTextAsync(viewModel.ReferenceNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.CRReferenceNumber + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }
        }

        private void PayNowClicked(object sender, EventArgs e)
        {


            _ = viewModel.doValidateZakatAmount();

        }




        private async void OnCopyTaxablePeriodClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(viewModel.TaxablePeriod);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatTaxablePeriod + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }
        }
    }
}