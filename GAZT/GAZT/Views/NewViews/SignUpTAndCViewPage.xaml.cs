using GAZT.ViewModel;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpTAndCViewPage : ContentPage
    {
        SignUpTAndCPageViewModel viewModel;
        public SignUpTAndCViewPage()
        {
            viewModel = App.Locator.SignUpTAndCPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            SetLTR();

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (!App.IsArabic)
                {
                    TCWebView.Source = "TermsAndConditionsEN.html";
                }
                else
                {
                    TCWebView.Source = "TermsAndConditionsAR.html";

                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
                }
                else
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";

                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsButtonEnabled = false;
            viewModel.IschkTAndC = false;
            viewModel.VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
        }
            private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}