using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAccountPopUpPageView : PopupPage
    {
        NewAccountPopUpPageViewModel viewModel;
        public NewAccountPopUpPageView(String Iban)
        {
            InitializeComponent();
            viewModel = App.Locator.NewAccountPopUpPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            NewAccountPopUpPageViewModel.ValidTypeIban = string.Empty;
            this.BindingContext = viewModel;
            viewModel.IbanNumberText = Iban;
            if(string.IsNullOrEmpty(viewModel.IbanNumberText))
            {
                viewModel.AccountText = "New Account";
            }
            else
            {
                viewModel.AccountText = "Edit Account";
                viewModel.IbanPartOne= Iban.Substring(0, 2);
                viewModel.IbanPartTwo = Iban.Substring(2, 8);
                viewModel.IbanPartThree = Iban.Substring(9, 4);
                viewModel.IbanPartFour = Iban.Substring(13,4);
                viewModel.IbanPartFive = Iban.Substring(17, 4);
                //Bind Iban and remove name
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
                            MessagingCenter.Send<Object, string>(this, "IbanReceived", viewModel.IbanNumberText);
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            }
            catch (Exception ex)
            {
                viewModel.IsIBANValid = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
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
            catch (Exception gec)
            {
            }
        }

        private void IbanAddButtonClicked(object sender, EventArgs e)
        {
            viewModel.IbanNumberText = string.Empty;
            viewModel.IbanNumberText = "SA"+viewModel.IbanPartOne + viewModel.IbanPartTwo + viewModel.IbanPartThree + viewModel.IbanPartFour + viewModel.IbanPartFive;
            Checked_IBAN();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {
          
                PopupNavigation.Instance.PopAsync();
           
        }
    }
}