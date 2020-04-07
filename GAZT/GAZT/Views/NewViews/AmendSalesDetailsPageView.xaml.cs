using GAZT.Manager;
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
    public partial class AmendSalesDetailsPageView : ContentPage
    {
        #region Variable
        AmendSalesDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion
     
        #region Constructor
        public AmendSalesDetailsPageView(SalesDetails SelectedSalesDetails)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.AmendSalesDetailsPageView;
                SelectedSalesDetails.ComingFromAmendEditMode = true;
                this.BindingContext = viewModel;
                AmendSalesDetailsPageViewModel.SelectedSalesDetails = SelectedSalesDetails;
                viewModel.ClearData();
                viewModel.isOnLoad = true;
                viewModel.OnLoad();
                SetLTR();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                SetDynamicBehaviour();
                ChangeAeroIcon();

            }
            catch (Exception ex)
            {
           
            }
        }
        #endregion

        #region Method

        private void SetDynamicBehaviour()
        {

            if (SalesType.Text.Equals(AppResources.ZZAveragenumberoflabour))
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false,Max = 14, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
            }
            else
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false,Max = 18, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
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

        private void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }

        private async void OnDeleteAttachmentClickedTapped(object sender, EventArgs e)
        {
            Image deleteImage = sender as Image;
            ZakatAttachment estimateZakatAttachment = (ZakatAttachment)deleteImage.BindingContext;
            if(estimateZakatAttachment != null) 
            {
                var result = await this.DisplayAlert(AppResources.ZZDELETEFILE,AppResources.ZZDeleteAttachmentConfirmationText + " " + estimateZakatAttachment.Filename + "?", AppResources.OKText, AppResources.ZZCancel);
                if (result)
                {
                 await   viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                }
                else
                {

                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue = viewModel.NewValue;
            AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason = viewModel.ChangeReason;
        }

        public void OnEntryUnFocussed(object sender, EventArgs args)
        {
            if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
            {
                NewValue.Text = UtilityManager.GetCommaSeparatedAmount(NewValue.Text);
                NewValue.TextColor = Color.Black;
            }
            else
            {
               
                // UserName.TextColor = Color.Black;
            }
        }


        public void OnEntryFocussed(object sender, EventArgs args)
        {
            if (NewValue.Text.Contains(","))
            {
                NewValue.Text = NewValue.Text.Replace(",", "");
                NewValue.TextColor = Color.Black;
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
    }
}
