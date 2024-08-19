using AppDynamics.Agent;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OTPPageView : ContentPage
{

	OTPPageViewModel viewModel;
	private TimeSpan remainingTime = TimeSpan.FromMinutes(2);
	private bool isTimeRemaining = false;
	public OTPPageView()
	{
		InitializeComponent();

		viewModel = App.Locator.OtpPageViewModel;
		this.BindingContext = viewModel;
		Step1.Focus();
		Step2.IsEnabled = true;
		Step3.IsEnabled = true;
		Step4.IsEnabled = true;
		ResendCodeText.IsEnabled = false;
		StartTimer();
		MessagingCenter.Subscribe<object, string>(this, "LanguageUpdate", (sender, arg) =>
		{
			SetTranslations();
		});
		SetTranslations();

	}

	private void SetTranslations()
	{

		this.VerificationCodeTitle.Text = AppResources.VerificationCodeTitle;
		this.OTPPageDescription.Text = AppResources.OTPPageDescription;
		this.OTPErrorMsg.Text = AppResources.OTPScreenErrMsg;
		this.MobileNumber.Text = AppResources.MobileNumberText;
		this.AccountLockedText.Text = AppResources.AccountLockedMsg;
		this.ResendCodeText.Text = AppResources.ResendVerificationCodeMsg;
		this.MobileNumberTxt.Text = App.MobileNumber;
	}






	private void StartTimer()
	{
		isTimeRemaining = true;
		Device.StartTimer(TimeSpan.FromSeconds(1), () =>
		{
			remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
			if (remainingTime.TotalSeconds <= -1)
			{
				StopTimer();
				return false;
			}
			UpdateTimerLabel();
			return true;
		});
	}

	private void StopTimer()
	{
		ResendCodeText.TextColor = Colors.White;
		ResendCodeText.IsEnabled = true;
		isTimeRemaining = false;
	}

	private void UpdateTimerLabel()
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			ResendTimer.Text = remainingTime.ToString(@"mm\:ss");
		});
	}

	private async void Step1_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue.Length == 1)
		{
			if (string.IsNullOrEmpty(Step2.Text))
			{
				Step2.IsEnabled = true;
				Step2.Focus();
			}

		}
	}

	private async void Step2_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue.Length == 1)
		{
			if (string.IsNullOrEmpty(Step3.Text))
			{
				Step3.Focus();
				Step3.IsEnabled = true;
			}

		}

		if (e.NewTextValue.Length == 0)
		{
			Step1.Focus();
			Step1.Text = string.Empty;

		}
	}

	private async void Step3_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue.Length == 1)
		{
			Step4.Focus();
			Step4.IsEnabled = true;


		}

		if (e.NewTextValue.Length == 0)
		{
			Step2.Focus();
			Step2.Text = string.Empty;

		}
	}

	private async void Step4_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue.Length == 1)
		{
			var tokenRequestModel = new TokenRequestModel() { Token = App.Token, Lang = "en", OTP = Step1.Text + Step2.Text + Step3.Text + Step4.Text, SourceType = "M", OsName = "android", BrowserName = "chrome" };
			await viewModel.TokenPostRequest(tokenRequestModel);

		}

		if (e.NewTextValue.Length == 0)
		{
			Step3.Focus();
			Step3.Text = string.Empty;
		}
	}



	public async Task LoginCompletedInWebView()
	{
		string response = string.Empty;
		string UserId = App.LoginDataRetrieved.TIN;

		Instrumentation.SetUserData("user_id", UserId);

		//string lang = "E";
		string language = UtilityManager.GetLanguageParameter();
		// * OLD TP PROFILE API
		//TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

		// * NEW TP PROFILE API
		TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);

		if (TPProfile != null)
		{
			//if ((0 == string.Compare("Registration is pending", TPProfile.TpType)))
			//{
			//    throw new GAZTRegistrationPendingException();
			//}

			App.TP = new TaxPayerProfile();
			App.TP = TPProfile;
			App.TP.userId = TPProfile.TIN;
			try
			{
				if (App.LoginDataRetrieved != null)
				{
					if (App.TP != null)
					{
						App.TP.firstName = App.LoginDataRetrieved.NameFirst;
						App.TP.lastName = App.LoginDataRetrieved.NameLast;
						App.TP.organizationName = App.LoginDataRetrieved.NameOrg1;
						App.TP.typeCheck = App.LoginDataRetrieved.TypeChk;
					}

				}

			}
			catch (Exception)
			{
			}

		}

		string OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
		string OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

		await Task.Run(() =>
		{
			try
			{

				App.IsLoginCalled = true;
				App.ArePreLoginLangCookiesSet = false;

				MainThread.BeginInvokeOnMainThread(() =>
				{
					//_navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
					App.HasToRefreshLoaderOnDashboard = true;
				});

			}

			catch (Exception)
			{

			}

		});

	}

	private void Resent_Tapped(object sender, EventArgs e)
	{
		if (!isTimeRemaining)
		{
			viewModel.ResendToken().ConfigureAwait(false);
			ResendCodeText.TextColor = Colors.Gray;
			ResendCodeText.IsEnabled = false;
			Step1.Text = Step2.Text = Step3.Text = Step4.Text = string.Empty;
			remainingTime = TimeSpan.FromMinutes(2);
			StartTimer();
		}
	}
}