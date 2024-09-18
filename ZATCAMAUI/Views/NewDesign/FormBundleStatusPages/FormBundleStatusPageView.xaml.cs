
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
            SetPickerFont();
            OnPageLoad();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Platform == DevicePlatform.Android)
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
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            BundleType.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            BundleType.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:

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

        private void BundleNumber_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                FormBundleApplicationNumberModelResult selectedfbnum = viewModel.FormBundleApplicatioNumberList[e.NewValue];
                viewModel.SelectedFormBindleFbnum = selectedfbnum;
                viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
                viewModel.TxtFBnum = selectedfbnum.Fbnum;
                viewModel.populate();
            }
            catch (Exception)
            {


            }
        }

        private void OnBundleNumberClicked(object sender, EventArgs e)
        {
            BundleNumber.IsOpen = true;
            if (viewModel.FormBundleApplicatioNumberList.Count > 0 && string.IsNullOrWhiteSpace(viewModel.TxtFBnum))
            {
                var selectedfbnum = viewModel.FormBundleApplicatioNumberList[0];
                viewModel.SelectedFormBindleFbnum = selectedfbnum;
                viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
                viewModel.TxtFBnum = selectedfbnum.Fbnum;
                viewModel.populate();
            }
                
        }
        private void OnBundleTypeClicked(object sender, EventArgs e)
        {
            BundleType.IsOpen = true;
            if (viewModel.FormBundleList.Count > 0 && string.IsNullOrWhiteSpace(viewModel.TxtFBtype))
            {
                var selectedfbtyp = viewModel.FormBundleList[0];
                viewModel.SelectedFormBindleFbnumPrev = null;
                viewModel.SelectedFormBindleFbtyp = selectedfbtyp;
                viewModel.TxtFBtype = selectedfbtyp.Txt50;
            }
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


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
        }
    }
}