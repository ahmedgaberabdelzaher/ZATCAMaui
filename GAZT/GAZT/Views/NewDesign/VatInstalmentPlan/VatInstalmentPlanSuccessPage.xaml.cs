using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Instalment_plan_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var _navigation = Application.Current.MainPage.Navigation;

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.VatInstalmentPlanSuccessPage)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                viewModel._navigationService.NavigateTo(App.InstalmentPlanPageView);

                //Application.Current.MainPage.Navigation.PopAsync();

            });

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
                if (item.GetType().Name == App.InstalmentPlanPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
        }

    }

}