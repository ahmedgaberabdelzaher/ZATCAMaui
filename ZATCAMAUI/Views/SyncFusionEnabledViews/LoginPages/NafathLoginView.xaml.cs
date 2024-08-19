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
	}


	private void Reset()
	{
		viewModel.NafathId = string.Empty;
		viewModel.Error = string.Empty;
	}
}