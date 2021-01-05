using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionFormPage_ViewModel;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TaxEvasionFormPage : ContentPage
    {
        private double width = 0;
        private double height = 0;
        TaxEvasionFormPageViewModel viewModel;
     
        public TaxEvasionFormPage(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.TaxEvasionFormPage;
                SetLTR();
                this.BindingContext = viewModel;
                MainLayout.Padding = new Thickness(0, 0, 0, 0);
                ChangeAeroIcon();

                viewModel.selectedtaxEList = new TaxEvasionReportDetails();
                viewModel.selectedtaxEList = SelectedTaxEvasionListItem;

                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel.UploadedDocumentsList = new UploadedDocumentsList();

                //viewModel.TaxEvasionReportTobeUsedToSubmit = new TaxEvasionReportTobeUsedToSubmit();

                //RegionPicker
            }
            catch (Exception ex)
            {
            }
            // viewModel.SelectedCategory = SelectedCat;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ClearFields();
            viewModel.CreateCompanyTypeList();

            SetDataToUI();
            SetPickerFont();

            if (viewModel.selectedtaxEList != null && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
                SetLocationToMap();
            }

            await GetRegionList();
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
                                RegionPicker.HeaderFontFamily = "GE SS Two";
                                RegionPicker.ColumnHeaderFontFamily = "GE SS Two";
                                RegionPicker.SelectedItemFontFamily = "GE SS Two";
                                RegionPicker.UnSelectedItemFontFamily = "GE SS Two";//RegionPickerAR

                                RegionPickerAR.HeaderFontFamily = "GE SS Two";
                                RegionPickerAR.ColumnHeaderFontFamily = "GE SS Two";
                                RegionPickerAR.SelectedItemFontFamily = "GE SS Two";
                                RegionPickerAR.UnSelectedItemFontFamily = "GE SS Two";//CityPicker

                                CityPicker.HeaderFontFamily = "GE SS Two";
                                CityPicker.ColumnHeaderFontFamily = "GE SS Two";
                                CityPicker.SelectedItemFontFamily = "GE SS Two";
                                CityPicker.UnSelectedItemFontFamily = "GE SS Two";//CityPickerAR

                                CityPickerAR.HeaderFontFamily = "GE SS Two";
                                CityPickerAR.ColumnHeaderFontFamily = "GE SS Two";
                                CityPickerAR.SelectedItemFontFamily = "GE SS Two";
                                CityPickerAR.UnSelectedItemFontFamily = "GE SS Two";//CityPickerAR
                            }
                            else
                            {
                                RegionPicker.HeaderFontFamily = "SSTArabic-Medium";
                                RegionPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                RegionPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                RegionPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//RegionPickerAR

                                RegionPickerAR.HeaderFontFamily = "SSTArabic-Medium";
                                RegionPickerAR.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                RegionPickerAR.SelectedItemFontFamily = "SSTArabic-Medium";
                                RegionPickerAR.UnSelectedItemFontFamily = "SSTArabic-Medium";//RegionPickerAR

                                CityPicker.HeaderFontFamily = "SSTArabic-Medium";
                                CityPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CityPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                CityPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//CityPickerAR

                                CityPickerAR.HeaderFontFamily = "SSTArabic-Medium";
                                CityPickerAR.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CityPickerAR.SelectedItemFontFamily = "SSTArabic-Medium";
                                CityPickerAR.UnSelectedItemFontFamily = "SSTArabic-Medium";//CityPickerAR.HeaderFontFamily = "SSTArabic-Medium";

                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {


                            RegionPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";

                            RegionPickerAR.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPickerAR.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPickerAR.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            RegionPickerAR.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CityPicker

                            CityPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CityPickerAR

                            CityPickerAR.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            CityPickerAR.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CityPickerAR
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }


        private void SetDataToUI()
        {
            viewModel.SelectedCategory = viewModel.selectedtaxEList.Category;
            if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
                //btn4.IsEnabled = false;
                if (App.IsArabic)
                {
                    viewModel.TxtReportDetailCity = viewModel.selectedtaxEList.City;
                    viewModel.TxtReportDetailRegion = viewModel.selectedtaxEList.RegionNameAr;
                }
                else
                {
                    viewModel.TxtReportDetailCity = viewModel.selectedtaxEList.City;
                    viewModel.TxtReportDetailRegion = viewModel.selectedtaxEList.RegionNameAr;
                }
                viewModel.IsVisibleForReportDisplay = false;
                btnReportDetailCity.IsEnabled = false;
                City_entry.IsEnabled = false;
                CityPicker.IsEnabled = false;
                CityPickerAR.IsEnabled = false;
                btnTxtReportDetailRegion.IsEnabled = false;
                RegionPicker.IsEnabled = false;
                RegionPickerAR.IsEnabled = false;
                Region_entry.IsEnabled = false;
                //  Attachment_Label.IsVisible = false;
                //  Attachment_Label.IsVisible = false;
                TFSAddress.IsEnabled = false;
                TCategory.IsEnabled = false;
                viewModel.TCategory = viewModel.selectedtaxEList.Category;

                //  Attachment_Tmg.IsVisible = false;
                //  Attachment_Frm.IsVisible = false;
                //  Attachment_Entry.IsVisible = false;
                //  Attachment_Tmg.IsEnabled = false;
                
                viewModel.TName = viewModel.selectedtaxEList.Username;
                viewModel.TMobNumber = viewModel.selectedtaxEList.PhoneNumber.Remove(0, 1);
             
                viewModel.TEmail = viewModel.selectedtaxEList.EmailId;
               
                viewModel.TFaciName = viewModel.selectedtaxEList.CompanyName;
                TFaciName.IsEnabled = false;
                //TFaciMobNo.IsEnabled = false;
                //TFaciMobNoAr.IsEnabled = false;
                //TFaciEmail.IsEnabled = false;
                RegionPicker.IsEnabled = false;
                RegionPickerAR.IsEnabled = false;
                CityPicker.IsEnabled = false;
                CityPickerAR.IsEnabled = false;
                viewModel.TFDAdress = viewModel.selectedtaxEList.District;
                viewModel.TFSAddress = viewModel.selectedtaxEList.Street;
                viewModel.IsSubmitButtonEnable = false;
               // submit_btnmane.IsEnabled = false;
                //submit_btnmane.BackgroundColor = Color.Gray;
                viewModel.TReportDetail = viewModel.selectedtaxEList.Content;
                viewModel.TVatNumber = viewModel.selectedtaxEList.VatNumber;
                TVatNumber.IsEnabled = false;
                RegionPicker.IsEnabled = false;
                RegionPickerAR.IsEnabled = false;
                CityPicker.IsEnabled = false;
                CityPickerAR.IsEnabled = false;
                //btnFacilityType.IsEnabled = false;

                try
                {
                    if (!(string.IsNullOrEmpty(viewModel.selectedtaxEList.Location)))
                    {
                        string[] words = viewModel.selectedtaxEList.Location.Split(',');
                        viewModel.selectedtaxEList.Latitude = words[0];
                        viewModel.selectedtaxEList.Longitude = words[1];
                    }
                }
                catch
                {
                    viewModel.selectedtaxEList.Latitude = string.Empty;
                    viewModel.selectedtaxEList.Longitude = string.Empty;
                }

                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.RegionCode))
                {
                    //viewModel.SelectedTaxEvasionRegion = viewModel.RList.Where(x => x.RegionCode == viewModel.selectedtaxEList.RegionCode).FirstOrDefault();
                    //viewModel.onSelectedTaxEvasionRegion();
                    //if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.CityCode))
                    //{
                    //    viewModel.SelectLCType = viewModel.CList.Where(x => x.CityCode == viewModel.selectedtaxEList.CityCode).FirstOrDefault();
                    //}
                }

                if (!(string.IsNullOrEmpty(viewModel.selectedtaxEList.Latitude) && string.IsNullOrEmpty(viewModel.selectedtaxEList.Longitude)))
                {
                    Position position = new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                    mapView.MoveToRegion(mapSpan);
                    Pin pin = new Pin();
                    pin.Label = "Report Location";
                    pin.Type = PinType.Place;
                    pin.Position = new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    mapView.Pins.Clear();
                    mapView.Pins.Add(pin);
                }
                mapView.IsEnabled = false;
                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.Tin))
                { viewModel.IsTIN = true; viewModel.TxtTIN = viewModel.selectedtaxEList.Tin; viewModel.IsTINVisible = true; }
                else
                {
                    viewModel.IsTIN = false;
                }
                TxtTIN.IsEnabled = false; 
                viewModel.TID = viewModel.selectedtaxEList.Id;
            }
            else
            {
               

              
            }
        }
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
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Padding = new Thickness(0, 0, 0, 0);
                    }
                }
                //reconfigure layout
            }
        }
        private void SetLTR()
        {
            //if (!App.IsArabic)
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //    viewModel.IsVisiblePickerAr = false;
            //    viewModel.IsVisiblePickerEn = true;
            //}
            //else
            //{
            //    PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Application.Current.GetType().Assembly);
            //    viewModel.IsVisiblePickerAr = true;
            //    viewModel.IsVisiblePickerEn = false;
            //}
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;
                //facilityMobileStackLayoutAr.IsVisible = true;
                //facilityMobileStackLayout.IsVisible = false;
        
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                viewModel.IsVisiblePickerAr = false;
                viewModel.IsVisiblePickerEn = true;
                //facilityMobileStackLayout.IsVisible = true;
                //facilityMobileStackLayoutAr.IsVisible = false;
                
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                RegionPickerAR.Focus();
            }
            else
            {
                RegionPicker.Focus();
            }
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                CityPickerAR.Focus();
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;
            }
            else
            {
                CityPicker.Focus();
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = true; });
           // await AddReport();
            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = false; });
            //viewModel.SubmitCreatedReport();
        }
        public async Task AddReport()
        {
            //if (string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            // {
            //submit_btnmane.BackgroundColor = Color.Gray;
            //}
            //else
            //{
            bool flag = true;
            bool showMessage = false;
            
 
            if (string.IsNullOrEmpty(TFaciName.Text.Trim()))
            {
                flag = false;
                FrmFName.HasError = true;
                showMessage = true;
                //flag = false; TFaciName.Focus(); FrmFName.HasError = true; showFillFeildsMessage();
            }
     
            if (string.IsNullOrEmpty(TFSAddress.Text.Trim()))
            {
                //flag = false; FrmFSAddress.HasError = true; showFillFeildsMessage(); TFSAddress.Focus(); 
                flag = false;
                FrmFSAddress.HasError = true;
                showMessage = true;
            }
            if (string.IsNullOrEmpty(TxtTIN.Text.Trim()) )
            {
                //flag = false; TxtTIN.Focus(); FrmTIN.HasError = true; showFillFeildsMessage();
                flag = false;
                FrmTIN.HasError = true;
                showMessage = true;
            }
   
            if (string.IsNullOrEmpty(Region_entry.Text))
            {
                flag = false;
                frmRegionPicker.HasError = true;
                showMessage = true;
                //if (App.IsArabic)
                //{
                //    RegionPickerAR.IsOpen = true;
                //}
                //else
                //{
                //    RegionPicker.IsOpen = true;
                //}
            }
            if (string.IsNullOrEmpty(City_entry.Text))
            {
                flag = false;
                FrmCity.HasError = true;
                showMessage = true;
                // showFillFeildsMessage();
                //if (App.IsArabic)
                //{
                //    CityPickerAR.IsOpen = true;
                //}
                //else
                //{
                //    CityPicker.IsOpen = true;
                //}
            }
            //else if (string.IsNullOrEmpty(Date_entry.Text))
            //{
            //    showFillFeildsMessage();
            //    flag = false; FrmDBO.HasError = true; DpDbo.IsOpen = true;
            //}
            if (showMessage == true)
            {
                await showFillFeildsMessage();
            }
            if (flag == true)
            {

                await viewModel.SubmitCreatedReport();
            }
            // }
        }

        
        private void TFSAddress_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFSAddress.Text))
            {
                FrmFSAddress.HasError = true;
            }
            else
            {
                FrmFSAddress.HasError = false;
            }
        }
        private void TFSName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFSAddress.Text))
            {
                FrmFSAddress.HasError = true;
            }
            else
            {
                FrmFSAddress.HasError = false;
            }
        }
        private void TxtTIN_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTIN.Text))
            {
                if (TxtTIN.Text.Length < 10 || (TxtTIN.Text.Substring(0, 1) != "3"))
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
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                    // TxtTIN.Focus();
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
                    if (TVatNumber.Text.Length < 15)
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZInvalidVatNumber;
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

        public void ClearFields()
        {
            try
            {
                //viewModel.SelectedTaxEvasionCompanyType = null;
                //viewModel.SelectedTaxEvasionRegion = null;
                //viewModel.SelectLCType = null;
                //viewModel.UploadedDocumentsListObj = null;
                viewModel.IsVisibleForReportDisplay = true;
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
                viewModel.UploadedDocumentsListObj.Clear();
                //Attachment_Entry.Text = string.Empty;
                //FacilityType_entry.Text = string.Empty;
                //Date_entry.Text = string.Empty;
                City_entry.Text = string.Empty;
                Region_entry.Text = string.Empty;
               
                TFaciName.Text = string.Empty;
      
                //TFaciMobNo.Text = string.Empty;
                //TFaciMobNoAr.Text = string.Empty;
               
                //TFaciEmail.Text = string.Empty;
                TVatNumber.Text = string.Empty;
                TxtTIN.Text = string.Empty;
                viewModel.IsTIN = true;
                viewModel.IsTINVisible = true;
                
                TFSAddress.Text = string.Empty;
                //DateLabel.IsVisible = false; DateLabel.IsEnabled = false; DateLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
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
                await viewModel.OnPageLoad();//TaxEvasionReport
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        private async void SetLocationToMap()
        {
            try
            {
                if (!((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId)))
                {
                    viewModel.IsLoading = false;
                    //clearFields();
                }
                else
                {
                    double lat = 24.7136, lon = 46.6753;
                    try
                    {
                        //var timeout = TimeSpan.FromSeconds(4);
                        //var locationRequestData = new GeolocationRequest(GeolocationAccuracy.Medium, timeout);

                        //var location = Geolocation.GetLocationAsync(locationRequestData).Result;
                        //if (location != null)
                        //{
                        //    lat = location.Latitude;
                        //    lon = location.Longitude;
                        //}

                        Position position = new Position(lat, lon);
                        MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                         mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = lat;
                        viewModel.Longitude = lon;
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
            }
            catch (Exception ex)
            {
            }
        }
        private async void mapView_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            if ((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
                //   viewModel.IsLoading = false;
                //clearFields();
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        Position position = new Position(location.Latitude, location.Longitude);
                        MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                         mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = location.Latitude;
                       // viewModel.TEReportobj.Latitude = location.Latitude.ToString();
                        viewModel.Longitude = location.Longitude;
                    }
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
                Pin pin = new Pin();
                pin.Label = "Your Location";
                pin.Type = PinType.Place;
                pin.Position = new Position(e.Position.Latitude, e.Position.Longitude);
                viewModel.Latitude = e.Position.Latitude;
                viewModel.Longitude = e.Position.Longitude;
                  mapView.Pins.Clear();
                  mapView.Pins.Add(pin);
            }
            else
            {
            }
        }
        //private void btnFacilityType_Clicked(object sender, EventArgs e)
        //{
        //    ddlFacilityType.IsOpen = true;
        //}
        private void btnTxtReportDetailRegion_Clicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                RegionPickerAR.IsOpen = true;
            }
            else
            {
                RegionPicker.IsOpen = true;
            }
        }
        private void btnReportDetailCity_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Region_entry.Text))
            {
                if (App.IsArabic)
                {
                    CityPickerAR.IsOpen = true;
                }
                else
                {
                    CityPicker.IsOpen = true;
                }
            }
            else
            {
                if (App.IsArabic)
                {
                    RegionPickerAR.IsOpen = true;
                }
                else
                {
                    RegionPicker.IsOpen = true;
                }
            }
        }
        //private void DpDbo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
        //    string month = selectedItem[0].ToString();
        //    string day = selectedItem[1].ToString();
        //    string year = selectedItem[2].ToString();
        //    viewModel.DatePick = day + "/" + month + "/" + year;
        //    viewModel.DatePickPrev = day + "/" + month + "/" + year;
        //}
        //private void btn4_Clicked(object sender, EventArgs e)
        //{
        //    DpDbo.IsOpen = true;
        //}
        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
        }
        //private void ddlFacilityType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    FacilityCompanyType selectedcompanytyp = (FacilityCompanyType)e.NewValue;
        //    ddlFacilityType.SelectedItem = selectedcompanytyp;
        //    viewModel.SelectedTaxEvasionCompanyType = selectedcompanytyp;
        //    viewModel.SelectedTaxEvasionCompanyTypePrev = selectedcompanytyp;
        //    viewModel.TxtFType = selectedcompanytyp.Name;
        //    FrmFType.HasError = false;
        //}
        private void RegionPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum selectedregion = (TaxEvasionRegionCityDatum)e.NewValue;
            RegionPicker.SelectedItem = selectedregion;
            viewModel.SelectedTaxEvasionRegion = selectedregion;//selectedregion
            viewModel.SelectedTaxEvasionRegionPrev = selectedregion;//selectedregion
            viewModel.TxtReportDetailRegion = selectedregion.Name;
            frmRegionPicker.HasError = false;
            viewModel.SelectLCTypePrev = null;
            viewModel.SelectLCType = null;
            viewModel.TxtReportDetailCity = string.Empty;
            //SelectedTaxEvasionRegion
        }
        private void RegionPickerAR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TaxEvasionRegionCityDatum selectedregion = (TaxEvasionRegionCityDatum)e.NewValue;
            RegionPickerAR.SelectedItem = selectedregion;
            viewModel.SelectedTaxEvasionRegion = selectedregion;//selectedregion
            viewModel.SelectedTaxEvasionRegionPrev = selectedregion;//selectedregion
            viewModel.TxtReportDetailRegion = selectedregion.Name;
            frmRegionPicker.HasError = false;
            viewModel.SelectLCTypePrev = null;
            viewModel.SelectLCType = null;
            viewModel.TxtReportDetailCity = string.Empty;
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
            CityPickerAR.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.Name;
            FrmCity.HasError = false;
        }
        private void TFaciName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFaciName.Text))
            { FrmFName.HasError = true; }
            else
            {
                FrmFName.HasError = false;
            }
        }
        //private void Date_entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(Date_entry.Text))
        //    {
        //        FrmDBO.HasError = false;
        //    }
        //}
        //private void DpDbo_Closed(object sender, EventArgs e)
        //{
        //    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
        //    string month = selectedItem[0].ToString();
        //    string day = selectedItem[1].ToString();
        //    string year = selectedItem[2].ToString();
        //    viewModel.DatePick = day + "/" + month + "/" + year;
        //}
        private async Task showFillFeildsMessage()
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
            // await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
        //private void ddlFacilityType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    viewModel.SelectedTaxEvasionCompanyType = viewModel.SelectedTaxEvasionCompanyTypePrev;
        //    ddlFacilityType.SelectedItem = viewModel.SelectedTaxEvasionCompanyTypePrev;
        //    if (viewModel.SelectedTaxEvasionCompanyTypePrev == null)
        //    {
        //        viewModel.TxtFType = string.Empty;
        //    }
        //}
        private void RegionPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedTaxEvasionRegion = viewModel.SelectedTaxEvasionRegionPrev;
            RegionPicker.SelectedItem = viewModel.SelectedTaxEvasionRegionPrev;
            if (viewModel.SelectedTaxEvasionRegionPrev == null)
            {
                viewModel.TxtReportDetailRegion = string.Empty;
            }
        }
        private void RegionPickerAR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedTaxEvasionRegion = viewModel.SelectedTaxEvasionRegionPrev;
            RegionPickerAR.SelectedItem = viewModel.SelectedTaxEvasionRegionPrev;
            if (viewModel.SelectedTaxEvasionRegionPrev == null)
            {
                viewModel.TxtReportDetailRegion = string.Empty;
            }

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
        private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.DatePick = viewModel.DatePickPrev;
            if (!string.IsNullOrEmpty(viewModel.DatePickPrev))
            {
                string[] Date = viewModel.DatePickPrev.Split('/');
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                //Select today dates
                todaycollection.Add(Date[1]);
                todaycollection.Add(Date[2]);
                todaycollection.Add(Date[0]);
                //DpDbo.SelectedItem = todaycollection;
            }
        }
        private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            UploadedDocumentsList attachment = (UploadedDocumentsList)arrowImage.BindingContext;
            viewModel.UploadedDocumentsListObj.Remove(attachment);
            viewModel.AttachmentCount = viewModel.AttachmentCount - 1;
            viewModel.AttachmentName = string.Empty;

        }
        //    private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    FrmDBO.HasError = false;
        //    try
        //    {
        //        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
        //        string month = selectedItem[0].ToString();
        //        string day = selectedItem[1].ToString();
        //        string year = selectedItem[2].ToString();
        //        viewModel.DatePick = day + "/" + month + "/" + year;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
