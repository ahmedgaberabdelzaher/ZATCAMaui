using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerProfilePageView : ContentPage
    {
        NewTaxpayerProfileViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public TaxpayerProfilePageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            viewModel = App.Locator.TaxpayerProfilePageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.ResidenceText = App.TP.TpType;

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

        private void OnMobileEditTapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;

            try
            {
                if( mobileData == null )
                    mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();

                viewModel.IsLoading = false;
                PopupNavigation.Instance.PushAsync(new UpdateMobilePopUp(mobileData));
            }
            catch (Exception ex)
            {
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

            if (App.TP != null)
            {
                viewModel.TPProfileNameLbl = App.TP.Name;
                viewModel.TINLabel = App.TP.Tin;

                if (App.TP.Mobile.Length < 12)
                    viewModel.MobileNumber = "+966" + App.TP.Mobile.Remove(0, 2);
                else
                    viewModel.MobileNumber = "+" + App.TP.Mobile.Remove(0, 2);

                viewModel.EmailEntry = App.TP.Email;
                viewModel.PasswordEntry = "********";
            }
        }
    }
}