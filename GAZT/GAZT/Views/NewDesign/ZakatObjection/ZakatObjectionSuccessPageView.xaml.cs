using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.ZakatObjection
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessPageView : ContentPage
    {
        private ZakatObjectionViewModel _viewModel;
        public ZakatObjectionSuccessPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            _viewModel = App.Locator.ZakatObjectionView;

            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

            }
            catch (Exception ex)
            {

            }
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
            catch (Exception ex)
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

                String downloadurl = Constants.downloadFile + "'" + _viewModel.VATReferanceNumber + "')/$value";
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

                String downloadurl = Constants.downloadFormFile + "'" + _viewModel.VATReferanceNumber + "')/$value";
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