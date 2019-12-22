using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPageView : ContentPage
    {
        DashboardPageViewModel viewModel;

        #region Constructor
        public DashboardPageView()
        {
            viewModel = App.Locator.DashboardPageView;
            InitializeComponent();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.onPageLoad();
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