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
    public partial class OTPPageView : ContentPage
    {

        #region Variable
        OTPPageViewModel viewModel;
        #endregion

        #region Constructor
        public OTPPageView()
        {
            viewModel = App.Locator.OTPPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
        }
        #endregion

        #region Method
        #endregion

    }
}