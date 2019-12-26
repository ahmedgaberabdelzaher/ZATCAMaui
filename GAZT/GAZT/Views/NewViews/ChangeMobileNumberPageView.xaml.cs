using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobileNumberPageView : ContentPage
    {
        #region Variable
        private ChangeMobileNumberPageViewModel viewModel;
        #endregion
        #region Constructor
        public ChangeMobileNumberPageView()
        {
            viewModel = App.Locator.ChangeMobileNumberPageView;
            InitializeComponent();

           
            this.BindingContext = viewModel;
          
        }
        #endregion

        #region Method
       
        #endregion
    }
}