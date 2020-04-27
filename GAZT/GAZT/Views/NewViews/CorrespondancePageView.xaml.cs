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
        private double width = 0;
        private double height = 0;
        public CorrespondancePageView()
        {
            Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
         
            Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            try
            {
                InitializeComponent();
                ParentGridZakat.Margin = new Thickness(0, 0, 0, 5);

                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.CorrespondancePageView;
                this.BindingContext = viewModel;
                ChangeAeroIcon();
                //  viewModel.onPageLoad();
            }
            catch(Exception ex)
            {

            }
            SetLTR();
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        ParentGridZakat.Margin = new Thickness(40, 0, 40, 5);

                        //ICRList.Margin = new Thickness(0, 5, 0, 0);
                        //BPicker.Margin = new Thickness(10, 0, 10, 0);
                        //FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
                        //ListLayout.Padding = new Thickness(40, 0, 40, 5);

                        // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        // var safeInsets = On<iOS>().SafeAreaInsets();
                        //ICRList.Margin = new Thickness(0, 5, 60, 0);
                        // BPicker.Margin = new Thickness(20, 0, 60, 0);
                        // FrmLicenseIssuedBy.Margin = new Thickness(20, 0, 80, 5);


                        //safeInsets.Left = 80;
                        //safeInsets.Right = 80;
                        //Padding = safeInsets;
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ParentGridZakat.Margin = new Thickness(0, 0, 0, 5);
                        //ICRList.Margin = new Thickness(0, 5, 0, 0);
                        //BPicker.Margin = new Thickness(10, 0, 10, 0);
                        //FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
                        //ListLayout.Padding = new Thickness(10, 0, 10, 5);

                    }
                }

                //reconfigure layout
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                viewModel.LabelHzAlignment = TextAlignment.End;


            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                viewModel.LabelHzAlignment = TextAlignment.Start;

            }
        }
        //private void ClickGestureRecognizer_ClickedForZakat(object sender, EventArgs e)
        //{

        //    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        //    viewModel.OnZAKATClicked();
        //}

        //private void ClickGestureRecognizer_ClickedForVAT(object sender, EventArgs e)
        //{

        //    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        //    viewModel.OnVATLabelClicked();
        //}
        //private void ClickGestureRecognizer_ClickedForET(object sender, EventArgs e)
        //{

        //    Resources["searchBarStyleForZakat"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForVAT"] = App.Current.Resources["MyBillsSmallMiniWhiteLabelStyle"];
        //    Resources["searchBarStyleForET"] = App.Current.Resources["MyBillsMediumMiniWhiteLabelStyle"];
        //    viewModel.OnEtLabelClicked();
        //}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            CorrespondanceModel Correspondence = ((Xamarin.Forms.ListView)sender).SelectedItem as CorrespondanceModel;
            viewModel.ShowCorrespondenceDetails(Correspondence);
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    FPicker.Focus();
        //}

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

        //private void BPickerButton_Clicked(object sender, EventArgs e)
        //{
        //    FPicker.IsOpen = true;
        //}

        //private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    FPicker.IsOpen = true;
        //}

        private void BPickerButtonZakat_Clicked(object sender, EventArgs e)
        {
            FPickerZakat.IsOpen = true;
        }

        private void BPickerButtonVAT_Clicked(object sender, EventArgs e)
        {
            FPickerVAT.IsOpen = true;
        }

        private void BPickerButtonET_Clicked(object sender, EventArgs e)
        {
            FPickerET.IsOpen = true;
        }

        private void FPickerZakat_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            CorrespondenceFiltersModel selectedZakat = (CorrespondenceFiltersModel)e.NewValue;
            FPickerZakat.SelectedItem = selectedZakat;
            viewModel.SelectedFilterZakat = selectedZakat;//selectedregion
            viewModel.SelectedFilterZakatPrev = selectedZakat;//selectedregion
            viewModel.TxtSelectedStatusZakat = selectedZakat.Filter;
        }

        private void FPickerZakat_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FPickerZakat.SelectedItem = viewModel.SelectedFilterZakatPrev;
            viewModel.SelectedFilterZakat = viewModel.SelectedFilterZakatPrev;//selectedregion
            if (viewModel.SelectedFilterZakatPrev == null)
            {
                viewModel.TxtSelectedStatusZakat = string.Empty;
            }
        }

        private void FPickerVAT_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            CorrespondenceFiltersModel selectedVAT = (CorrespondenceFiltersModel)e.NewValue;
            FPickerVAT.SelectedItem = selectedVAT;
            viewModel.SelectedFilterVAT = selectedVAT;//selectedregion
            viewModel.SelectedFilterVATPrev = selectedVAT;//selectedregion
            viewModel.TxtSelectedStatusVAT = selectedVAT.Filter;
        }

        private void FPickerVAT_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FPickerVAT.SelectedItem = viewModel.SelectedFilterVATPrev;
            viewModel.SelectedFilterVAT = viewModel.SelectedFilterVATPrev;//selectedregion
            if (viewModel.SelectedFilterVATPrev == null)
            {
                viewModel.TxtSelectedStatusVAT = string.Empty;
            }
        }

        private void FPickerET_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            CorrespondenceFiltersModel selectedET = (CorrespondenceFiltersModel)e.NewValue;
            FPickerET.SelectedItem = selectedET;
            viewModel.SelectedFilterET = selectedET;//selectedregion
            viewModel.SelectedFilterETPrev = selectedET;//selectedregion
            viewModel.TxtSelectedStatusET = selectedET.Filter;
        }

        private void FPickerET_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FPickerET.SelectedItem = viewModel.SelectedFilterETPrev;
            viewModel.SelectedFilterET = viewModel.SelectedFilterETPrev;//selectedregion
            if (viewModel.SelectedFilterETPrev == null)
            {
                viewModel.TxtSelectedStatusET = string.Empty;
            }
        }
    }
}