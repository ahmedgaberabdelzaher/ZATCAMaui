using System;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using Xamarin.Essentials;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace EGAZT.Views.NewDesign.VatReview
{
    [Preserve(AllMembers = true)]
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
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            viewModel = App.Locator.VatReviewSuccessView;

            this.BindingContext = viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

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

           
                    await Clipboard.SetTextAsync(viewModel.VATReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                    }
                   
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = true;
            });
            if (viewModel.VATReferanceNumber != null)
            {

                string downloadurl = Constants.downloadFile + "'" + viewModel.VATReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void VatReview_Tapped(object sender, EventArgs e)
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