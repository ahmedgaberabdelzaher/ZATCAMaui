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
            InitializeComponent();
            viewModel = App.Locator.submitReportViewModel;
        }

        async void map_MapClicked(System.Object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (statusLocationAlways == PermissionStatus.Granted || statusLocationWhenInUse == PermissionStatus.Granted)
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    Geocoder geoCoder = new Geocoder();

                    Position position = new Position(location.Latitude, location.Longitude);
                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    viewModel.Street = possibleAddresses.FirstOrDefault();
                    var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    string address = placemark?.SubThoroughfare ?? placemark?.Thoroughfare ?? placemark?.SubAdminArea ?? placemark?.AdminArea;
       
                    map?.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(position.Latitude, position.Longitude), Distance.FromMiles(2)));
                    map?.Pins.Clear();
                    map?.Pins.Add(new Pin()
                    {
                        Address = address,
                        Label = address,
                        Position = new Position(position.Latitude, position.Longitude)
                    });

                }
                else
                {
                    await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    
                }
               

                

            }
            catch (System.Exception)
            {

            }
        }

    }
}

