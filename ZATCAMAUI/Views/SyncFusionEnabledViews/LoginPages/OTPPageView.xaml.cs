using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OTPPageView : ContentPage
{

	OTPPageViewModel viewModel;
	
	public OTPPageView(Location location)
	{
        InitializeComponent();

		viewModel = App.Locator.OtpPageViewModel;
		this.BindingContext = viewModel;
		viewModel.IsShowMsgView = false;
		viewModel.UserLocation = location;
    }


	
}