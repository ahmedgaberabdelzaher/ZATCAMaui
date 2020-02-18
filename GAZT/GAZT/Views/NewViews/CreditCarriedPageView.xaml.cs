using GAZT.Models;
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
    public partial class CreditCarriedPageView : ContentPage
    {
        CreditCarriedPageViewModel viewModel;
        public CreditCarriedPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.CreditCarriedPageView;
                this.BindingContext = viewModel;
                if(vATDeclaration.d!=null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                }
                if (viewModel.VATDeclarationData.d!=null && viewModel.VATDeclarationData.d.CFSet.results != null && viewModel.VATDeclarationData.d.ADRSet.results.Count != 0)
                {
                   if(viewModel.VATDeclarationData.d.CFSet.results.Count()!=0)
                    {
                        viewModel.CreditCarriedsList = viewModel.VATDeclarationData.d.CFSet.results;
                        viewModel.IsListViewVisible = true;
                        viewModel.IsNoDataLabelVisible = false;
                    }
                    else
                    {
                        viewModel.IsListViewVisible = false;
                        viewModel.IsNoDataLabelVisible = true;
                    }
                }
               
            }
            catch(Exception e)
            {

            }
        }
    }
}