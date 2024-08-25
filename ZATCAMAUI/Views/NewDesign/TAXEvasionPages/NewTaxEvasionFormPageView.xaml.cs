
using Maui.GoogleMaps;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaxEvasionFormPageView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;

        public NewTaxEvasionFormPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewTaxEvasionFormPageView;
            BindingContext = viewModel;
            SetPickerFont();
            ClearFields();
            SetDataToUI();
            SetLocationToMap();
            viewModel.CreateCompanyTypeList();
            _ = viewModel.OnPageLoad();

        }
        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {

                            RegionPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            RegionPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            RegionPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            RegionPicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy


                            CityPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            CityPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            CityPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            CityPicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy


                            ReportTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ReportTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ReportTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            ReportTypePicker.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:

                        RegionPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        RegionPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        RegionPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        RegionPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//ddlLIssuedBy

                        CityPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        CityPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        CityPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        CityPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//ddlLIssuedBy

                        ReportTypePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ReportTypePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ReportTypePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ReportTypePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//ddlLIssuedBy

                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                RegionPicker.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                CityPicker.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                ReportTypePicker.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                RegionPicker.BackgroundColor = (Color)Application.Current.Resources["White"];
                CityPicker.BackgroundColor = (Color)Application.Current.Resources["White"];
                ReportTypePicker.BackgroundColor = (Color)Application.Current.Resources["White"];
            }

            GetCameraCommand();
            GetGalleryCommand();
        }

        private async void OnAttachmentClick(object sender, EventArgs e)
        {


            await MopupService.Instance.PushAsync(new ZAKATOkCancelPopUpView("SelectAttachment"));

        }

        public void GetCameraCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "OnCameraClicked", (sender, arg) =>
                {
                    viewModel.UploadAttachment();
                });
            }
            catch (Exception)
            {


            }
        }

        public void GetGalleryCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "OnGalleryClicked", (sender, arg) =>
                {
                    viewModel.AddAttachment();
                });
            }
            catch (Exception)
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
            catch (Exception)
            {


            }
        }



        #region Method
        public void ClearFields()
        {
            try
            {
                if (viewModel.CList != null)
                {
                    try
                    {
                        viewModel.CList.Clear();
                    }
                    catch (Exception)
                    {


                    }

                }
                if (viewModel.RList != null)
                {

                    try
                    {
                        viewModel.RList.Clear();
                    }
                    catch (Exception)
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
            catch (Exception)
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
                viewModel.TMobNumber = viewModel.TMobNumber.Replace("+966", "");

        }

        private async void SetLocationToMap()
        {
            try
            {
                double lat = 24.7136, lon = 46.6753;
                try
                {
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

                catch (Exception)
                {
                    // Unable to get location


                }
            }
            catch (Exception)
            {


            }

            //});

        }
        private async void setCurrentLocationtomap()
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
                    catch (Exception)
                    {


                    }
                    mapView.Pins.Add(pin);



                }

                catch (Exception)
                {
                    // Unable to get location
                }
            }
            catch (Exception)
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
                    //  MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionTINValidationMessage));
                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                    // TxtTIN.Focus();
                }
                else if (TxtTIN.Text.Length < 10)
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionTINDigitValidationMessage));
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
                    if (TVatNumber.Text.Length < 15)
                    {

                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionVATDigitValidationMessage));
                        FrmVAT.HasError = true;
                        TVatNumber.Text = string.Empty;
                    }
                    else if (TVatNumber.Text.Substring(0, 1) != "1")

                    {
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTaxEvasionVATNumValidationMessage));
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

        private async void mapView_MapClicked(object sender, MapClickedEventArgs e)
        {
            try
            {

                Pin pin = new Pin();
                pin.Label = "Your Location";
                pin.Type = PinType.Place;
                pin.Position = new Position(e.Point.Latitude, e.Point.Longitude);
                viewModel.Latitude = e.Point.Latitude;
                viewModel.Longitude = e.Point.Longitude;
                mapView.Pins.Clear();
                mapView.Pins.Add(pin);
                var addrs = (await Geocoding.GetPlacemarksAsync(new Location(viewModel.Latitude, viewModel.Longitude))).FirstOrDefault();
                viewModel.RLocation = addrs.Thoroughfare + " " + addrs.SubThoroughfare + ", " + addrs.Locality + ", " + addrs.CountryName + " - " + addrs.PostalCode;

            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception


            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception


            }
            catch (PermissionException ex)
            {


                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location


            }

        }

        private void RegionBtnClicked(object sender, EventArgs e)
        {
            RegionPicker.IsOpen = true;
        }

        void RegionPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            //TODO
            TaxEvasionRegionCityDatum taxEvasionRegionCityDatum = viewModel.RList[e.NewValue];
            //RegionPicker.SelectedItem = taxEvasionRegionCityDatum;
            viewModel.SelectedTaxEvasionRegion = taxEvasionRegionCityDatum;
        }

        void CityPickerButton_Clicked(object sender, EventArgs e)
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

        void CityPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            //TODO
            TaxEvasionRegionCityDatum selectedcity = viewModel.CList[e.NewValue];
            //CityPicker.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            //viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.Name;
        }

        void TFDAdress_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFDAdress.Text))
            {
                FrmFDAddress.HasError = true;
            }
            else
            {
                FrmFDAddress.HasError = false;
                viewModel.IsFDHasError = false;
            }
        }

        void TMobNumber_Unfocused(object sender, FocusEventArgs e)
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
                        //MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZInvalidMobileNoError));
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
                        // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZInvalidMobileNoError));
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

        void ReportTypeButton_Clicked(object sender, EventArgs e)
        {
            ReportTypePicker.IsOpen = true;
        }

        void ReportTypePicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            //TODO
            TaxEvasionCategoriesDataModel selectedReportType = viewModel.ReportTypes[e.NewValue];
            //ReportTypePicker.SelectedItem = selectedReportType;
            viewModel.SelectedReportTypeListItem = selectedReportType;
            viewModel.SelectedCategory = selectedReportType.Title;
            viewModel.TxtReporttype = selectedReportType.Title;
        }

        void OnDeleteAttachmentClicked(object sender, EventArgs e)
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
    }
}