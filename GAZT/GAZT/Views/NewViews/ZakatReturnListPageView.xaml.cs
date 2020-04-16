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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnListPageView : ContentPage
    {

        #region Variable
        ZakatReturnListPageViewModel viewModel;

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
            if(viewModel.myZakatReturnsList != null && viewModel.myZakatReturnsList.Count > 0)
            {
                ZAKATStatus selectedICRStatus = (ZAKATStatus)e.NewValue;
                viewModel.SelectedICRStatus = selectedICRStatus;
                viewModel.TxtSelectedStatus = selectedICRStatus.Value;
                viewModel.PreviousSelectedICRStatus = selectedICRStatus;
                viewModel.GetFilteredZAKATICRList(selectedICRStatus);
            }
               

            //}


        }
    }
}