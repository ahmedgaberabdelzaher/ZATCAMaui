using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
//using Syncfusion.BarcodeReader.OPX;
//using Syncfusion.Pdf.Parsing;

namespace EGAZT.Views.NewDesign.VATLookUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
        bool isMandatoryDataEntered = true;
        VATLookUpNewPageViewModel viewModel;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            this.BindingContext = viewModel;

            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
        }
     
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            isMandatoryDataEntered = true;
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            await Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(viewModel.Name))
                {
                    viewModel.ResetFormData();
                    return;
                }
                viewModel.ValidateFormData();
                if (isMandatoryDataEntered)
                {
                    viewModel.getBarcodeData();
                }

            });

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        
        void PPicker_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsNameVisible)
                return;

            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
                PopUp popUp = new PopUp();//SetPlaceholderText();
                popUp.Message = viewModel.VATACCOrCRNOOrVATCER;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }

        void PPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
        }

        private void btnScan_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            ZXingScannerPage scanPage = new ZXingScannerPage();
            Navigation.PushAsync(scanPage);

            string id = string.Empty;
            string _language = "A";
            scanPage.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    entryNumber.Text = result.Text;
                    id = result.Text;
                    viewModel.SelectedParameterType = viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                    SearchParameterEnter.Text = AppResources.ZVATLookupIDTaxpayerTinType1;
                    viewModel.getBarcodeData();

                });
            };
        }
    }
}
