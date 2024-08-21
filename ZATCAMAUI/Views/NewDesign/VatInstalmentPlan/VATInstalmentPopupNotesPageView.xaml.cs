
using Mopups.Pages;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class VATInstalmentPopupNotesPageView : PopupPage
{
	VATInstalmentNotesPageViewModel viewModel;

	public VATInstalmentPopupNotesPageView()
	{
		InitializeComponent();
		viewModel = App.Locator.VATInstalmentPopupNotesPageView;
		this.BindingContext = viewModel;
		viewModel.MobileNumber = UtilityManager.MaskMobileNUmber(App.TP.mobile);


	}
	// Invoked when a hardware back button is pressed
	protected override bool OnBackButtonPressed()
	{
		// Return true if you don't want to close this popup page when a back button is pressed
		return true;
	}

	// Invoked when background is clicked
	protected override bool OnBackgroundClicked()
	{
		// Return false if you don't want to close this popup page when a background of the popup page is clicked
		return false;
	}

	void OtpFirstEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
	{
		if (viewModel.OTPFirstDigit.Length > 0)
		{
			OTPSecondEntry.Focus();
		}
	}

	void OtpSecondEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
	{
		if (viewModel.OTPSecondDigit.Length > 0)
		{
			OTPThirdEntry.Focus();
		}
	}

	void OtpThirdEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
	{
		if (viewModel.OTPThirdDigit.Length > 0)
		{
			OTPFourthEntry.Focus();
		}
	}
}