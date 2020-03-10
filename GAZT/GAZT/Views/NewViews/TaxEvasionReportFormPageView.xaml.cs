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
    public partial class TaxEvasionReportFormPageView : ContentPage
    {
        
        TaxEvasionReportFormPageViewModel viewModel;


        public TaxEvasionReportFormPageView()
        {

            viewModel = App.Locator.TaxEvasionReportFormPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            viewModel.onPageLoad();
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            RegionPicker.Focus();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            CityPicker.Focus();
        }
    }
}