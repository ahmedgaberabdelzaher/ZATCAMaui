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
    public partial class ZakatReturnListPageView : ContentPage
    {

        #region Variable
        ZakatReturnListPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public ZakatReturnListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnListPageView;
            SetLTR();
            this.BindingContext = viewModel;
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