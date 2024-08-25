
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AttachmentPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ICRListPage;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ICRListPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ICRListPageView : ContentPage
    {
        #region Variable
        ICRListPageViewModel viewModel;
        public static bool AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
        int Count = 0;
        private double width = 0;
        private double height = 0;
        #endregion

        #region Constructor
        public ICRListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ICRListPageView;
            BindingContext = viewModel;
            SetPickerFont();
            Count = 1;
            FrmLicenseIssuedBy.Margin = new Thickness(5, 0, 5, 5);
            App.ICRStatus = string.Empty;
            viewModel.IsICRListVisible = true;
            viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();
            AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
            ICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            };
            //BPicker
        }
        #endregion
        #region Method
      
      
        public async Task IntialiseAsync()
        {
            try
            {
                await viewModel.onPageLoad();
                if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0)
                {
                    BPicker.Columns[0].SelectedIndex = 14;
                }
            }
            catch (Exception)
            {
            }
        }
        private void SelectedICR(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage)
                {
                    ICRStatus selectedICR = viewModel.ICRStatusList[e.NewValue];
                    viewModel.SelectedICRStatus = viewModel.ICRStatusList[viewModel.SelectedPickerIndex];// selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(viewModel.PreviousSelectedICRStatus);
                    viewModel.ICRSelectedIndex = viewModel.SelectedPickerIndex;
                }
                else
                {
                    ICRStatus selectedICR = viewModel.ICRStatusList[e.NewValue];
                    viewModel.SelectedICRStatus = selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(selectedICR);
                }
            }
            catch (Exception)
            {
            }
        }
        public async Task IntialiseAsyncForPreviousSelectedFilter()
        {
            try
            {
                await viewModel.onPageLoad();
                if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0 && viewModel.PreviousSelectedICRStatus != null)
                {
                    if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false)
                    {
                        viewModel.SelectedICRStatus = viewModel.PreviousSelectedICRStatus;
                    }
                    else if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = true)
                    {
                        AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
       
        #endregion
        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            BPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            BPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            BPicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        {
                            BPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            BPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }

            }
            catch (Exception)
            {


            }

        }



        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                AttachmentPageViewModel.AttachmentUploadedSize = 0;
                AttachmentPageViewModel.IsToBeFilled = true;
                AttachmentPageViewModel.attachmentSizeVisibility = false;
                if (Count != 1)
                {
                    if (!string.IsNullOrEmpty(App.ICRStatus))
                    {
                        if (viewModel.PreviousSelectedICRStatus != null)
                        {
                            await IntialiseAsyncForPreviousSelectedFilter();
                        }
                    }
                    else
                    {
                        await IntialiseAsync();
                    }
                }
                Count++;
            }
            catch (Exception)
            {
            }
        }
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
        }
        private void BPickerButton_Clicked(object sender, EventArgs e)
        {
            BPicker.IsOpen = true;
        }
        private void BPicker_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {
                ICRStatus selectedfbtyp = viewModel.ICRStatusList[BPicker.Columns[0].SelectedIndex];
                //BPicker.SelectedItem = selectedfbtyp;
                viewModel.SelectedICRStatus = selectedfbtyp;
                viewModel.SelectedICRStatusPrev = selectedfbtyp;
                viewModel.TxtSelectedStatus = selectedfbtyp.Txt30;
            }
            catch (Exception)
            {

            }

        }
        private void BPicker_CancelButtonClicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.SelectedICRStatus = viewModel.SelectedICRStatusPrev;
                //BPicker.SelectedItem = viewModel.SelectedICRStatusPrev;
                if (viewModel.SelectedICRStatusPrev == null)
                {
                    viewModel.TxtSelectedStatus = string.Empty;
                }
            }
            catch (Exception)
            {

            }

        }
    }
}