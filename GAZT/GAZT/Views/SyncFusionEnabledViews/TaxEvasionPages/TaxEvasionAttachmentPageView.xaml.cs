using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage_ViewModel;
using GAZT.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.SyncFusionEnabledViews.TaxEvasionPages
{
    [Preserve(AllMembers = true)]
    public partial class TaxEvasionAttachmentPageView : ContentPage
    {
        TaxEvasionReportAttachmentPageViewModel viewModel;

        public TaxEvasionAttachmentPageView(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            InitializeComponent();
            viewModel = App.Locator.TaxEvasionAttachmentPageView;
            SetLTR();
            this.BindingContext = viewModel;
            // MainLayout.Padding = new Thickness(0, 0, 0, 0);
            ChangeAeroIcon();

            viewModel.selectedtaxEList = new TaxEvasionReportDetails();
            viewModel.selectedtaxEList = SelectedTaxEvasionListItem;

            viewModel.TaxEvasionReportTobeUsedToSubmit = SelectedTaxEvasionListItem;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel.UploadedDocumentsList = new UploadedDocumentsList();
        }
     
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ClearFields();

            SetDataToUI();

            if (viewModel.selectedtaxEList != null && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
                SetLocationToMap();
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
                    var timeout = TimeSpan.FromSeconds(4);

                    try
                    {
                        var locationRequestData = new GeolocationRequest(GeolocationAccuracy.Medium, timeout);

                        var location = await Geolocation.GetLocationAsync(locationRequestData);
                        if (location != null)
                        {
                            lat = location.Latitude;
                            lon = location.Longitude;
                        }

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
                        //Handle not enabled on device exception
                    }
                    catch (PermissionException pEx)
                    {
                        //Handle permission exception
                        Position position = new Position(lat, lon);
                        MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                        mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = lat;
                        viewModel.Longitude = lon;
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

        public void ClearFields()
        {
            try
            {
                //viewModel.SelectedTaxEvasionCompanyType = null;
                //viewModel.SelectedTaxEvasionRegion = null;
                //viewModel.SelectLCType = null;
                //viewModel.UploadedDocumentsListObj = null;
          

                viewModel.AttachmentCount = 0;

                viewModel.UploadedDocumentsListObj.Clear();
         
               
            }
            catch (Exception ex)
            {
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

            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;

            }
        }
  
        private void SetDataToUI()
        {
           // viewModel.SelectedCategory = viewModel.selectedtaxEList.Category;
            if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
           
         
                  Attachment_Label.IsVisible = true;
                  Attachment_Label.IsVisible = true;
                
                  Attachment_Tmg.IsVisible = true;
                  Attachment_Frm.IsVisible = true;
                  Attachment_Tmg.IsEnabled = true;
            
                //TFaciMobNo.IsEnabled = false;
                //TFaciMobNoAr.IsEnabled = false;
                //TFaciEmail.IsEnabled = false;
         
                viewModel.IsSubmitButtonEnable = true;
                submit_btnmane.IsEnabled = true;
                submit_btnmane.BackgroundColor = Color.Gray;
      
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
                mapView.IsEnabled = true;
            
            }
            else
           {

                if (viewModel.selectedtaxEList != null && viewModel.selectedtaxEList.PhoneNumber != null)
                {
                    string mobb = App.TaxEvasionUserData.Mobile;

                    if (mobb == null)
                        mobb = string.Empty;

                   // viewModel.TMobNumber = mobb;  
                }
            }
        }
        private async void mapView_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            if ((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId))
            {
                viewModel.IsLoading = false;
                //clearFields();
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        Position position = new Position(location.Latitude, location.Longitude);
                        MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                         //mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = location.Latitude;
                        //viewModel.TEReportobj.Latitude = location.Latitude.ToString();
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
        private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            UploadedDocumentsList attachment = (UploadedDocumentsList)arrowImage.BindingContext;
            viewModel.UploadedDocumentsListObj.Remove(attachment);
            viewModel.AttachmentCount = viewModel.AttachmentCount - 1;
            viewModel.AttachmentName = string.Empty;

        }
    }
}
