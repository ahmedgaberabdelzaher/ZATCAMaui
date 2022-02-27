using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Helper;
using GAZT.Manager;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
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
                    viewModel.IbanPartTwo= string.Empty;
                    viewModel.IbanPartThree= string.Empty;
                    viewModel.IbanPartFour= string.Empty;
                    viewModel.IbanPartFive= string.Empty;
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
                SetLTR();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
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
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        NewAccountPopPageViewModel.ValidTypeIban = string.Empty;
                        viewModel.IsIBANValid = false;
                        if (viewModel.IbanNumberText == "SA")
                        {
                            MessagingCenter.Send<Object, string>(this, "IbanReceivedVATDeclaration", viewModel.IbanNumberText);
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            }
            catch (Exception )
            {
                viewModel.IsIBANValid = false;
                Device.BeginInvokeOnMainThread(async () =>
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
                    this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
            }
            catch (Exception )
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

            PopupNavigation.Instance.PopAsync();

        }

        private void Closed_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
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
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
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
                //if (viewModel.IbanPartOne != null && viewModel.IbanPartOne.Length == 2)
                //{

                //}
                //else
                //{
                //    IbanOne.Focus();
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                //if (viewModel.IbanPartTwo != null && viewModel.IbanPartTwo.Length == 8)
                //{

                //}
                //else
                //{
                //    IbanTwo.Focus();
                //}
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
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
                //if (viewModel.IbanPartThree != null && viewModel.IbanPartThree.Length == 4)
                //{

                //}
                //else
                //{
                //    IbanThree.Focus();
                //}
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

        private void IbanFive_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.IbanPartFive != null && viewModel.IbanPartFive.Length >= 4)
                {
                    //IbanFour.Focus();
                }
                //if (viewModel.IbanPartFour != null && viewModel.IbanPartFour.Length == 4)
                //{

                //}
                //else
                //{
                //    IbanFour.Focus();
                //}
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

        private void IbanOneFocused(object sender, FocusEventArgs e)
        {
            try
            {
              //  SetFocus();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

        private void IbanTwoFocused(object sender, FocusEventArgs e)
        {
            try
            {
               // SetFocus();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

        private void IbanThreeFocused(object sender, FocusEventArgs e)
        {
            try
            {
                //SetFocus();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);

            }
        }

        private void IbanFourFocused(object sender, FocusEventArgs e)
        {
            try
            {
               // SetFocus();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

        private void IbanFiveFocused(object sender, FocusEventArgs e)
        {
            try
            {
              //  SetFocus();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
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
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }

    }
}