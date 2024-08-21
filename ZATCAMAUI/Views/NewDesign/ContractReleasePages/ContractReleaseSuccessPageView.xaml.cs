
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseSuccessPageView : ContentPage
    {
        public ContractReleaseViewModel viewModel;
        public ContractReleaseSuccessPageView(ContractReleaseViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;

            viewModel.ReferenceNumberTxt = viewModel.ContractReleaseData1.d.Fbnumz;
            viewModel.ContractNumberTxt = viewModel.ContractReleaseData1.d.AContNo;
        }



        private void Dashboard_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ContractReleasePageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ContractReleaseListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.ContractReleaseSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
            viewModel._navigationService.NavigateTo(App.ContractReleaseListPageView);
        }

        private async void ReferenceNumberCopyTapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ContractReleaseData.d.Fbnumz != null)
                {
                    await Clipboard.SetTextAsync(viewModel.ContractReleaseData.d.Fbnumz);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessageBox(AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
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
            if (viewModel.ContractReleaseData.d.Fbnumz != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });

                string downloadurl = ZATCAConstants.CRDownloadAcknowledementFile + viewModel.ContractReleaseData.d.Fbnumz;
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private void Download_AcknowledgementForm(object sender, EventArgs e)
        {
            if (viewModel.ContractReleaseData1.d.Fbnumz != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });
                string downloadurl = ZATCAConstants.CRDownloadCoverFormFile + viewModel.ContractReleaseData1.d.Fbnumz;
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private async void ContractNumberCopyTapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ContractReleaseData.d.AContNo != null)
                {
                    await Clipboard.SetTextAsync(viewModel.ContractReleaseData.d.AContNo);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessageBox(AppResources.CRContractingNumber + " " + text, AppResources.Copied);
                        });
                    }
                }
            }
            catch (Exception)
            {


            }
        }
    }
}