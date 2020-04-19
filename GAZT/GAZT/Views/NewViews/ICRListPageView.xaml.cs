using GAZT.Models;
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
        public static bool AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
        int Count = 0;
        private double width = 0;
        private double height = 0;
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
            ChangeAeroIcon();
            SetLTR();
            Count = 1;
            App.ICRStatus = string.Empty;
            viewModel.IsICRListVisible = true;
            viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();
            AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
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



        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if(App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
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
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        public async Task IntialiseAsync()
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

        private void SelectedICR(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage)
                {
                    ICRStatus selectedICR = (ICRStatus)e.NewValue;
                    viewModel.SelectedICRStatus = selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(viewModel.PreviousSelectedICRStatus);
                }
                else
                {
                    ICRStatus selectedICR = (ICRStatus)e.NewValue;
                    viewModel.SelectedICRStatus = selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(selectedICR);
                }
            }
            catch(Exception ex)
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
                    if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false)
                    {
                        viewModel.SelectedICRStatus = viewModel.PreviousSelectedICRStatus;
                    }
                    if(AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = true)
                    {
                        AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
                    }
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
                       await IntialiseAsync();
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