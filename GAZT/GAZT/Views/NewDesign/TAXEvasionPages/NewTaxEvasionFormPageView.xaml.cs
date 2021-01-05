using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
//using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaxEvasionFormPageView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;
        public NewTaxEvasionFormPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewTaxEvasionFormPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetPickerFont();
            ClearFields();
            SetDataToUI();
            SetLocationToMap();
            viewModel.CreateCompanyTypeList();
            viewModel.OnPageLoad();

        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            RegionPicker.HeaderFontFamily = "SSTArabic-Medium";
                            RegionPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            RegionPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            RegionPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy


                            CityPicker.HeaderFontFamily = "SSTArabic-Medium";
                            CityPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            CityPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            CityPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//dd      


                            ReportTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                            ReportTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            ReportTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            ReportTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        RegionPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        RegionPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        RegionPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        RegionPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        CityPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CityPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CityPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CityPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy  

                        ReportTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ReportTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ReportTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ReportTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                RegionPicker.BackgroundColor = Color.FromHex("#f7f7f7");
                CityPicker.BackgroundColor = Color.FromHex("#f7f7f7");
                ReportTypePicker.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                RegionPicker.BackgroundColor = Color.FromHex("#FFFFFF");
                CityPicker.BackgroundColor = Color.FromHex("#FFFFFF");
                ReportTypePicker.BackgroundColor = Color.FromHex("#FFFFFF");
            }

            GetCameraCommand();
            GetGalleryCommand();
        }

        private async void OnAttachmentClick(object sender, EventArgs e)
        {

            //var result = await this.DisplayAlert(AppResources.Attachment, AppResources.NDSelectFilesOrCameraToUploadTheAttachment, AppResources.NDCamera, AppResources.NDFiles);
            //if (result)
            //{
            //    viewModel.UploadAttachment();
            //}
            //else
            //{
            //    await viewModel.AddAttachment();
            //}

            await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView("SelectAttachment"));

        }

        public async void GetCameraCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "OnCameraClicked", async (sender, arg) =>
                {
                     viewModel.UploadAttachment();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void GetGalleryCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "OnGalleryClicked", async (sender, arg) =>
                {
                     viewModel.AddAttachment();
                });
            }
            catch (Exception ex)
            {

            }
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                MessagingCenter.Unsubscribe<object, string>(this, "OnCameraClicked");
                MessagingCenter.Unsubscribe<object, string>(this, "OnGalleryClicked");
            }
            catch (Exception ex)
            {

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

                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        #region Method
        public void ClearFields()
        {
            try
            {
                //viewModel.SelectedTaxEvasionCompanyType = null;
                //viewModel.SelectedTaxEvasionRegion = null;
                //viewModel.SelectLCType = null;
                //viewModel.UploadedDocumentsListObj = null;
                //viewModel.IsVisibleForReportDisplay = true;
                if (viewModel.CList != null)
                {
                    try
                    {
                        viewModel.CList.Clear();
                    }
                    catch (Exception ex)
                    {

                    }

                }
                if (viewModel.RList != null)
                {

                    try
                    {
                        viewModel.RList.Clear();
                    }
                    catch (Exception ex)
                    {

                    }

                }

                viewModel.AttachmentCount = 0;
                viewModel.TxtReportDetailCity = string.Empty;
                viewModel.TxtReportDetailRegion = string.Empty;
                viewModel.TFDAdress = string.Empty;
                viewModel.TFSAddress = string.Empty;
                viewModel.TReportDetail = string.Empty;
                viewModel.TFaciName = string.Empty;
                viewModel.TxtTIN = string.Empty;
                viewModel.TVatNumber = string.Empty;

                viewModel.UploadedDocumentsListObj.Clear();

                City_entry.Text = string.Empty;
                Region_entry.Text = string.Empty;
                TFaciName.Text = string.Empty;
                TVatNumber.Text = string.Empty;
                TxtTIN.Text = string.Empty;

                TFSAddress.Text = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }

        private void SetDataToUI()
        {
            if (App.TaxEvasionUserData != null)
            {
                viewModel.TMobNumber = App.TaxEvasionUserData.Mobile;
                viewModel.TName = App.TaxEvasionUserData.FullName;
                viewModel.TFWType = "NA";
            }
            if (viewModel.TMobNumber.StartsWith("+966"))
                viewModel.TMobNumber = viewModel.TMobNumber.Replace("+966","");

        }

        private async void SetLocationToMap()
        {
            //await Task.Run(async () =>
            //{
                try
                {
                    double lat = 24.7136, lon = 46.6753;
                    try
                    {
                    //var timeout = TimeSpan.FromSeconds(4);

                    //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    //var location = await Geolocation.GetLocationAsync(request);
                    //if (location != null)
                    //{
                    //    lat = location.Latitude;
                    //    lon = location.Longitude;
                    //}
                    Position position = new Position(lat, lon);
                        MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                        mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = lat;
                        viewModel.Longitude = lon;
                        Pin pin = new Pin();
                        pin.Label = "Report Location";
                        pin.Type = PinType.Place;
                        pin.Position = position;//new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                        var addrs = (await Geocoding.GetPlacemarksAsync(new Location(viewModel.Latitude, viewModel.Longitude))).FirstOrDefault();
                        viewModel.RLocation = addrs.Thoroughfare + " " + addrs.SubThoroughfare + ", " + addrs.Locality + ", " + addrs.CountryName + " - " + addrs.PostalCode;
                    try
                    {
                        mapView.Pins.Clear();
                    }
                    catch
                    {

                    }

                    mapView.Pins.Add(pin);

                    setCurrentLocationtomap();


                }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Handle not supported on device exception
                    }
                    catch (FeatureNotEnabledException fneEx)
                    {
                        // Handle not enabled on device exception
                    }
                    catch (PermissionException pEx)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to get location
                    }
                }
                catch (Exception ex)
                {
                }

            //});
           
        }
        private async  void setCurrentLocationtomap()
        {
            try
            {
                double lat = 24.7136, lon = 46.6753;
                try
                {
                    //var timeout = TimeSpan.FromSeconds(4);

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        lat = location.Latitude;
                        lon = location.Longitude;
                    }
                    Position position = new Position(lat, lon);
                    MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                    mapView.MoveToRegion(mapSpan);
                    viewModel.Latitude = lat;
                    viewModel.Longitude = lon;
                    Pin pin = new Pin();
                    pin.Label = "Report Location";
                    pin.Type = PinType.Place;
                    pin.Position = position;//new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    var addrs = (await Geocoding.GetPlacemarksAsync(new Location(location.Latitude, location.Longitude))).FirstOrDefault();
                    viewModel.RLocation = addrs.Thoroughfare + " " + addrs.SubThoroughfare + ", " + addrs.Locality + ", " + addrs.CountryName + " - " + addrs.PostalCode;
                    try
                    {
                        mapView.Pins.Clear();
                    }
                    catch
                    {

                    }
                    mapView.Pins.Add(pin);



                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TxtTIN_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTIN.Text))
            {

                PopUp popUp = new PopUp();
                popUp.Message = AppResources.ZInvalidTinNumber;
                popUp.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                if (TxtTIN.Text.Substring(0, 1) != "3")
                {
                  //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                     PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionTINValidationMessage));
                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                    // TxtTIN.Focus();
                }else if(TxtTIN.Text.Length < 10)
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionTINDigitValidationMessage));
                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                }
                else
                {
                    FrmTIN.HasError = false;
                }
            }
        }


       




        private void TVatNumber_Unfocused(object sender, FocusEventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(TVatNumber.Text))
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZInvalidVatNumber;
                    popUp.IsLinkAvailable = false;
                    //if (App.IsArabic)
                    //{
                    //    popUp.FlowDirections = "RightToLeft";
                    //}
                    //else
                    //{
                    //    popUp.FlowDirections = "LeftToRight";
                    //}
                    //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    if (TVatNumber.Text.Length < 15)
                    {

                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionVATDigitValidationMessage));
                        FrmVAT.HasError = true;
                        TVatNumber.Text = string.Empty;
                    }else if(TVatNumber.Text.Substring(0, 1) != "1")
                        
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionVATNumValidationMessage));
                        FrmVAT.HasError = true;
                        TVatNumber.Text = string.Empty;
                    }

                    else
                    {
                        FrmVAT.HasError = false;
                    }
                }
            }
        }

        private void TFaciName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFaciName.Text))
            { FrmFName.HasError = true; }
            else
            {
                FrmFName.HasError = false;
                viewModel.IsFacilityNameHasError = false;
            }
        }

        private void TReportDetail_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TReportDetail.Text))
            {
                FrmReportDetail.HasError = true;
            }
            else
            {
                FrmReportDetail.HasError = false;
                viewModel.IsReportDetailHasError = false;
            }
        }

        private async void mapView_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            try
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //var location = await Geolocation.GetLocationAsync(request);
                //if (location != null)
                //{
                //    Position position = new Position(location.Latitude, location.Longitude);
                //    MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                //    //mapView.MoveToRegion(mapSpan);
                //    viewModel.Latitude = location.Latitude;
                //    // viewModel.TEReportobj.Latitude = location.Latitude.ToString();
                //    viewModel.Longitude = location.Longitude;
                //}
                Pin pin = new Pin();
                pin.Label = "Your Location";
                pin.Type = PinType.Place;
                pin.Position = new Position(e.Position.Latitude, e.Position.Longitude);
                viewModel.Latitude = e.Position.Latitude;
                viewModel.Longitude = e.Position.Longitude;
                mapView.Pins.Clear();
                mapView.Pins.Add(pin);
                var addrs = (await Geocoding.GetPlacemarksAsync(new Location(viewModel.Latitude, viewModel.Longitude))).FirstOrDefault();
                viewModel.RLocation = addrs.Thoroughfare + " " + addrs.SubThoroughfare + ", " + addrs.Locality + ", " + addrs.CountryName + " - " + addrs.PostalCode;

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }

        private void RegionBtnClicked(object sender, EventArgs e)
        {
            RegionPicker.IsOpen = true;
        }

        void RegionPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum taxEvasionRegionCityDatum = (TaxEvasionRegionCityDatum)e.NewValue;
            RegionPicker.SelectedItem = taxEvasionRegionCityDatum;
            viewModel.SelectedTaxEvasionRegion = taxEvasionRegionCityDatum;
        }

        void CityPickerButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(Region_entry.Text))
            {
               
                 CityPicker.IsOpen = true;
                
            }
            else
            {
                RegionPicker.IsOpen = true;
            }
        }

        void CityPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum selectedcity = (TaxEvasionRegionCityDatum)e.NewValue;
            CityPicker.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            //viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.Name;
        }

        void TFDAdress_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFDAdress.Text))
            {
                FrmFDAddress.HasError = true;
            }
            else
            { FrmFDAddress.HasError = false;
                viewModel.IsFDHasError = false;
            }
        }

        void TMobNumber_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (App.IsArabic)
            {
                if (!string.IsNullOrEmpty(TMobNumber.Text))
                {
                    if (TMobNumber.Text.Length < 9)
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZInvalidMobileNoError;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZInvalidMobileNoError));
                        FrmNumber.HasError = true;
                        TMobNumber.Text = string.Empty;
                    }
                    else
                    {
                        FrmNumber.HasError = false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(TMobNumber.Text))
                {
                    if (TMobNumber.Text.Length < 9)
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZInvalidMobileNoError;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                       // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZInvalidMobileNoError));
                        FrmNumber.HasError = true;
                        TMobNumber.Text = string.Empty;
                    }
                    else
                    {
                        FrmNumber.HasError = false;
                    }
                }
            }
        }

        void ReportTypeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ReportTypePicker.IsOpen = true;
        }

        void ReportTypePicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionCategoriesDataModel selectedReportType = (TaxEvasionCategoriesDataModel)e.NewValue;
            ReportTypePicker.SelectedItem = selectedReportType;
            viewModel.SelectedReportTypeListItem = selectedReportType;
            viewModel.SelectedCategory = selectedReportType.Title;
            viewModel.TxtReporttype = selectedReportType.Title;
        }

        void OnDeleteAttachmentClicked(System.Object sender, System.EventArgs e)
        {
            Image arrowImage = sender as Image;
            UploadedDocumentsList attachment = (UploadedDocumentsList)arrowImage.BindingContext;
            viewModel.UploadedDocumentsListObj.Remove(attachment);
            viewModel.AttachmentCount = viewModel.AttachmentCount - 1;
            viewModel.AttachmentName = string.Empty;
        }
        #endregion

        private void TNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TName))
            {
                viewModel.IsTnameHasError = false;

            }
        }

        private void TMobNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TMobNumber))
            {
                viewModel.IsTmobileHasError = false;

            }
        }

        private void Region_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.IsRegionHasError = false;
        }

        private void City_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(City_entry.Text))
                {
                viewModel.IsCityHasError = false;

            }
        }

        private void TFSAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TFSAddress))
            {
                viewModel.IsFSHasError = false;
            }
        }

        private void mapView_MapClicked(object sender, MapClickedEventArgs e)
        {

        }

        //private void mapView_MapClicked(object sender, MapClickedEventArgs e)
        //{

        //}
    }
}