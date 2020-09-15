using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatInstalmentPlanSuccessPage : ContentPage
    {
        VATInstalmentPlanViewModel viewModel;
        public VatInstalmentPlanSuccessPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.VatInstalmentPlanPageView;
            this.BindingContext = viewModel;
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
                if (viewModel.VATReferanceNumber != null)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsLoading = true;
                    });

                    await Clipboard.SetTextAsync(viewModel.VATReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);



                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
            }
            catch (Exception ex)
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
            if (viewModel.VATReferanceNumber != null)
            {

                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });

                String downloadurl = Constants.downloadFile + "'" + viewModel.VATReferanceNumber + "')/$value";
                await WebServiceManager.FileDownload(downloadurl, "pdf");

                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;

                });
            }
        }

      

        protected override void OnAppearing()
        {
            base.OnAppearing();

          
        }

    }

}