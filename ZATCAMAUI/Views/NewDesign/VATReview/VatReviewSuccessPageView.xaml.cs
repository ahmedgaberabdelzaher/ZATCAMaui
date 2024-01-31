

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATReview
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
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            viewModel = App.Locator.VatReviewSuccessView;

            BindingContext = viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
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
            catch (Exception)
            {




            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = true;
            });
            if (viewModel.VATReferanceNumber != null)
            {

                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.VATReferanceNumber + "')/$value";
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
            MainThread.BeginInvokeOnMainThread(() =>
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

            viewModel._navigationService.NavigateTo(App.VatReviewListPageView);
        }
    }
}