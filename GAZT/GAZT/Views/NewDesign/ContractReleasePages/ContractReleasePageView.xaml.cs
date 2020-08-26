using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ContractReleaseViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ContractRelease
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleasePageView : ContentPage
    {
        ContractReleaseViewModel viewModel;
        
        public ContractReleasePageView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            //App.IsArabic = false;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;

            Task.Run(async () =>
            {
                viewModel.IsLoading = true;
                await GetContractReleaseData();

            });
        }

        public async Task GetContractReleaseData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.OnPageLoad();

                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception ex)
            {

            }
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
    }
}