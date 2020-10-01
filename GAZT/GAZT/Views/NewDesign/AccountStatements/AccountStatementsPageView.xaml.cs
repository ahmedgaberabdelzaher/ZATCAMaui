using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    public partial class AccountStatementsPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;

        public AccountStatementsPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementsPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
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

        private void btn_Clicked(object sender, System.EventArgs e)
        {
            TaxTypePicker.IsOpen = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PopulateASFilterData();
        }
    }
}
