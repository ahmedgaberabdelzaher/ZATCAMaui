using System.Collections.ObjectModel;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements;

public partial class AccountsStatementObjectionDetailsPage : ContentPage
{
	AccountStatementObjectiondetailVM viewModel;
	private AccoungtDetails accoungt;

	public AccountsStatementObjectionDetailsPage(string NameStat, AccoungtDetails accoungt1)
	{
		viewModel = App.Locator.AccPageObjDetailVM;

		this.BindingContext = viewModel;
		accoungt = accoungt1;
		this.viewModel.NAmeof = NameStat;
		viewModel.accountDetails = accoungt1;
		InitializeComponent();
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();

		viewModel.isObjectionDetailsVisible = false;
		viewModel.isReturnsVisible = false;
		viewModel.isInstalmentDetailsVisible = false;
		viewModel.isBIllDetialsVisble = false;

		// CR1265 
		viewModel.oBJDTLSets = new ObservableCollection<Result_Obj>(viewModel.accountDetails.d.OBJ_DTLSet);
		viewModel.RETDTLSets = new ObservableCollection<Result_RET>(viewModel.accountDetails.d.RET_DTLSet);
		viewModel.instDTLSET = new ObservableCollection<Result_InST>(viewModel.accountDetails.d.INSTL_DTLSet);
		viewModel.billDetails = new ObservableCollection<Result_Bill>(viewModel.accountDetails.d.BILL_DTLSet);

		if (this.viewModel.NAmeof == "Return Details")
		{
			viewModel.isReturnsVisible = true;
			viewModel.isObjectionDetailsVisible = false;
			viewModel.isInstalmentDetailsVisible = false;
			viewModel.isBIllDetialsVisble = false;
		}
		else if (viewModel.oBJDTLSets.Count != 0 && (this.viewModel.NAmeof == "Objection Details"))
		{
			viewModel.isObjectionDetailsVisible = true;
			viewModel.isReturnsVisible = false;
			viewModel.isInstalmentDetailsVisible = false;
			viewModel.isBIllDetialsVisble = false;
		}
		else if (this.viewModel.NAmeof == "Instalment Plan Details")
		{
			viewModel.isInstalmentDetailsVisible = true;
			viewModel.isReturnsVisible = false;
			viewModel.isObjectionDetailsVisible = false;
			viewModel.isBIllDetialsVisble = false;
		}
		else if (this.viewModel.NAmeof == "Instalment Plan Orginal Bill Details")
		{
			viewModel.isBIllDetialsVisble = true;
			viewModel.isReturnsVisible = false;
			viewModel.isObjectionDetailsVisible = false;
			viewModel.isInstalmentDetailsVisible = false;
		}
		else
		{
			viewModel.isObjectionDetailsVisible = false;
			viewModel.isReturnsVisible = false;
			viewModel.isInstalmentDetailsVisible = false;
			viewModel.isBIllDetialsVisble = false;
		}

	}
}