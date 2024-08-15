using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ZakatExemptionRequestListPageView : ContentPage
{
	private ZakatExemptionRequestListViewModel viewModel;

	public ZakatExemptionRequestListPageView()
	{
		InitializeComponent();

		viewModel = App.Locator.ZakatExemptionRequestListPageView;
		this.BindingContext = viewModel;

		Task.Run(() => this.viewModel.GetDetailsForZakatExeListAsync()).Wait();

		viewModel.PopulateStatusTypeList();
		this.BindingContext = viewModel;

	}


	protected async override void OnAppearing()
	{



		MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
		{
			viewModel.FilterWithStatus(viewModel.PickerModel.SelectedValue);

		});
	}



	private void searchButtonTapped(object sender, EventArgs e)
	{
		viewModel.IsSearchButtonVisible = false;
		viewModel.IsCloseButtonVisible = true;
	}
	private void filterButtonTapped(object sender, EventArgs e)
	{
		viewModel.FiltersClicked();
	}

	private void CloseSearchButton_Tapped(object sender, EventArgs e)
	{
		viewModel.IsSearchButtonVisible = true;
		viewModel.IsCloseButtonVisible = false;
		viewModel.SearchText = "";

		viewModel.CopiedZakatExemptionListViewData = viewModel.ZakatExemptionListViewData;
		if (searchBar != null)
		{
			searchBar.Text = "";
		}
	}

	private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{

		searchBar = (sender as SearchBar);


		var keyword = e.NewTextValue;
		try
		{
			viewModel.SearchText = searchBar.Text;
			viewModel.FilterWithReferenceNumber();
		}
		catch (Exception ex)
		{

		}
	}


}