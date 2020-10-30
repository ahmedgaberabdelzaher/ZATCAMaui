using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class ZakatRegistrationFinancialDetails : ContentPage
    {
        ZakatRegistrationFinancialDetailsPageViewModel viewModel;
        public ZakatRegistrationFinancialDetails()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.ZakatRegistrationFinancialDetailsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadDataFinancialDetails();
        }
    }
}
