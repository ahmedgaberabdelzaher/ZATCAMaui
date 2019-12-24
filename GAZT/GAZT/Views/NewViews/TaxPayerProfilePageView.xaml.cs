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
    public partial class TaxPayerProfilePageView : ContentPage
    {
        #region Variable
        TaxPayerProfilePageViewModel viewModel;
        #endregion

        #region Constructor
        public TaxPayerProfilePageView()
        {
            viewModel = App.Locator.TaxPayerProfilePageView;
            InitializeComponent();
            
           
            this.BindingContext = viewModel;
            
        }
        #endregion

        #region Method

       
        #endregion
    }
}