using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionPages
{
    [Preserve(AllMembers = true)]
    public partial class TaxEvasionRegistrationPageView : ContentPage
    {
        TaxEvasionRegistrationViewModel viewModel;

        public TaxEvasionRegistrationPageView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.TaxEvasionRegistrationFormPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            GetRegionList();
            SetPickerFont();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
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
        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtName))
            {
                FrmName.HasError = false;
            }
        }

        private void btnReportDetailCity_Clicked(object sender, EventArgs e)
        {
            CityPicker.IsOpen = true;
        }

        private void btnSubmitNext_Clicked(object sender, EventArgs e)
        {

        }
      
        private void CityPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum selectedcity = (TaxEvasionRegionCityDatum)e.NewValue;
            CityPicker.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.Name;
            FrmCity.HasError = false;

        }
        private void CityPickerAR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum selectedcity = (TaxEvasionRegionCityDatum)e.NewValue;
           // CityPickerAR.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.Name;
            FrmCity.HasError = false;
        }

        private void CityPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectLCType = viewModel.SelectLCTypePrev;
            CityPicker.SelectedItem = viewModel.SelectLCTypePrev;
            if (viewModel.SelectLCTypePrev == null)
            {
                viewModel.TxtReportDetailCity = string.Empty;
            }
        }
        private void CityPickerAR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectLCType = viewModel.SelectLCTypePrev;
            CityPickerAR.SelectedItem = viewModel.SelectLCTypePrev;
            if (viewModel.SelectLCTypePrev == null)
            {
                viewModel.TxtReportDetailCity = string.Empty;
            }
        }


        private void TEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.InvalidEmailFormat;
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;

                }
                else
                {
                    FrmEmailAddress.HasError = false;
                }
            }
            else
            {
                FrmEmailAddress.HasError = false;
            }
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public async Task GetRegionList()
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.OnPageLoad();//TaxEvasionRegistration
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = true; });
            await AddReport();
            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = false; });
            //viewModel.SubmitCreatedReport();
        }

        public async Task AddReport()
        {
            bool flag = true;
            bool showMessage = false;
            if (string.IsNullOrEmpty(EntryName.Text))
            {
                showMessage = true;
                FrmName.HasError = true;
                flag = false;

                //flag = false; TName.Focus(); FrmName.HasError = true; showFillFeildsMessage();
            }
            if (string.IsNullOrEmpty(City_entry.Text))
            {
                flag = false;
                FrmCity.HasError = true;
                showMessage = true;
            }
            if (showMessage == true)
            {
                showFillFeildsMessage();
            }
            if (flag == true)
            {
                await viewModel.RegisterCommandClick();
            }
        }

        private void showFillFeildsMessage()
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZPleasefillallthemandatoryfields;
            popUp.IsLinkAvailable = false;
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

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                CityPicker.HeaderFontFamily = "GE SS Two";
                                CityPicker.ColumnHeaderFontFamily = "GE SS Two";
                                CityPicker.SelectedItemFontFamily = "GE SS Two";
                                CityPicker.UnSelectedItemFontFamily = "GE SS Two";//CityPickerAR

                                CityPickerAR.HeaderFontFamily = "GE SS Two";
                                CityPickerAR.ColumnHeaderFontFamily = "GE SS Two";
                                CityPickerAR.SelectedItemFontFamily = "GE SS Two";
                                CityPickerAR.UnSelectedItemFontFamily = "GE SS Two";//CityPicker
                            }
                            else
                            {
                                CityPicker.HeaderFontFamily = "SSTArabic-Medium";
                                CityPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CityPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                CityPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//CityPickerAR

                                CityPickerAR.HeaderFontFamily = "SSTArabic-Medium";
                                CityPickerAR.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CityPickerAR.SelectedItemFontFamily = "SSTArabic-Medium";
                                CityPickerAR.UnSelectedItemFontFamily = "SSTArabic-Medium";//CityPickerAR
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            CityPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";

                            CityPickerAR.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CityPicker

                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
