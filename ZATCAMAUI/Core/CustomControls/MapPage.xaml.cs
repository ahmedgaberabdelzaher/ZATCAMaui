using Maui.GoogleMaps;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport;

namespace ZATCAMAUI.Controls
{
    public partial class MapPage : PopupPage
    {
        SubmitReportViewModel viewModel;
        public MapPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.SubmitReportViewModel;
                BindingContext = viewModel;
                viewModel.GoogleMap = map;
            }
            catch (Exception)
            {

            }
        }
        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return true;
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        async void map_MapLongClicked(System.Object sender, MapLongClickedEventArgs e)
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (statusLocationAlways == PermissionStatus.Granted || statusLocationWhenInUse == PermissionStatus.Granted)
                {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(2));
                    var location = await Geolocation.GetLocationAsync(request);
                    viewModel.SubmitReport.Latitude = location.Latitude;
                    viewModel.SubmitReport.Longitude = location.Longitude;
                    Geocoder geoCoder = new Geocoder();

                    Position position = new Position(location.Latitude, location.Longitude);

                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                    viewModel.SubmitReport.CompanyAddress = possibleAddresses.FirstOrDefault();

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
            catch (System.Exception)
            {

            }
        }

        async void map_MapClicked(System.Object sender, MapClickedEventArgs e)
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (statusLocationAlways == PermissionStatus.Granted || statusLocationWhenInUse == PermissionStatus.Granted)
                {
                    var location = e.Point;
                    viewModel.SubmitReport.Latitude = location.Latitude;
                    viewModel.SubmitReport.Longitude = location.Longitude;
                    Geocoder geoCoder = new Geocoder();

                    Position position = e.Point;

                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                    viewModel.SubmitReport.CompanyAddress = possibleAddresses.FirstOrDefault();

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
            catch (System.Exception)
            {

            }
        }

    }
}

