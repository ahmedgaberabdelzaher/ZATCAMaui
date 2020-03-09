using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAZT.ViewModel.NewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportTypePageView : ContentPage
    {
        //FormBundleStatusPageViewModel viewModel;
        TaxEvasionReportTypePageViewModel viewModel;

        public TaxEvasionReportTypePageView()
        {
            viewModel = App.Locator.TaxEvasionReportTypePageView;
            InitializeComponent();
            this.BindingContext = viewModel;

            //viewModel.onPageLoad();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {



        }
    }
}