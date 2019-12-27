using GAZT.Models;
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
        public ChangePasswordPageView(NavigateToOtp navigateTo)
        {
            viewModel = App.Locator.ChangePasswordPageView;
            InitializeComponent();
            SetLTR();
            this.BindingContext = viewModel;           
            viewModel.NavigateToOtpForEmailEnum = navigateTo;
            viewModel.OnPageLoad();

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