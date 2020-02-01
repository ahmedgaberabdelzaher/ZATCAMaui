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
    public partial class SalesDetailsPageView : ContentPage
    {

        #region Variable
        SalesDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public SalesDetailsPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.SalesDetailsPageView;
            viewModel.zakatReturnDetailsD = ZakatReturnDetail;
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.onPageLoad();
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            SalesDetails.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is ListView lv) lv.SelectedItem = null;
            };
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