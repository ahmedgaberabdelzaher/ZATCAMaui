
using Mopups.Pages;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;

public partial class VATInstalmentPopupRevokePageView : PopupPage
{
	VATInstalmentPlanListViewModel viewModel;
	public VATInstalmentPopupRevokePageView()
	{
		InitializeComponent();
		viewModel = App.Locator.VatInstalmentPlanListPageView;
		this.BindingContext = viewModel;
		viewModel.GetVATRevokeList();
	}



	private async void RevokeListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
	{


		var item = e.DataItem as VATRevokeUiListModel;

		var selectedItemFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, item.Fbnum, App.LoginDataRetrieved.TIN, item.Fbust, "VTIA");

		if (selectedItemFormID.d != null)
		{

			viewModel.VatRevokeResponse = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentPlanDetails("", selectedItemFormID.d.Fbguid);

			if (viewModel.VatRevokeResponse != null && viewModel.VatRevokeResponse.d != null)
			{
				viewModel.PopulateSummaryReasonData(viewModel.VatRevokeResponse);
				viewModel.BtnSetDetails = await VATInstalationPlanWebServiceManager.GAZTGetVATRevokeBtnSet(Fbtypz: "VTIR", Fbnum: viewModel.VatRevokeResponse.d.Fbnumz,
					Formproc: viewModel.VatRevokeResponse.d.Formprocz, Status: viewModel.VatRevokeResponse.d.Statusz, TxnTp: viewModel.VatRevokeResponse.d.TxnTpz);
				viewModel.EnableVAtInstalmentSummary();
				viewModel.EnableRevokeButtons();
				await viewModel.CallCaptchaApiAsync(selectedItemFormID.d.Fbguid);
			}
			else
			{

			}


		}

	}

}