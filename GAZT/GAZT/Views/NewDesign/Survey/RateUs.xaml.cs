using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.SurveyViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Survey
{
    public partial class RateUs : BaseContentPage
    {
        RateUsViewModel viewModel;
        public RateUs()
        {
            viewModel = App.Locator.rateUsViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }

        void WebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("https://zatca.gov.sa/") || e.Url.Contains("http://gazt.gov.sa/"))
            {
                Navigation.PopAsync();
            }
        }
    }
}
