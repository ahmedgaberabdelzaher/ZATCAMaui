using ZATCAMAUI.Models.VATInstalmentModels;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;

public partial class VatInstalmentPlanRevokePageView : ContentPage
{

	#region Variable
	VATInstalmentPlanRevokeViewModel viewModel;

	#endregion
	public VatInstalmentPlanRevokePageView()
	{
		InitializeComponent();
		viewModel = App.Locator.VatInstalmentPlanRevokePageView;
		this.BindingContext = viewModel;
		_ = viewModel.GetVATInstalmentPlanList();
	}
	private void VATInstalmentRevokeList_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
	{

		try
		{
			var item = e.DataItem as VATRevokeUiListModel;



		}
		catch (Exception )
		{
		}
	}
}