using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber;

namespace ZATCAMAUI.Views.NewDesign.ChangeMobile;

public partial class NafathChangeMobileNumberSuccessView : ContentPage
{
	NafathChangeMobileNumberSuccessViewModel viewModel { get; set; }
	public NafathChangeMobileNumberSuccessView()
	{
		InitializeComponent();
		viewModel = App.Locator.NafathChangeMobileNumberSuccessViewModel;
		BindingContext = viewModel;
	}

}