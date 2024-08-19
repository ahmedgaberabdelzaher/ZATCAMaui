using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

public partial class AccountLockedPageView : ContentPage
{
	AccountLockedViewModel viewModel;
	public AccountLockedPageView()
	{
		InitializeComponent();
		viewModel = App.Locator.AccountLockedViewModel;
		this.BindingContext = viewModel;
	}



}