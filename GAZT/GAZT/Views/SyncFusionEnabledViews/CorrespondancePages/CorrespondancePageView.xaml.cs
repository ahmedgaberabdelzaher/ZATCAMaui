using EGAZT.ViewModel.SyncFusionEnabledViewModel.CorrespondancePage_ViewModel;
using GAZT.Helper;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.Correspondance
{
    [Preserve(AllMembers = true)]
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
                SetPickerFont();
            }
            catch(Exception ex)
            {
            }
            SetLTR();
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                FPickerZakat.HeaderFontFamily = "GE SS Two";
                                FPickerZakat.ColumnHeaderFontFamily = "GE SS Two";
                                FPickerZakat.SelectedItemFontFamily = "GE SS Two";
                                FPickerZakat.UnSelectedItemFontFamily = "GE SS Two";//FPickerVAT

                                FPickerVAT.HeaderFontFamily = "GE SS Two";
                                FPickerVAT.ColumnHeaderFontFamily = "GE SS Two";
                                FPickerVAT.SelectedItemFontFamily = "GE SS Two";
                                FPickerVAT.UnSelectedItemFontFamily = "GE SS Two";//FPickerET

                                FPickerET.HeaderFontFamily = "GE SS Two";
                                FPickerET.ColumnHeaderFontFamily = "GE SS Two";
                                FPickerET.SelectedItemFontFamily = "GE SS Two";
                                FPickerET.UnSelectedItemFontFamily = "GE SS Two";
                            }
                            else
                            {
                                FPickerZakat.HeaderFontFamily = "SSTArabic-Medium";
                                FPickerZakat.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                FPickerZakat.SelectedItemFontFamily = "SSTArabic-Medium";
                                FPickerZakat.UnSelectedItemFontFamily = "SSTArabic-Medium";

                                FPickerVAT.HeaderFontFamily = "SSTArabic-Medium";
                                FPickerVAT.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                FPickerVAT.SelectedItemFontFamily = "SSTArabic-Medium";
                                FPickerVAT.UnSelectedItemFontFamily = "SSTArabic-Medium";

                                FPickerET.HeaderFontFamily = "SSTArabic-Medium";
                                FPickerET.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                FPickerET.SelectedItemFontFamily = "SSTArabic-Medium";
                                FPickerET.UnSelectedItemFontFamily = "SSTArabic-Medium";


                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        FPickerZakat.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerZakat.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerZakat.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerZakat.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium"; FPickerVAT

                        FPickerVAT.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerVAT.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerVAT.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerVAT.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";//FPickerET

                        FPickerET.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerET.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerET.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        FPickerET.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";//FPickerET


                        break;
                }
            }
            catch (Exception ex)
            {

            }

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

            try
            {
                await viewModel.onPageLoad();
                viewModel.SetData();
                viewModel.SelectedFilterZakat = viewModel.SelectedFilterZakatPrev;
                viewModel.SelectedFilterVAT = viewModel.SelectedFilterVATPrev;
                viewModel.SelectedFilterET = viewModel.SelectedFilterETPrev;


            }
            catch (Exception ex)
            {

            }
        }
        //private void ZakatActions_Clicked(object sender, EventArgs e)
        //{
        //    var KeywordItem = ((MenuItem)sender).CommandParameter as CorrespondanceModel;
        //}
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
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
            try
            {
                FPickerZakat.SelectedItem = viewModel.SelectedFilterZakatPrev;
                viewModel.SelectedFilterZakat = viewModel.SelectedFilterZakatPrev;//selectedregion
                if (viewModel.SelectedFilterZakatPrev == null)
                {
                    viewModel.TxtSelectedStatusZakat = string.Empty;
                }
            }
            catch(Exception ex)
            {

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
        private void tabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
        }
    }
}