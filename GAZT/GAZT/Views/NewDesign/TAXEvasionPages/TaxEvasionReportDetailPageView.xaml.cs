using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportDetailPageView : ContentPage
    {
        TaxEvasionReportDetailPageViewModel viewModel;
        public TaxEvasionReportDetailPageView(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionReportDetailPageView;
            this.BindingContext = viewModel;
            viewModel.SelectedTaxEvasionListItem = SelectedTaxEvasionListItem;
        }
    }
}