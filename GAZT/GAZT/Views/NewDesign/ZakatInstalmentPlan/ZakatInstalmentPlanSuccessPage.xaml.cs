using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using GAZT.Helper;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ZakatInstalmentPlanSuccessPage : ContentPage
    {
        ZakatInstalmentPlanViewModel viewModel;
        public ZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.ZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;

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
                this.FlowDirection = FlowDirection.LeftToRight;
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
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                    }
                  
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }
        }




        private void Instalment_plan_Tapped(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
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

                MessagingCenter.Send<Object, Boolean>(this, "ISCallBackFromSuccess", true);

                //Application.Current.MainPage.Navigation.PopAsync();



            });













            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    var _navigation = Application.Current.MainPage.Navigation;

            //    foreach (var item in _navigation.NavigationStack)
            //    {
            //        if (item.GetType().Name == App.VatInstalmentPlanSuccessPage)
            //        {
            //            _navigation.RemovePage(item);
            //            break;
            //        }
            //    }
            //    viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);

            //    //Application.Current.MainPage.Navigation.PopAsync();

            //});

        }




        private async void Download_Acknowledgement(object sender, EventArgs e)
        {

            await Task.Run(() =>
            {
                viewModel.IsLoading = true;



            });

            if (viewModel.ZakatReferanceNumber != null)
            {

                String downloadurl = Constants.downloadFile + "'" + viewModel.ZakatReferanceNumber + "')/$value";
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