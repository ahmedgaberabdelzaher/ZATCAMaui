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
           //     ImageBackArrow.Rotation = 0;

                Image_backArrow.Rotation = 0;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
              //  viewModel.RotationForImageInArabic = 180;
                Image_backArrow.Rotation = 180;
             //   ImageBackArrow.Rotation = 180;

            }
        }
        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                    Resources["ImageReverse"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                    Resources["ImageReverse"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch(Exception ex)
            { 
            
            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
         
            try 
            {
                 await  Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await viewModel.onPageLoad();
                viewModel.SetData();
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
                //  viewModel.SetAllCorrespondancedata();
                ;            }
            catch (Exception ex)
            {
                viewModel.IsLoading = true;
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

        private void ListView_Correspondance_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            viewModel.ShowCorrespondenceDetails(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }
      
        
    }
}