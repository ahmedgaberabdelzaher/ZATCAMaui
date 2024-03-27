

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatInstalmentPlanSuccessPage : ContentPage
    {
        VATInstalmentPlanViewModel viewModel;
        public VatInstalmentPlanSuccessPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VatInstalmentPlanSuccessPageView;
            BindingContext = viewModel;
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
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);
                        });

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
                    if (item.GetType().Name == App.VatInstalmentPlanPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }



                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.VatInstalmentPlanListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.VatInstalmentPlanSuccessPage)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                viewModel._navigationService.NavigateTo(App.VatInstalmentPlanListPageView);

                MessagingCenter.Send<object, bool>(this, "ISCallBackFromSuccess", true);

                



            });

        }


        private async void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.VATReferanceNumber != null)
            {

                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });

                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.VATReferanceNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);


                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;

                });
            }
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