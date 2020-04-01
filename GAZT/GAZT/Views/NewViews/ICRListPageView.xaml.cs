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
    public partial class ICRListPageView : ContentPage
    {

        #region Variable
        ICRListPageViewModel viewModel;
        int Count = 0;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public ICRListPageView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ICRListPageView;
            this.BindingContext = viewModel;
            SetLTR();
            Count = 1;
            App.ICRStatus = string.Empty;
            viewModel.IsICRListVisible = true;
            viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();

            ICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };



            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }

        #endregion

        #region Method

        public async void IntialiseAsync()
        {
            try
            {
                    await viewModel.onPageLoad();
                    if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0)
                    {
                        BPicker.SelectedIndex = 14;
                    }
            }
            catch(Exception e)
            {

            }
        }

        public async Task IntialiseAsyncForPreviousSelectedFilter()
        {
            try
            {
                await viewModel.onPageLoad();
                if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0 && viewModel.PreviousSelectedICRStatus!=null)
                {
                    //int indexofPreviousSelectedFilter = viewModel.ICRStatusList.FindIndex(x => x.Estat == viewModel.PreviousSelectedICRStatus.Estat);
                    viewModel.SelectedICRStatus = viewModel.PreviousSelectedICRStatus;
                   // BPicker.SelectedIndex = indexofPreviousSelectedFilter;
                }
            }
            catch (Exception e)
            {

            }
        }

        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        #endregion

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                AttachmentPageViewModel.AttachmentUploadedSize = 0;
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
                        IntialiseAsync();
                    }
                }
                Count++;
            }
            catch(Exception ex)
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

        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
    }
}