

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OldZakatInstalmentPlanSuccessPage : ContentPage
    {
        OldZakatInstalmentPlanViewModel viewModel;
        public OldZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();

            viewModel = App.Locator.OldZakatInstalmentPlanSuccessPageView;
            BindingContext = viewModel;


            viewModel.SuccessMessage = AppResources.VatInstalmentPlanSubmittedSuccess;

        }

        private async void Instalment_copy_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ZakatReferanceNumber != null)
                {


                    await Clipboard.SetTextAsync(viewModel.ZakatReferanceNumber);
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




        private void Instalment_plan_Tapped(object sender, EventArgs e)
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.OldZakatInstalmentPlanPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.OldZakatInstalmentPlanListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.OldZakatInstalmentPlanSuccessPage)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                viewModel._navigationService.NavigateTo(App.OldZakatInstalmentPlanListPageView);

                MessagingCenter.Send<object, bool>(this, "ISCallBackFromSuccess", true);

                //Application.Current.MainPage.Navigation.PopAsync();



            });

        }




        private async void Download_Acknowledgement(object sender, EventArgs e)
        {

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;



            });

            if (viewModel.ZakatReferanceNumber != null)
            {

                string downloadurl = ZATCAConstants.ZOdownloadAckLetter + "'" + viewModel.ZakatReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);


            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;



            });
        }

        private async void Download_Form(object sender, EventArgs e)
        {


            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            if (viewModel.ZakatReferanceNumber != null)
            {
                string downloadurl = ZATCAConstants.OldZakatdownloadCoverFormFile + "'" + viewModel.ZakatReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);



            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }

    }

}