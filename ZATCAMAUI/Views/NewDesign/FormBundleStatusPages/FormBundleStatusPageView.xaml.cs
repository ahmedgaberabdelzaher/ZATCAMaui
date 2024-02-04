
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.NewDesign.FormBundleStatusPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormBundleStatusPageView : ContentPage
    {
        FormBundleStatusPageViewModel viewModel;
        public FormBundleStatusPageView()
        {
            viewModel = App.Locator.FormBundleStatusPageView;
            InitializeComponent();
            BindingContext = viewModel;
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
            Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                BundleType.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                BundleNumber.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                BundleType.BackgroundColor = (Color)Application.Current.Resources["White"];
                BundleNumber.BackgroundColor = (Color)Application.Current.Resources["White"];
            }
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            BundleType.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Device.Android:

                        BundleType.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BundleType.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BundleType.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BundleType.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//ddlLIssuedBy

                        break;
                }
            }
            catch (Exception)
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

                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                FlowDirection = FlowDirection.RightToLeft;

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
        private void BundleType_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                FormBundleResult selectedfbtyp = viewModel.FormBundleList[e.NewValue];
                //BundleType.SelectedItem = selectedfbtyp;
                viewModel.SelectedFormBindleFbnumPrev = null;
                viewModel.SelectedFormBindleFbtyp = selectedfbtyp;
                viewModel.TxtFBtype = selectedfbtyp.Txt50;

            }
            catch (Exception)
            {


            }
        }

        private void BundleType_OkButtonClicked(object sender, EventArgs e)
        {

        }

        private void BundleNumber_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                FormBundleApplicationNumberModelResult selectedfbnum = viewModel.FormBundleApplicatioNumberList[e.NewValue];
                //BundleNumber.SelectedItem = selectedfbnum;//Fbnum
                viewModel.SelectedFormBindleFbnum = selectedfbnum;
                viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
                viewModel.TxtFBnum = selectedfbnum.Fbnum;
                //var item = sender as Picker;
                //var selectedItem = item.SelectedItem as FormBundleApplicationNumberModelResult;
                viewModel.populate();
            }
            catch (Exception)
            {


            }
        }

        private void BundleNumber_OkButtonClicked(object sender, EventArgs e)
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
            try
            {
                var listView = (ListView)sender;
                listView.SelectedItem = null;
            }
            catch (Exception)
            {


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