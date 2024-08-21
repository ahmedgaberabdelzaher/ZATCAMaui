using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM.FilterVatEffectiveDatePageViewModel;

namespace ZATCAMAUI.Views.NewDesign.UpdateVatEffectiveDate;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FilterVatEffectiveDatePageView : ContentPage
{
	FilterVatEffectiveDatePageViewModel viewModel;
	public FilterVatEffectiveDatePageView()
	{
		InitializeComponent();
		viewModel = App.Locator.FilterVatEffectiveDatePageView;
		this.BindingContext = viewModel;

	}

	protected override void OnAppearing()
	{

		base.OnAppearing();

		MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
		{

			VatEffectDateFilterModel filter1 = new VatEffectDateFilterModel();
			VatEffectDateFilterModel filter2 = new VatEffectDateFilterModel();

			viewModel.PickerModel = arg;
			var selectedType = string.Empty;
			string SelectedIDTypeValue = string.Empty;
			if (arg.PickerId == "DateSortTypePicker")
			{
				viewModel.SelectedDateSortText = arg.SelectedValue;
				var dateTypeMatch = viewModel.FilterList.RemoveAll(selectedValue => (selectedValue.filterId == 1));
				filter1.filterId = 1;
				filter1.filterType = "DateType";
				filter1.filterName = arg.SelectedValue;
				viewModel.FilterList.Add(filter1);

			}
			else if (arg.PickerId == "UpdatedBySortTypePicker")
			{
				viewModel.SelectedUpdatedSortText = arg.SelectedValue;
				var updateByTypeMatch = viewModel.FilterList.RemoveAll(selectedValue => (selectedValue.filterId == 2));
				filter2.filterId = 2;
				filter2.filterType = "UpdatedByType";
				filter2.filterName = arg.SelectedValue;
				viewModel.FilterList.Add(filter2);

			}
		});
	}


	protected override void OnDisappearing()
	{
		base.OnDisappearing();

		MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

	}
}