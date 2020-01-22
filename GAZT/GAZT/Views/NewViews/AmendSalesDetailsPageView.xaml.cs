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
    public partial class AmendSalesDetailsPageView : ContentPage
    {
        #region Variable
        AmendSalesDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public AmendSalesDetailsPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AmendSalesDetailsPageView;
            this.BindingContext = viewModel;
            viewModel.OnLoad();
            SetLTR();

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

        private void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            return;
        }
    }
}