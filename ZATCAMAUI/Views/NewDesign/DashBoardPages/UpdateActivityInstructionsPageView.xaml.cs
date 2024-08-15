using Mopups.Pages;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UpdateActivityInstructionsPageView : PopupPage
{
	public UpdateActivityInstructionsPageViewModel viewModel;
	public UpdateActivityInstructionsPageView(bool InstructionChecked, string respMessage)
	{
		InitializeComponent();
		viewModel = App.Locator.UpdateActivityInstructionsPage;
		this.BindingContext = viewModel;
		viewModel.IsInstructionChecked = InstructionChecked;
		viewModel.isInstructionCheckedEnable = !InstructionChecked;
		viewModel.responseMessage = respMessage;
	}
}