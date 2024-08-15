
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

            viewModel = App.Locator.ChangeFillingPeriodSuccessPageView;
            BindingContext = viewModel;
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
                if (viewModel.ChangeFillingResponse.d1.Fbnumz != null)
                {
                    await Clipboard.SetTextAsync(viewModel.ChangeFillingResponse.d1.Fbnumz);
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
            if (viewModel.ChangeFillingResponse.d1.Fbnumz != null)
            {
                String downloadurl = ZATCAConstants.downloadFile + viewModel.ChangeFillingResponse.d1.Fbnumz;
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
        }
    }
}