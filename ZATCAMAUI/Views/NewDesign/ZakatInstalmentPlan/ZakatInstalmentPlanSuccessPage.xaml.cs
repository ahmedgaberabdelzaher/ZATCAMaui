using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ZakatInstalmentPlanSuccessPage : ContentPage
    {
        ZakatInstalmentPlanViewModel viewModel;
        public ZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.ZakatInstalmentPlanSuccessPageView;
            BindingContext = viewModel;

            if (Preferences.Get("IsFromRevok", false))
            {
                viewModel.SuccessMessage = AppResources.ZakatInstalmentRevokedSuccessfully;
                viewModel.ZakatReferanceNumber = Preferences.Get("RevokeRef", "");
            }
            else
            {
                viewModel.SuccessMessage = AppResources.VatInstalmentPlanSubmittedSuccess;
            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);
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
                    if (item.GetType().Name == App.ZakatInstalmentPlanPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }



                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ZakatInstalmentPlanListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ZakatInstalmentPlanSuccessPage)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                viewModel._navigationService.NavigateTo(App.ZakatInstalmentPlanListPageView);

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

                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.ZakatReferanceNumber + "')/$value";
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


        }

    }

}