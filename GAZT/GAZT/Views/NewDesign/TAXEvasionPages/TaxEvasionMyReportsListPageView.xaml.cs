using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    public partial class TaxEvasionMyReportsListPageView : ContentPage
    {
        TaxEvasionMyReportsListPageViewModel viewModel;
        public TaxEvasionMyReportsListPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionMyReportsListPageView;
            this.BindingContext = viewModel;
          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
    }
}
