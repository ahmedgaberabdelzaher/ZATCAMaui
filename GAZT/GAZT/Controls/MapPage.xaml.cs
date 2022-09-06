using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace EGAZT.Controls
{
    public partial class MapPage : ContentView
    {
        SubmitReportViewModel viewModel;
        public MapPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.SubmitReportViewModel;
                viewModel.GoogleMap = map;
            }
            catch (Exception ex)
            {

            }
        }
       async void map_MapLongClicked(System.Object sender, Xamarin.Forms.GoogleMaps.MapLongClickedEventArgs e)
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (statusLocationAlways == PermissionStatus.Granted || statusLocationWhenInUse == PermissionStatus.Granted)
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    viewModel.SubmitReport.Latitude = location.Latitude;
                    viewModel.SubmitReport.Longitude = location.Longitude;
                    Geocoder geoCoder = new Geocoder();

                    Position position = new Position(location.Latitude, location.Longitude);

                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                    viewModel.SubmitReport.Street = possibleAddresses.FirstOrDefault();

                    viewModel.SubmitReport.Location = $"{viewModel.SubmitReport.Latitude},{viewModel.SubmitReport.Longitude},{possibleAddresses.FirstOrDefault()}";

                    //var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                    //var placemark = placemarks?.FirstOrDefault();
                    //string address = placemark?.SubThoroughfare ?? placemark?.Thoroughfare ?? placemark?.SubAdminArea ?? placemark?.AdminArea;

                    var zoomLevel = 10.71; // pick a value between 1 and 18
                    var latlongdeg = 360 / (Math.Pow(2, zoomLevel));

                    map?.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(position.Latitude, position.Longitude), Distance.FromMiles(latlongdeg)));

                    map?.Pins.Clear();

                    map?.Pins.Add(new Pin()
                    {
                        Address = possibleAddresses.FirstOrDefault(),
                        Label = possibleAddresses.FirstOrDefault(),
                        Position = new Position(position.Latitude, position.Longitude)
                    });

                }
                else
                {
                    await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                }




            }
            catch (System.Exception ex)
            {

            }
        }
    }
}

