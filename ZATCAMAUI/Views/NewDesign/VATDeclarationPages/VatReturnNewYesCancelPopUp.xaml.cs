using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class VatReturnNewYesCancelPopUp : PopupPage
{
	string _confirmationText = string.Empty;
	public delegate void OnSelectDelegate(string item);
	public OnSelectDelegate OnSelect { get; set; } = null;
	public VatReturnNewYesCancelPopUp(string ConfirmationText)
	{
		InitializeComponent();
		_confirmationText = confirmationText.Text = ConfirmationText;
	}

	private async void OnOkayButtonClicked(object sender, EventArgs e)
	{
		MessagingCenter.Send<Object, string>(this, "OkayToSubmit", "Yes");
		OnSelect?.Invoke("Yes");
		await MopupService.Instance.PopAsync();

	}

	private async void OnCancelClicked(object sender, EventArgs e)
	{
		MessagingCenter.Send<Object, string>(this, "NoToCancel", "No");
		OnSelect?.Invoke("No");
		await MopupService.Instance.PopAsync();

	}

}