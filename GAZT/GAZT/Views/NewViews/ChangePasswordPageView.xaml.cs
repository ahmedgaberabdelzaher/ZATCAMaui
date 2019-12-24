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
    public partial class ChangePasswordPageView : ContentPage
    {

        #region Variable

        ChangePasswordPageViewModel viewModel;
        #endregion

        #region Constructor
        public ChangePasswordPageView()
        {
            viewModel = App.Locator.ChangePasswordPageView;
            InitializeComponent();

           
            
            this.BindingContext = viewModel;
        }
        #endregion

        #region Method

        #endregion


    }
}