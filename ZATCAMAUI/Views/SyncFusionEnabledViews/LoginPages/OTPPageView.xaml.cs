using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OTPPageView : ContentPage
{

	OTPPageViewModel viewModel;
	
	public OTPPageView()
	{
        InitializeComponent();

		viewModel = App.Locator.OtpPageViewModel;
		this.BindingContext = viewModel;

	}


	
}