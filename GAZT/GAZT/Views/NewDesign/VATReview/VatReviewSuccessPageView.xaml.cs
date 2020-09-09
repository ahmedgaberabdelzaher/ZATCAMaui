using System;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using Xamarin.Essentials;

namespace EGAZT.Views.NewDesign.VatReview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewSuccessPageView : ContentPage
    {
        public VatReviewViewModel viewModel;
        public VatReviewSuccessPageView()
        {
            InitializeComponent();
        
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.VatReviewView;

            this.BindingContext = viewModel;
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private async void Instalment_copy_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.VATReferanceNumber != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsLoading = true;
                    });

                    await Clipboard.SetTextAsync(viewModel.VATReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);



                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
            }
            catch (Exception ex)
            {



            }
        }
        private async void VatReview_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            
            //await Application.Current.MainPage.Navigation.PushAsync(new VatReviewListPageView());

            viewModel._navigationService.NavigateTo(App.VatReviewListPageView);
        }
    }
}