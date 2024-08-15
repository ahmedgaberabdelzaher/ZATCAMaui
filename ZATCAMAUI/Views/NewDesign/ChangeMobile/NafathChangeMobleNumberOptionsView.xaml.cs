namespace ZATCAMAUI.Views.NewDesign.ChangeMobile;

public partial class NafathChangeMobleNumberOptionsView : PopupPage
{
	NafathChangeMobileNumberOptionsViewModel viewModel;
	public NafathChangeMobleNumberOptionsView()
	{
		InitializeComponent();
		viewModel = App.Locator.NafathChangeMobileNumberOptionsViewModel;
		BindingContext = viewModel;
	}
}