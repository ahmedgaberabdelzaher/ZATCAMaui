using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerCorrespondancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondancePageView : ContentPage
    {
        TaxpayerCorrespondancePageViewModel viewModel;
        public TaxpayerCorrespondancePageView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondancePageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.PopulateFilterDropdownList();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
         
            try
            {
                await viewModel.onPageLoad();
                viewModel.SetData();
              //  viewModel.SetAllCorrespondancedata();
  ;            }
            catch (Exception ex)
            {

            }
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            CorrespondanceDownPicker.IsOpen = true;
        }

        private void CorrespondanceDownPicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ReturnTypes selectedReturntype = (ReturnTypes)e.NewValue;
                CorrespondanceDownPicker.SelectedItem = selectedReturntype;//Fbnum
                viewModel.SelectedDropdownItem = selectedReturntype;

            }
            catch (Exception ex)
            {

            }
        }
    }
}