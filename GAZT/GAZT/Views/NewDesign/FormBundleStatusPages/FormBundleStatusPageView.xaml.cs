using EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel;
using GAZT.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

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
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                BundleType.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                BundleNumber.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                BundleType.BackgroundColor =  (Color)Application.Current.Resources["White"];
                BundleNumber.BackgroundColor =  (Color)Application.Current.Resources["White"];
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
                            

                            BundleType.HeaderFontFamily = "Somar-SemiBold";
                            BundleType.ColumnHeaderFontFamily = "Somar-SemiBold";
                            BundleType.SelectedItemFontFamily = "Somar-SemiBold";
                            BundleType.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        //BundleNumber.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        //BundleNumber.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        //BundleNumber.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        //BundleNumber.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        BundleType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        BundleType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        BundleType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        BundleType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception )
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
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.onPageLoad();
            });
            await Task.Run(() =>
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
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
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
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
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
            try { 
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
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