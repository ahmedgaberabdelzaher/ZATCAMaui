using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber;

namespace ZATCAMAUI.Views.NewDesign.ChangeMobile;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.OnAppearing();
        FirstDigitEntry.Focus();
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
        else if (viewModel.SecondDigit.Length == 0)
        {
            FirstDigitEntry.Focus();
        }
    }

    void OtpThirdEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        if (viewModel.ThirdDigit.Length > 0)
        {
            FourthDigitEntry.Focus();
        }
        else if (viewModel.ThirdDigit.Length == 0)
        {
            SecondDigitEntry.Focus();
        }
    }

    void OtpFourthEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        if (viewModel.FourthDigit.Length > 0)
        {
            FifthDigitEntry.Focus();
        }
        else if (viewModel.FourthDigit.Length == 0)
        {
            ThirdDigitEntry.Focus();
        }
    }

    void OtpFifthEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        if (viewModel.FifthDigit.Length > 0)
        {
            SixthDigitEntry.Focus();
        }
        else if (viewModel.FifthDigit.Length == 0)
        {
            FourthDigitEntry.Focus();
        }
    }

    protected override void OnDisappearing()
    {
        viewModel.StopTimer();
        base.OnDisappearing();
    }
}