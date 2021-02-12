using System;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.MyBillsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class MyBillsSuccessPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        public MyBillsSuccessPageView(String refNum)
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
            viewModel.ReferenceNumber = refNum;
            viewModel.TaxablePeriod = "";
            this.BindingContext = viewModel;
            SetLTR();
            ChangeAeroIcon();
        }

        private async void OnCopyReferenceNumberButtonClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(viewModel.ReferenceNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDReferenceNumber + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }   
        }

        private void GotodashboardClicked(object sender, EventArgs e)
        {
        
            var _navigation = Application.Current.MainPage.Navigation;
            
            //foreach (var item in _navigation.NavigationStack)
            //{
            //    if (item.GetType().Name == App.GAZTNewDesignMyReturnsNewPageView)
            //    {
            //        _navigation.RemovePage(item);
            //        break;
            //    }
            //}
          
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignMyBillsPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.PaymentProcessWebview)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            
            viewModel._navigationService.GoBack();
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
    }
}