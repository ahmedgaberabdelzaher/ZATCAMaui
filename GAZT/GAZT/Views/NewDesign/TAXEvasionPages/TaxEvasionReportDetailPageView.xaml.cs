using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportDetailPageView : ContentPage
    {
        TaxEvasionReportDetailPageViewModel viewModel;
        double Latitude = 24.774265;
        double Logitude = 46.738586;
        public TaxEvasionReportDetailPageView(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionReportDetailPageView;
            this.BindingContext = viewModel;
            viewModel.SelectedTaxEvasionListItem = SelectedTaxEvasionListItem;
            SetLTR();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SetLocationToMap();
        }
        private async void SetLocationToMap()
        {
            try
            {
                //if (!((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.TicketId)))
                //{
                //    viewModel.IsLoading = false;
                //    //clearFields();
                //}
                //else
                //{
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
                    if (viewModel.SelectedTaxEvasionListItem != null)
                    {
                        if (viewModel.SelectedTaxEvasionListItem.Location != null)
                        {
                           
                            string[] Reportlocation = viewModel.SelectedTaxEvasionListItem.Location.Split(',');
                            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
                            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
                            numberFormat.NumberDecimalSeparator = ".";

                            double number = double.Parse("22.1", numberFormat);
                            lat = Convert.ToDouble(Reportlocation[0]);
                            //lat = double.Parse(Reportlocation[0], numberFormat);
                            //lon = double.Parse(Reportlocation[1], numberFormat);
                            lon = Convert.ToDouble(Reportlocation[1]);

                        }
                        if (viewModel.SelectedTaxEvasionListItem.Latitude != null)
                        {
                           
                          
                        }


                    }

                    Position position = new Position(lat, lon);
                    MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                    mapView.MoveToRegion(mapSpan);
                    Pin pin = new Pin();
                    pin.Label = "Report Location";
                    pin.Type = PinType.Place;
                    pin.Position = position;
                    mapView.Pins.Add(pin);//new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    //var addrs = (await Geocoding.GetPlacemarksAsync(new Location(location.Latitude, location.Longitude))).FirstOrDefault();
                    //viewModel.RLocation = addrs.Thoroughfare + " " + addrs.SubThoroughfare + "," + addrs.Locality + "," + addrs.CountryName + "-" + addrs.PostalCode;
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
                //}
            }
            catch (Exception ex)
            {
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

    }
}