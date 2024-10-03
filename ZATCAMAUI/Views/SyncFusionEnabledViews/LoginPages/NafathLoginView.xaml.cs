using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NafathLoginView : ContentPage
{
	NafathLoginViewModel viewModel;
	public NafathLoginView(string navigation)
	{
		InitializeComponent();
		viewModel = App.Locator.NafathLoginViewModel;
		Reset();
		viewModel.navigation = navigation;
		BindingContext = viewModel;

        DisplayLocationPermissionDilaogAsync();
    }

    private async Task DisplayLocationPermissionDilaogAsync()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();
                viewModel.LocationData.lattitude = location?.Latitude.ToString();
                viewModel.LocationData.longitude = location?.Longitude.ToString();
            }
            else
            {
                viewModel.LocationData.lattitude = "UNKNOWN";
                viewModel.LocationData.longitude = "UNKNOWN";
            }
        }
        catch (FeatureNotEnabledException )
        {
            viewModel.LocationData.lattitude = "UNKNOWN";
            viewModel.LocationData.longitude = "UNKNOWN";
        }
        catch (Exception)
        {

        }
    }

    private void Reset()
	{
		viewModel.NafathId = string.Empty;
		viewModel.Error = string.Empty;
	}
}