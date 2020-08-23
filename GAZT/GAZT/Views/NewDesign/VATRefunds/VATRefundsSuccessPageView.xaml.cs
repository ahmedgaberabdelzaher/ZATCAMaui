using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    public partial class VATRefundsSuccessPageView : ContentPage
    {
        VATRefundsSuccessPageViewModel viewModel;
        public VATRefundsSuccessPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsSuccessPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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

        void btnGoToDasboard_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                for (var counter = 1; counter < 5; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            });
        }

        void btnRefunds_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                for (var counter = 1; counter < 3; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                }
                await Navigation.PopAsync();
            });
        }
    }
}
