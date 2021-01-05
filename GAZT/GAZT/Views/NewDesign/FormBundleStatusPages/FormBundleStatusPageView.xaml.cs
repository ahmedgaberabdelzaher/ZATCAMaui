using EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.FormBundleStatusPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormBundleStatusPageView : ContentPage
    {
        FormBundleStatusPageViewModel viewModel;
        public FormBundleStatusPageView()
        {
            viewModel = App.Locator.FormBundleStatusPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            viewModel.ClearData();
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            OnPageLoad();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                BundleType.BackgroundColor = Color.FromHex("#f7f7f7");
                BundleNumber.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                BundleType.BackgroundColor = Color.FromHex("#FFFFFF");
                BundleNumber.BackgroundColor = Color.FromHex("#FFFFFF");
            }
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            //BundleNumber.HeaderFontFamily = "SSTArabic-Medium";
                            //BundleNumber.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            //BundleNumber.SelectedItemFontFamily = "SSTArabic-Medium";
                            //BundleNumber.UnSelectedItemFontFamily = "SSTArabic-Medium";


                            BundleType.HeaderFontFamily = "SSTArabic-Medium";
                            BundleType.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            BundleType.SelectedItemFontFamily = "SSTArabic-Medium";
                            BundleType.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        //BundleNumber.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        //BundleNumber.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        //BundleNumber.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        //BundleNumber.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        BundleType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BundleType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BundleType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        BundleType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        public void SetLTR()
        {
            if (!App.IsArabic)
            {

                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                this.FlowDirection = FlowDirection.RightToLeft;

            }
        }

        public async void OnPageLoad()
        {
            Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.onPageLoad();
            });
            Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        private void BundleType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                FormBundleResult selectedfbtyp = (FormBundleResult)e.NewValue;
                BundleType.SelectedItem = selectedfbtyp;
                viewModel.SelectedFormBindleFbnumPrev = null;
                viewModel.SelectedFormBindleFbtyp = selectedfbtyp;
                viewModel.TxtFBtype = selectedfbtyp.Txt50;

            }
            catch (Exception ex)
            {

            }
        }

        private void BundleType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void BundleNumber_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

                FormBundleApplicationNumberModelResult selectedfbnum = (FormBundleApplicationNumberModelResult)e.NewValue;
                BundleNumber.SelectedItem = selectedfbnum;//Fbnum
                viewModel.SelectedFormBindleFbnum = selectedfbnum;
                viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
                viewModel.TxtFBnum = selectedfbnum.Fbnum;
                //var item = sender as Picker;
                //var selectedItem = item.SelectedItem as FormBundleApplicationNumberModelResult;
                viewModel.populate();
            }
            catch (Exception ex)
            {

            }
        }

        private void BundleNumber_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnBundleNumberClicked(object sender, EventArgs e)
        {
            BundleNumber.IsOpen = true;
        }
        private void OnBundleTypeClicked(object sender, EventArgs e)
        {
            BundleType.IsOpen = true;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private void BundleType_Closed(object sender, EventArgs e)
        {
            
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
        }
    }
}