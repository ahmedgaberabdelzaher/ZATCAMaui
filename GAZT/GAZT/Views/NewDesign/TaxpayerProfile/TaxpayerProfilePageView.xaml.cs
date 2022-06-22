using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerProfilePageView : ContentPage
    {
        NewTaxpayerProfileViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public TaxpayerProfilePageView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            viewModel = App.Locator.TaxpayerProfilePageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();

            TpProfileTaxpaayertypeRefresh();
            try
            {
                viewModel.ResidenceText = App.TP.TpType;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

            // * Page content direction
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                MobileNumberCodeEntry.HorizontalTextAlignment = TextAlignment.End;
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                MobileNumberCodeEntry.HorizontalTextAlignment = TextAlignment.Start;
            }
        }


        public async void TpProfileTaxpaayertypeRefresh()
        {
            try
            {

                viewModel.IsLoading = true;
                TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(App.TP.Tin);

                if (TPProfile != null)
                {
                    //if ((0 == string.Compare("Registration is pending", TPProfile.TpType)))
                    //{
                    //    throw new GAZTRegistrationPendingException();
                    //}
                    if (App.TP != null)
                    {
                        App.TP.TpType = TPProfile.TpType;
                        viewModel.ResidenceText = App.TP.TpType;
                    }

                }
                viewModel.IsLoading = false;
            }
            catch
            {
                viewModel.IsLoading = false;
            }
        }

        private void OnMobileEditTapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;

            try
            {
                if (mobileData == null)
                    mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();

                viewModel.IsLoading = false;
                PopupNavigation.Instance.PushAsync(new UpdateMobilePopUp(mobileData));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
                viewModel.IsLoading = false;
            }
        }

        private void OnEmailEditTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UpdateEmailPopUp());
        }

        private void OnPasswordEditTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UpdatePasswordPopUp());
        }

        private void OnBackArrowBtnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.GoBack();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            try
            {


                if (App.TP != null)
                {
                    //viewModel.TPProfileNameLbl = App.TP.Name;

                    if (App.TP.TypeChk == "X")
                    {
                        viewModel.TPProfileNameLbl = App.TP.NameFirst + " " + App.TP.NameLast;
                    }
                    else
                    {
                        viewModel.TPProfileNameLbl = App.TP.NameOrg1;
                    }

                    viewModel.TINLabel = App.TP.Tin;
                    try
                    {
                        if (!string.IsNullOrEmpty(App.TP.Mobile))
                        {
                            if (App.TP.Mobile.Length < 12)
                                viewModel.MobileNumber = "+966" + App.TP.Mobile.Remove(0, 2);
                            else
                                viewModel.MobileNumber = "+" + App.TP.Mobile.Remove(0, 2);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }

                
                    viewModel.EmailEntry = App.TP.Email;
                    viewModel.PasswordEntry = "********";

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel.GetTinStatusDATA();
                    });


                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
    }
}