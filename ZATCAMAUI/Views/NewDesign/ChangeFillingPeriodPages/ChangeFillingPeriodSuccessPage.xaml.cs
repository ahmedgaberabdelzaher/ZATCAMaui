

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodSuccessPage : ContentPage
    {
        public ChangeFillingPeriodViewModel viewModel;

        public ChangeFillingPeriodSuccessPage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ChangeFillingPeriodSuccessPageView;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }


        private void Dashboard_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ChangeFillingPeriodPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ChangeFillingPeriodListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ChangeFillingPeriodSuccessPage)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);
        }

        private async void ReferenceNumberCopyTapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ChangeFillingResponse.d.Fbnumz != null)
                {
                    await Clipboard.SetTextAsync(viewModel.ChangeFillingResponse.d.Fbnumz);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessageBox(
                            AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
                        });
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.ChangeFillingResponse.d.Fbnumz != null)
            {
                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.ChangeFillingResponse.d.Fbnumz + "')/$value";
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
        }
    }
}