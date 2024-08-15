using Syncfusion.Maui.DataSource;

namespace ZATCAMAUI.Views.NewDesign.UpdateVatEffectiveDate;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UpdateVatEffectiveDatePageView : ContentPage
{
	private UpdateVatEffectiveDateViewModel viewModel;
	public UpdateVatEffectiveDatePageView()
	{
		InitializeComponent();

		viewModel = App.Locator.UpdateVatEffectiveDateView;
		this.BindingContext = viewModel;
		viewModel.GetAllVatEffectiveDateLogs();
	}

	public void SortListInAscendingOrder()
	{
		EffectiveDateListView.DataSource.SortDescriptors.Add(new SortDescriptor()
		{
			PropertyName = "EffDtAfter",
			Direction = ListSortDirection.Ascending,
		});
		EffectiveDateListView.RefreshView();
	}
	public void SortListInDescendingOrder()
	{

		EffectiveDateListView.DataSource.SortDescriptors.Add(new SortDescriptor()
		{
			PropertyName = "EffDtAfter",
			Direction = ListSortDirection.Descending,
		});
		EffectiveDateListView.RefreshView();
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

		viewModel.CopiedVatLogs = viewModel.VatLogs;
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
		catch (Exception)
		{

		}
	}

}