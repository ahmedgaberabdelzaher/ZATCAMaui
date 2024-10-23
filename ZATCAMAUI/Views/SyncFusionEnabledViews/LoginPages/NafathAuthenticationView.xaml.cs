using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NafathAuthenticationView : ContentPage
{
	NafathAuthenticationViewModel viewModel;


	public NafathAuthenticationView(NafathLoginResponseModel model)
	{
		InitializeComponent();
		viewModel = App.Locator.NafathAuthenticationViewModel;
		setData(model);
		viewModel.navigation = model.navigation;
		BindingContext = viewModel;
	}


	private void setData(NafathLoginResponseModel model)
	{
		model.result.APICall = "2";
		viewModel.AuthenticationNumber = model.result.randomNumber;
		viewModel.Response = model.result;
	}


	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		viewModel._isTimerRepeatRequired = false;
	}
}