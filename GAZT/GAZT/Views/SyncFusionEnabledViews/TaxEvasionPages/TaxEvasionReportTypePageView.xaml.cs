using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportTypePageView : ContentPage
    {
        //FormBundleStatusPageViewModel viewModel;
        TaxEvasionReportTypePageViewModel viewModel;

        public TaxEvasionReportTypePageView(string MobileNumber)
        {
            viewModel = App.Locator.TaxEvasionReportTypePageView;
            InitializeComponent();
            ChangeAeroIcon();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            

            viewModel.TaxEvasionListobj = new TaxEvasionReport();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.CategorySelected_Index = "0";
            viewModel.MobileNumber = MobileNumber;

            SetLTR();
            //viewModel.onPageLoad();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;
            viewModel.CategorySelected_Index = "0";


        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {



        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = true;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = true;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = true;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = false;

        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = true;
            viewModel.IsimgVisiblec5 = false;

        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            viewModel.IsimgVisiblec1 = false;
            viewModel.IsimgVisiblec2 = false;
            viewModel.IsimgVisiblec3 = false;
            viewModel.IsimgVisiblec4 = false;
            viewModel.IsimgVisiblec5 = true;
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