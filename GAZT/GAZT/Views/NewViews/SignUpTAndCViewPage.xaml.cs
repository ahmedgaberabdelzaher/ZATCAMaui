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
}