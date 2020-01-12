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
        ChangeMobileNumberPageViewModel viewModel;
        #endregion
        #region Constructor
        public ChangeMobileNumberPageView()
        {
            viewModel = App.Locator.ChangeMobileNumberPageView;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            SetLTR();
            this.BindingContext = viewModel;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Task.Delay(20000);
            viewModel.NewMobile = "5";
            

          

        }
        #endregion
    }
}