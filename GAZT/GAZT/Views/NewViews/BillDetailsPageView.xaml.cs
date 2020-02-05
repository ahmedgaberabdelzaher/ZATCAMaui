using GAZT.Models;
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
    public partial class BillDetailsPageView : ContentPage
    {

        #region Variable
        BillDetailsPageViewModel viewModel;
        #endregion

       

        #region Constructor
        public BillDetailsPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.BillDetailsPageView;
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            SetLTR();
            viewModel.zakatReturnDetailsD = ZakatReturnDetail;
            viewModel.OnPageLoad();
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