using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Application = Microsoft.Maui.Controls.Application;

public enum IsComingFromScreen
{
    VATAmendReactivation = 0
}

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewAccountPopUpPageView : PopupPage
    {
        NewAccountPopUpPageViewModel viewModel;
        public NewAccountPopUpPageView(string Iban)
        {
            InitializeComponent();
            viewModel = App.Locator.NewAccountPopUpPageView;

            NewAccountPopUpPageViewModel.ValidTypeIban = string.Empty;
            this.BindingContext = viewModel;
            viewModel.IbanNumberText = Iban;
            viewModel.CloseButtonVisible = true;
            string SAremovedIban = Iban.Replace("SA", string.Empty);
            if (string.IsNullOrEmpty(viewModel.IbanNumberText))
            {
                viewModel.AccountText = AppResources.ZTERNewAccount;
            }
            else
            {
                try
                {
                    viewModel.AccountText = AppResources.VATREditAccount;
                    viewModel.IbanPartOne = SAremovedIban.Substring(0, 2);
                    viewModel.IbanPartTwo = SAremovedIban.Substring(2, 8);
                    viewModel.IbanPartThree = SAremovedIban.Substring(9, 4);
                    viewModel.IbanPartFour = SAremovedIban.Substring(13, 4);
                    viewModel.IbanPartFive = SAremovedIban.Substring(17, 4);
                }
                catch (Exception)
                {
                    
                    
                }

                //Bind Iban and remove name
            }
            SetLTR();
        }

        public NewAccountPopUpPageView(String Iban, IsComingFromScreen isComingFromScreen)
        {
            InitializeComponent();
            viewModel = App.Locator.NewAccountPopUpPageView;
            On<iOS>().SetUseSafeArea(true);
            NewAccountPopUpPageViewModel.ValidTypeIban = string.Empty;
            this.BindingContext = viewModel;
            viewModel.IbanNumberText = Iban;
            string SAremovedIban = Iban.Replace("SA", string.Empty);
            if (string.IsNullOrEmpty(viewModel.IbanNumberText))
            {
                viewModel.AccountText = AppResources.ZTERNewAccount;

                viewModel.IbanPartOne = string.Empty;
                viewModel.IbanPartTwo = string.Empty;
                viewModel.IbanPartThree = string.Empty;
                viewModel.IbanPartFour = string.Empty;
                viewModel.IbanPartFive = string.Empty;
            }
            else
            {
                try
                {
                    viewModel.AccountText = AppResources.VATREditAccount;
                    viewModel.IbanPartOne = SAremovedIban.Substring(0, 2);
                    viewModel.IbanPartTwo = SAremovedIban.Substring(2, 8);
                    viewModel.IbanPartThree = SAremovedIban.Substring(9, 4);
                    viewModel.IbanPartFour = SAremovedIban.Substring(13, 4);
                    viewModel.IbanPartFive = SAremovedIban.Substring(17, 4);
                }
                catch (Exception)
                {
                    
                    
                }

                //Bind Iban and remove name
            }

            if (isComingFromScreen == IsComingFromScreen.VATAmendReactivation)
            {
                viewModel.CloseButtonVisible = true;
                btnDone.IsVisible = true;

            }
            else
            {
                viewModel.CloseButtonVisible = false;
                btnDone.IsVisible = true;

            }
            SetLTR();
        }

        private async void Checked_IBAN()
        {
            try
            {
                try
                {
                    var response = WebServiceManager.GAZTCheckIBAN(viewModel.IbanNumberText);
                    if (response != null)
                    {
                        viewModel.IsIBANValid = true;
                        NewAccountPopUpPageViewModel.ValidTypeIban = viewModel.IbanNumberText;
                        MessagingCenter.Send<Object, string>(this, "IbanReceived", viewModel.IbanNumberText);
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        NewAccountPopUpPageViewModel.ValidTypeIban = string.Empty;
                        viewModel.IsIBANValid = false;
                        if (viewModel.IbanNumberText == "SA")
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            }
            catch (Exception)
            {
                
                
                viewModel.IsIBANValid = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                });
            }
        }

        private void SetLTR()
        {
            try
            {
                if (App.IsArabic)
                {
                    //this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
                }
                else
                {
                    //this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
                }
            }
            catch (Exception gec)
            {
            }
        }

        private void IbanAddButtonClicked(object sender, EventArgs e)
        {
            viewModel.IbanNumberText = string.Empty;
            viewModel.IbanNumberText = "SA" + viewModel.IbanPartOne + viewModel.IbanPartTwo + viewModel.IbanPartThree + viewModel.IbanPartFour + viewModel.IbanPartFive;
            Checked_IBAN();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {

            if (viewModel.IsIBANValid)
            {
                PopupNavigation.Instance.PopAsync();

            }
            else
            {
                Checked_IBAN();

            }

        }

        private async void IbanOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                if (!string.IsNullOrEmpty(viewModel.IbanPartFive))
                {
                    await Task.Delay(1);
                    IbanFive.Focus();
                }
                else if (!string.IsNullOrEmpty(viewModel.IbanPartFour))
                {
                    await Task.Delay(1);
                    IbanFour.Focus();
                }
                else if (!string.IsNullOrEmpty(viewModel.IbanPartThree))
                {
                    await Task.Delay(1);
                    IbanThree.Focus();
                }
                else if (!string.IsNullOrEmpty(viewModel.IbanPartTwo))
                {
                    await Task.Delay(1);
                    IbanTwo.Focus();
                }
            }
            else if (e.NewTextValue.ToCharArray().Count() == ((GAZTBorderlessEntry)sender).MaxLength)
            {
                await Task.Delay(1);
                IbanTwo.Focus();
            }
        }

        private async void IbanTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await Task.Delay(1);
                IbanOne.Focus();
            }
            else if (e.NewTextValue.ToCharArray().Count() == ((GAZTBorderlessEntry)sender).MaxLength)
            {
                await Task.Delay(1);
                IbanThree.Focus();
            }
        }

        private async void IbanThree_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await Task.Delay(1);
                IbanTwo.Focus();
            }
            else if (e.NewTextValue.ToCharArray().Count() == ((GAZTBorderlessEntry)sender).MaxLength)
            {
                await Task.Delay(1);
                IbanFour.Focus();
            }
        }

        private async void IbanFour_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await Task.Delay(1);
                IbanThree.Focus();
            }
            else if (e.NewTextValue.ToCharArray().Count() == ((GAZTBorderlessEntry)sender).MaxLength)
            {
                await Task.Delay(1);
                IbanFive.Focus();
            }
        }

        private async void IbanFive_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await Task.Delay(1);
                IbanFour.Focus();
            }
            else if (e.NewTextValue.ToCharArray().Count() == ((GAZTBorderlessEntry)sender).MaxLength)
            {
                IbanFive.Unfocus();
            }
        }

        private async void IbanOne_Completed(object sender, EventArgs e)
        {
            await Task.Delay(1);
            IbanTwo.Focus();
        }

        private void CloseIban_Tapped(object sender, EventArgs e)
        {
            viewModel.IbanPartOne = string.Empty;
            viewModel.IbanPartTwo = string.Empty;
            viewModel.IbanPartThree = string.Empty;
            viewModel.IbanPartFour = string.Empty;
            viewModel.IbanPartFive = string.Empty;
            PopupNavigation.Instance.PopAsync();
        }
    }
}