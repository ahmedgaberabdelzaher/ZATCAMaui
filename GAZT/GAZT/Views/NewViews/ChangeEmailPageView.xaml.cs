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
    public partial class ChangeEmailPageView : ContentPage
    {
        #region Variable
        ChangeEmailPageViewModel viewModel;
        
        #endregion

        #region Constructor
        public ChangeEmailPageView()
        {
            viewModel = App.Locator.ChangeEmailPageView;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
            NavigationPage.SetBackButtonTitle(this, "");
        }
        #endregion

        #region Method
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion
    }
}