using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.ContractReleasePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseSuccessPageView : ContentPage
    {
        public ContractReleaseViewModel viewModel;

        public ContractReleaseSuccessPageView(ContractReleaseViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            //viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;

            ReferenceNumberTxt.Text = viewModel.ContractReleaseData.d.Fbnumz;
            ContractNumberTxt.Text = viewModel.ContractReleaseData.d.AContNo;
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
                        await viewModel._dialogService.ShowMessageBox(AppResources.CRReferenceNumber + " " + text, AppResources.Copied);

                    }

                }
            }
            catch (Exception ex)
            {



            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.ContractReleaseData.d.Fbnumz != null)
            {



                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading1 = true;
                });



                String downloadurl = Constants.CRDownloadAcknowledementFile + "'" + viewModel.ContractReleaseData.d.Fbnumz + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);



                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading1 = false;
                });
            }
        }



        private void Download_AcknowledgementForm(object sender, EventArgs e)
        {
            if (viewModel.ContractReleaseData.d.Fbnumz != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading1 = true;
                });
                String downloadurl = Constants.CRDownloadCoverFormFile + "'" + viewModel.ContractReleaseData.d.Fbnumz + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading1 = false;
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
                        await viewModel._dialogService.ShowMessageBox(AppResources.CRContractingNumber + " " + text, AppResources.Copied);

                    }

                }
            }
            catch (Exception ex)
            {



            }
        }
    }
}