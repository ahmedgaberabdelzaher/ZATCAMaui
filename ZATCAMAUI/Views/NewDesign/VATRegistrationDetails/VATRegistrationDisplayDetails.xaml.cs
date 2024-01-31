using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATRegistrationDetails
{

    public partial class VATRegistrationDisplayDetails : ContentPage
    {
        VATRegistrationDisplayDetailsPageViewModel viewModel;
        public VATRegistrationDisplayDetails()
        {
            InitializeComponent();
            ChangeAeroIcon();
            viewModel = App.Locator.VATRegistrationDisplayDetails;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            SetLTR();
            Task.Run(async () =>
            {
                //viewModel.IsLoading = true;
                await GetVatRegistrationData();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public async Task GetVatRegistrationData()
        {
            try
            {
                await Task.Run(() =>
                {
                    //viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.onPageLoad();
                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception)
            {


            }
        }
    }
}
