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
            viewModel.OnPageLoad();

            ZakatICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
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
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();

        }

        #endregion

        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            return;
        }
    }
}