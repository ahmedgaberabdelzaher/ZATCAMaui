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
    public partial class ICRListPageView : ContentPage
    {

        #region Variable
        ICRListPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public ICRListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ICRListPageView;
            this.BindingContext = viewModel;
            SetLTR();
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