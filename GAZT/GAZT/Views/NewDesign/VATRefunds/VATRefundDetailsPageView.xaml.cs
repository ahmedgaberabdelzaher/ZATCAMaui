using System;
using System.Collections.Generic;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    public partial class VATRefundDetailsPageView : ContentPage
    {
        VATRefundDetailsPageViewModel viewModel;
        public VATRefundDetailsPageView(VATRefundsModel vATRefundsModel)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundDetailsPageView;
            viewModel.ReloadData(vATRefundsModel);
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }
        private void SetLTR()
        {
            //if (App.IsArabic)
            //{
                this.FlowDirection = FlowDirection.LeftToRight;
            //}
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

        void ConfirmButton_Tapped(object sender, EventArgs e)
        {
            try
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsSuccessPageView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
