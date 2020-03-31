using GAZT.Helper;
using GAZT.Models;
using GAZT.ViewModel;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CorrespondancePageView : ContentPage
    {
        CorrespondancePageViewModel viewModel;
        public CorrespondancePageView()
        {
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
         
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            try
            {
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.CorrespondancePageView;
                this.BindingContext = viewModel;
                //  viewModel.onPageLoad();
            }
            catch(Exception ex)
            {

            }
            SetLTR();
        }

        private void ClickGestureRecognizer_ClickedForZakat(object sender, EventArgs e)
        {

            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            viewModel.OnZAKATClicked();
        }

        private void ClickGestureRecognizer_ClickedForVAT(object sender, EventArgs e)
        {
            
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            viewModel.OnVATLabelClicked();
        }
        private void ClickGestureRecognizer_ClickedForET(object sender, EventArgs e)
        {
            
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            viewModel.OnEtLabelClicked();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            viewModel.ShowCorrespondenceDetails(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            FPicker.Focus();
        }

        private void ListView_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            viewModel.ShowVATPDF(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private void ListView_ItemTapped_2(object sender, ItemTappedEventArgs e)
        {
            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            string Url = Constants.GAZTGetCorrespondenceAttach + "'" + Correspondence.Cokey + "',Cotyp='" + Correspondence.Cotype + "')/$value";
            viewModel.ShowETPDF(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            //Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];

            //Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
           

         
          await  viewModel.onPageLoad();
            viewModel.SetData();



        }
        //private void ZakatActions_Clicked(object sender, EventArgs e)
        //{
        //    var KeywordItem = ((MenuItem)sender).CommandParameter as CorrespondanceModel;



        //}
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void BPickerButton_Clicked(object sender, EventArgs e)
        {
            FPicker.IsOpen = true;
        }

        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
    }
}