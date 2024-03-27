

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessPageView : ContentPage
    {
        private ZakatObjectionViewModel _viewModel;
        public ZakatObjectionSuccessPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            _viewModel = App.Locator.ZakatObjectionSuccessView;

            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

            }
            catch (Exception)
            {


            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["Back"];
            }
        }

        private async void Instalment_copy_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (_viewModel.VATReferanceNumber != null)
                {

                    await Clipboard.SetTextAsync(_viewModel.VATReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await _viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                    }

                }
            }
            catch (Exception)
            {




            }
        }
        private void ZAkat_Objections_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZakatObjectionPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZakatObjectionsListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ZakatObjectionSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            //await Application.Current.MainPage.Navigation.PushAsync(new VatReviewListPageView());

            _viewModel._navigationService.NavigateTo(App.ZakatObjectionsListPageView);
        }

        private void Reference_Num_copy_Tapped(object sender, EventArgs e)
        {

        }

        private async void Download_Acknowledgement(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                _viewModel.IsLoading = true;


            });
            if (_viewModel.VATReferanceNumber != null)
            {

                string downloadurl = ZATCAConstants.downloadFile + "'" + _viewModel.VATReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                _viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }


            await Task.Run(() =>
            {
                _viewModel.IsLoading = false;


            });
        }

        private async void Download_Form(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                _viewModel.IsLoading = true;


            });
            if (_viewModel.VATReferanceNumber != null)
            {

                string downloadurl = ZATCAConstants.downloadFormFile + "'" + _viewModel.VATReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                _viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
            await Task.Run(() =>
            {
                _viewModel.IsLoading = false;


            });


        }
    }
}