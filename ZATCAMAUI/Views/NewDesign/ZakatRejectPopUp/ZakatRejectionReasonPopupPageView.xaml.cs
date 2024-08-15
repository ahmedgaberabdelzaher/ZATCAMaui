using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.ZakatRejectPopUp;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ZakatRejectionReasonPopupPageView : PopupPage
{
	public ZakatRejectionReasonPopupViewModel viewModel;
	public delegate void OnSelectDelegate(string item);
	public OnSelectDelegate OnSelect { get; set; } = null;
	public ZakatRejectionReasonPopupPageView()
	{
		InitializeComponent();
		viewModel = App.Locator.ZakatRejectionReasonPopupPageView;
		this.BindingContext = viewModel;
		viewModel.RejectReasonText = string.Empty;
	}

	private async void CancelButtonClicked(object sender, System.EventArgs e)
	{
		MessagingCenter.Send<Object, string>(this, "RejectCancelled", "No");
		OnSelect?.Invoke("No");
		await MopupService.Instance.PopAsync();
	}

	private async void RejectButtonClicked(object sender, System.EventArgs e)
	{
		MessagingCenter.Send<Object, string>(this, "Reject", viewModel.RejectReasonText);
		OnSelect?.Invoke("Yes");
		await MopupService.Instance.PopAsync();
	}
}