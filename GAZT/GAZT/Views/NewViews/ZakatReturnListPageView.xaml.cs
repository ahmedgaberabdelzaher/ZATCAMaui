using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnListPageView : ContentPage
    {

        #region Variable
        ZakatReturnListPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public static bool AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = false;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public ZakatReturnListPageView()
        {
            InitializeComponent();
                     On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
             viewModel = App.Locator.ZakatReturnListPageView;
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.GetZAKATICRStatusList();
            viewModel.SelectedIndex = 13;
            viewModel.TxtSelectedStatus = viewModel.ICRStatusList[13].Value;
            viewModel.HandleNoDataMessageVisibility(viewModel.MyZakatReturns);
            ZakatICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
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
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        var safeInsets = On<iOS>().SafeAreaInsets();
                        ZakatICRList.Margin = new Thickness(0, 5, 70, 0);
                        BPicker.Margin = new Thickness(20, 0, 60, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(20, 0, 80, 5);


                        //safeInsets.Left = 80;
                        //safeInsets.Right = 80;
                        Padding = safeInsets;
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ZakatICRList.Margin = new Thickness(0, 5, 0, 0);
                        BPicker.Margin = new Thickness(0, 0, 0, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(0, 0, 0, 5);


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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                //PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Application.Current.GetType().Assembly);
            }
        }
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();

        }

        #endregion

        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();// called from here so List have the updated data after amendment or release
            viewModel.MyZakatReturns = new List<Models.EstimatedZakatReturnsResult>();
            viewModel.myZakatReturnsList = new List<Models.EstimatedZakatReturnsResult>();
            await viewModel.OnPageLoad();
        }

        private void BPickerButton_Clicked(object sender, EventArgs e)
        {
            BPicker.IsOpen = true;
        }

        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ICRStatusChnaged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //if(AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage)
            //{
            //    ZAKATStatus selectedICRStatus = (ZAKATStatus)e.NewValue;
            //    viewModel.SelectedICRStatus = selectedICRStatus;
            //    viewModel.TxtSelectedStatus = viewModel.PreviousSelectedICRStatus.Value;
            //    viewModel.GetFilteredZAKATICRList(viewModel.PreviousSelectedICRStatus);
            //      viewModel.PreviousSelectedICRStatus = selectedICRStatus;             
            //}
            //else
            //{
            if(viewModel.myZakatReturnsList != null )
            {
                try
                {
                    ZAKATStatus selectedICRStatus = (ZAKATStatus)e.NewValue;
                    viewModel.SelectedICRStatus = selectedICRStatus;
                    viewModel.TxtSelectedStatus = selectedICRStatus.Value;
                    viewModel.PreviousSelectedICRStatus = selectedICRStatus;
                    viewModel.GetFilteredZAKATICRList(selectedICRStatus);
                }
                catch(Exception ex)
                {

                }
              
            }
            else
            {

            }
               

            //}


        }
    }
}