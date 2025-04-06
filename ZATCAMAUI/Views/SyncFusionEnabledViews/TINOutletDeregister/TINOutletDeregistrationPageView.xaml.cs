using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;
using static ZATCAMAUI.Models.TINOutletDeregister.TinOutletPrevousRequestsModel;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;

public partial class TINOutletDeregistrationPageView : ContentPage
{
	TINOutletDeregistrationViewModel viewModel;

	public TINOutletDeregistrationPageView()
	{
		
		try
		{
            InitializeComponent();
            viewModel = App.Locator.TINOutletDeregistrationPageView;
			this.BindingContext = viewModel;

		}
		catch (Exception ex)
		{

		}

	}
	protected override void OnAppearing()
	{
		base.OnAppearing();

		try
		{
            Task.Run(() => this.viewModel.GetTinOutletDeregisteredRequests()).Wait();
            viewModel.IsSearchButtonVisible = true;
            viewModel.IsCloseButtonVisible = false;
            viewModel.SearchText = "";

            MessagingCenter.Subscribe<object, string>(this, "OutletDeregTappedforAcc", (sender, arg) =>
			{
				//viewModel.ClickSorted(arg);
				PreviousRequests item = new PreviousRequests();
				viewModel.NavigatingtoRequestPageView(item);
			});
		}
		catch (Exception )
		{
			
			

		}
	}
	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		MessagingCenter.Unsubscribe<object, string>(this, "OutletDeregTappedforAcc");
	}


	private void searchButtonTapped(object sender, EventArgs e)
	{
		viewModel.IsSearchButtonVisible = false;
		viewModel.IsCloseButtonVisible = true;
	}

	private void CloseSearchButton_Tapped(object sender, EventArgs e)
	{
		viewModel.IsSearchButtonVisible = true;
		viewModel.IsCloseButtonVisible = false;
		viewModel.SearchText = "";

		viewModel.CopiedPreviousRequestList = viewModel.PreviousRequestList;
		if (searchBar != null)
		{
			searchBar.Text = "";
		}
	}

	private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{

		searchBar = (sender as SearchBar);


		var keyword = e.NewTextValue;
	}

}