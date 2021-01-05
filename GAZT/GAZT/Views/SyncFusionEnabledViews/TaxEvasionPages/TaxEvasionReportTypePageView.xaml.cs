using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage_ViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportType
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportTypePageView : ContentPage
    {
        //FormBundleStatusPageViewModel viewModel;
        TaxEvasionReportTypePageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public TaxEvasionReportTypePageView(string mobileNumber)
        {
            viewModel = App.Locator.TaxEvasionReportTypePageView;
            InitializeComponent();
            ChangeAeroIcon();
            //MainLayout.Padding = new Thickness(0, 0, 0, 0);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.TaxEvasionListobj = new TaxEvasionReportDetails();
            
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.CategorySelected_Index = "0";
            viewModel.MobileNumber = mobileNumber;
            SetLTR();
            viewModel.IsnextbuttonEnable = false;
            //viewModel.onPageLoad();
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
                        //MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        //MainLayout.Padding = new Thickness(0, 0, 0, 0);
                    }
                }
                //reconfigure layout
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.CategorySelected_Index = "0";
            viewModel.NextbuttonDisableColor= Color.FromHex("#9EA4A9");
            viewModel.IsnextbuttonEnable = false;
            await viewModel.OnPageLoad();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.IsLoading = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (!viewModel.IsLoading)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                Navigation.RemovePage(pg);
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

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var index = (ReportTypesListView.ItemsSource as ObservableCollection<TaxEvasionCategoriesDataModel>).IndexOf(e.SelectedItem as TaxEvasionCategoriesDataModel);
            var itemselected = viewModel.ReportTypes[index];

            foreach (var c in viewModel.ReportTypes)
            {
                c.IsTypeSelected = false;
            }

            if (!itemselected.IsTypeSelected)
            {
                itemselected.IsTypeSelected = true;
            }

            ReportTypesListView.ItemsSource = null;
            ReportTypesListView.ItemsSource = viewModel.ReportTypes;

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsnextbuttonEnable = true;
                viewModel.NextbuttonDisableColor = Color.FromHex("#005e4b");
            });

            //var selectedcolors = viewModel.ReportTypes.Where(p => p.IsTypeSelected == true);
        }
    }
}