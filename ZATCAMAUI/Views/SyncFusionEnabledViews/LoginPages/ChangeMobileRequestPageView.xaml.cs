using System.Text;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChangeMobileRequestPageView : ContentPage
{
	ChangeMobileRequestViewModel viewModel;
	

	public ChangeMobileRequestPageView(Dictionary<string, string> d)
	{
        InitializeComponent();
        this.BindingContext = viewModel = App.Locator.ChangeMobileRequestPageView;
        viewModel.d = d;
    }

	

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");
		MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "SelectedItem");
		MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");

		viewModel.StopTimer();
	}

	private void CountryCodeTapped(object sender, EventArgs e)
	{
		MopupService.Instance.PushAsync(new InternationalCodeSearchPage(viewModel.CountryCodesList));
	}

	private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
	{
		StringBuilder Message = new StringBuilder();
		PopUp popUp = new PopUp();
		if (!string.IsNullOrWhiteSpace(EntryMobileNumber.Text))
		{

			if (EntryMobileNumber.Text.Substring(0, 1) == "0")
			{
				Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0 + " ");
			}
			if (viewModel.TxtCountryCode == "+966")
			{
				if (EntryMobileNumber.Text.Substring(0, 1) != "5")
				{
					Message.AppendLine(AppResources.ZZMobilenumberhastostartwithnumber5);
				}
			}
			if (EntryMobileNumber.Text.Length < 9)
			{

				Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
			}
			if (Message.Length > 0)
			{
				popUp.Message = Message.ToString();
				popUp.IsLinkAvailable = false;
				if (App.IsArabic)
				{
					popUp.FlowDirections = "RightToLeft";
					popUp.isFontSet = true;
				}
				else
				{
					popUp.FlowDirections = "LeftToRight";
				}
				MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
				viewModel.IsAllValidContactDataEnteredMobileNbr = false;

				EntryMobileNumber.Text = string.Empty;
			}
			else
			{
				viewModel.IsAllValidContactDataEnteredMobileNbr = true;
				viewModel.TxtMobileNumberwithCountryCode = "(" + viewModel.TxtCountryCode + ")" + " " + EntryMobileNumber.Text;
			}
		}
		else
		{
			Message.AppendLine(AppResources.EnterMobileNumber);
			popUp.Message = Message.ToString();
			MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
		}

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

	async void TapRentDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
	{
		Image item = sender as Image;
		Attachment data = item.BindingContext as Attachment;
		string QuestionMark = string.Empty;
		if (App.IsArabic)
		{
			QuestionMark = "؟";
		}
		else
		{
			QuestionMark = "?";
		}
		var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + " " + data.Filename + QuestionMark);
		confirmPopup.OnSelect =async (str) =>
		{
			if (str == "Yes")
			{
				await viewModel.OnRentAttachmentDeleteButtonTapped(data);
			}
		};
		await MopupService.Instance.PushAsync(confirmPopup);
	}

	void PhoneNumberTextChanged(System.Object sender, TextChangedEventArgs e)
	{
		if (viewModel.DissableSendOtp == false)
		{
			viewModel.DissableSendOtp = true;
			viewModel.ShowOTPSection = false;
			viewModel.StopTimer();
		}
	}

}