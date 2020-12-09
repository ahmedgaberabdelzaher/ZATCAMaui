using System;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodSuccessPage : ContentPage
    {
        public ChangeFillingPeriodViewModel viewModel;

        public ChangeFillingPeriodSuccessPage()
        {
            InitializeComponent();

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ChangeFillingPeriodPageView;

            this.BindingContext = viewModel;

            // ReferenceNumberTxt.Text = viewModel.ChangeFillingResponse.d.Fbnumz;
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
                        await viewModel._dialogService.ShowMessageBox(
                            AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.ChangeFillingResponse.d.Fbnumz != null)
            {

                String downloadurl = Constants.downloadFile + "'" + viewModel.ChangeFillingResponse.d.Fbnumz + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
        }
    }
}