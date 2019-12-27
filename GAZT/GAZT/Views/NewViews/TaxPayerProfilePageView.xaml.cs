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
    public partial class TaxPayerProfilePageView : ContentPage
    {
        #region Variable
        TaxPayerProfilePageViewModel viewModel;
        #endregion

        #region Constructor
        public TaxPayerProfilePageView()
        {
            viewModel = App.Locator.TaxPayerProfilePageView;
            InitializeComponent();
            
           
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();

        }
        #endregion

        #region Method
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SetTP();
            Task.Delay(20000);
          

            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        #endregion
    }
}