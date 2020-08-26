using System;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
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

        public ContractReleaseSuccessPageView()
        {
            InitializeComponent();


            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;
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

        private void ContractNumberCopyTapped(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var _navigation = Application.Current.MainPage.Navigation;

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatInstalmentPlanPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

        }

        private void Back_To_ContractList_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;

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

        private void RefNumberCopyTapped(object sender, EventArgs e)
        {

        }
    }
}