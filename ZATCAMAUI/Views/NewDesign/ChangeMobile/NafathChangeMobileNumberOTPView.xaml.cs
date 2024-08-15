namespace ZATCAMAUI.Views.NewDesign.ChangeMobile;

public partial class NafathChangeMobileNumberOTPView : ContentPage
{
	public partial class NafathChangeMobileNumberOTPView : ContentPage
	{
		NafathChangeMobileNumberOTPViewModel viewModel;
		public NafathChangeMobileNumberOTPView(NafathChangeMobileNumberSendOTPResponse request)
		{
			InitializeComponent();
			viewModel = App.Locator.NafathChangeMobileNumberOTPViewModel;
			viewModel.Request = request;
			BindingContext = viewModel;
			viewModel.Reset();
		}


		void OtpFirstEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
		{
			if (viewModel.FirstDigit.Length > 0)
			{
				SecondDigitEntry.Focus();
			}
		}

		void OtpSecondEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
		{
			if (viewModel.SecondDigit.Length > 0)
			{
				ThirdDigitEntry.Focus();
			}
		}

		void OtpThirdEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
		{
			if (viewModel.ThirdDigit.Length > 0)
			{
				FourthDigitEntry.Focus();
			}
		}

		void OtpFourthEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
		{
			if (viewModel.ThirdDigit.Length > 0)
			{
				FifthDigitEntry.Focus();
			}
		}

		void OtpFifthEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
		{
			if (viewModel.ThirdDigit.Length > 0)
			{
				SixthDigitEntry.Focus();
			}
		}

		protected override void OnDisappearing()
		{
			viewModel.StopTimer();
			base.OnDisappearing();
		}
	}
}