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
        int Count = 0;
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
            Count = 1;
            IntialiseAsync();

            ICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is ListView lv) lv.SelectedItem = null;
            };


        }

        #endregion

        #region Method

        public async void IntialiseAsync()
        {
            try
            {
                await viewModel.onPageLoad();
            }
            catch(Exception e)
            {

            }
        }

        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        #endregion

        protected virtual void OnAppearing()
        {
            if (Count != 1)
            {
                IntialiseAsync();
            }
            Count++;
        }

        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
           
        }
    }
}