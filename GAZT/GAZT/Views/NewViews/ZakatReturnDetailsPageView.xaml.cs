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
    public partial class ZakatReturnDetailsPageView : ContentPage
    {

        #region Variable
        ZakatReturnDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public ZakatReturnDetailsPageView(string fbguid)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnDetailsPageView;
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad(fbguid);
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