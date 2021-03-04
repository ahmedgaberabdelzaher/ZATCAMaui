using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.MyBillsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class MyBillsSadadDetailsPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        GAZTNewDesignDashBoardPageViewModel _dashBoardPageViewModel;
        private int screenIndex;
        public MyBillsSadadDetailsPageView(int screenIndex)
        {
            InitializeComponent();
            this.screenIndex = screenIndex;


            if(screenIndex == 0) {

                _dashBoardPageViewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                _dashBoardPageViewModel.ReferenceNumber = _dashBoardPageViewModel.selectedSadadNo;
                _dashBoardPageViewModel.TotalAmount = _dashBoardPageViewModel.selectedAmount;
                this.BindingContext = _dashBoardPageViewModel;
            }
            else if(screenIndex == 1) {

                viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
                viewModel.ReferenceNumber = viewModel.selectedSadadNo;
                viewModel.TotalAmount = viewModel.selectedAmount;
                this.BindingContext = viewModel;
            }


            //if (!isDashboard)
            //{
            //    viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
            //    viewModel.ReferenceNumber = viewModel.selectedSadadNo;
            //    viewModel.TotalAmount = viewModel.selectedAmount;
            //    this.BindingContext = viewModel;    
            //}
            //else
            //{
            //    _dashBoardPageViewModel = App.Locator.GAZTNewDesignDashBoardPageView;
            //    _dashBoardPageViewModel.ReferenceNumber = _dashBoardPageViewModel.selectedSadadNo;
            //    _dashBoardPageViewModel.TotalAmount = _dashBoardPageViewModel.selectedAmount;
            //    this.BindingContext = _dashBoardPageViewModel;
            //}

            
            SetLTR();
            ChangeAeroIcon();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

        }

        private async void OnCopySADADNumberButtonClicked(object sender, EventArgs e)
        {


            if (screenIndex == 0)
            {
                
                await Clipboard.SetTextAsync(_dashBoardPageViewModel.ReferenceNumber);
            }
            else if(screenIndex == 1)
            {
                await Clipboard.SetTextAsync(viewModel.ReferenceNumber);    
            }
            
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.MyBillsSADADPaymentNumberCopy + " " + text));

                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }   
        }

        private void DoneClicked(object sender, EventArgs e)
        {
            if (screenIndex == 0)
            {
                _dashBoardPageViewModel._navigationService.GoBack();
            }
            else if (screenIndex == 1)
            {
                viewModel._navigationService.GoBack();    
            }
            //viewModel._navigationService.GoBack();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

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