using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAccountPopPage : PopupPage
    {
        public NewAccountPopPageViewModel viewModel;
        public NewAccountPopPage(String Iban)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.NewAccountPopPageView;
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
                    viewModel.AccountText = AppResources.VATREditAccount;
                    viewModel.IbanPartOne = SAremovedIban.Substring(0, 2);
                    viewModel.IbanPartTwo = SAremovedIban.Substring(2, 8);
                    viewModel.IbanPartThree = SAremovedIban.Substring(10, 4);
                    viewModel.IbanPartFour = SAremovedIban.Substring(14, 4);
                    viewModel.IbanPartFive = SAremovedIban.Substring(18, 4);
                    //Bind Iban and remove name
                }
            }
            catch (Exception)
            {
            }
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
                        NewAccountPopPageViewModel.ValidTypeIban = viewModel.IbanNumberText;
                        MessagingCenter.Send<Object, string>(this, "IbanReceivedVATDeclaration", viewModel.IbanNumberText);
                        await MopupService.Instance.PopAsync();
                    }
                    else
                    {
                        NewAccountPopPageViewModel.ValidTypeIban = string.Empty;
                        viewModel.IsIBANValid = false;
                        if (viewModel.IbanNumberText == "SA")
                        {
                            MessagingCenter.Send<Object, string>(this, "IbanReceivedVATDeclaration", viewModel.IbanNumberText);
                            await MopupService.Instance.PopAsync();
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


        private void IbanAddButtonClicked(object sender, EventArgs e)
        {
            viewModel.IbanNumberText = string.Empty;
            viewModel.IbanNumberText = "SA" + viewModel.IbanPartOne + viewModel.IbanPartTwo + viewModel.IbanPartThree + viewModel.IbanPartFour + viewModel.IbanPartFive;
            Checked_IBAN();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {

            MopupService.Instance.PopAsync();

        }

        private void Closed_Tapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }

        private void IbanOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartOne != null && viewModel.IbanPartOne.Length >= 2)
                {
                    IbanTwo.Focus();
                }
            }
            catch (Exception)
            {
            }

        }

        private void IbanTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartTwo != null && viewModel.IbanPartTwo.Length >= 8)
                {
                    IbanThree.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void IbanThree_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartThree != null && viewModel.IbanPartThree.Length >= 4)
                {
                    IbanFour.Focus();
                }

            }
            catch (Exception)
            {
            }
        }

        private void IbanFour_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartFour != null && viewModel.IbanPartFour.Length >= 4)
                {
                    IbanFive.Focus();
                }

            }
            catch (Exception)
            {
            }
        }

        private void IbanFive_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartFive != null && viewModel.IbanPartFive.Length >= 4)
                {

                }

            }
            catch (Exception)
            {
            }
        }
        public void SetFocus()
        {
            try
            {
                if (viewModel.IbanPartOne != null && !(viewModel.IbanPartOne.Length == 2))
                {
                    IbanOne.Focus();
                }
                else if (viewModel.IbanPartTwo != null && !(viewModel.IbanPartTwo.Length == 8))
                {
                    IbanTwo.Focus();
                }
                else if (viewModel.IbanPartThree != null && !(viewModel.IbanPartThree.Length == 4))
                {
                    IbanThree.Focus();
                }
                else if (viewModel.IbanPartFour != null && !(viewModel.IbanPartFour.Length == 4))
                {
                    IbanFour.Focus();
                }
            }
            catch (Exception)
            {


            }
        }

    }
}