using EGAZT.Helper;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OldZakatInstalmentPlanSuccessPage : ContentPage
    {
        OldZakatInstalmentPlanViewModel viewModel;
        public OldZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();

            SetLTR();
            viewModel = App.Locator.OldZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;


            viewModel.SuccessMessage = AppResources.VatInstalmentPlanSubmittedSuccess;

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
            catch (Exception )
            {



            }
        }




        private void Instalment_plan_Tapped(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
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

                String downloadurl = Constants.ZOdownloadAckLetter + "'" + viewModel.ZakatReferanceNumber + "')/$value";
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
                String downloadurl = Constants.OldZakatdownloadCoverFormFile + "'" + viewModel.ZakatReferanceNumber + "')/$value";
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
            this.Padding = safeInsets;
        }

    }

}