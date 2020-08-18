using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    public partial class TaxpayerProfileSuccessPage : ContentPage
    {
        TaxpayerProfileSuccessViewModel viewModel;
        int ProfileSuccessId;

        public TaxpayerProfileSuccessPage(int SuccessId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = App.Locator.TaxpayerProfileSuccessPage;
            this.BindingContext = viewModel;

            // * Update UI
            ProfileSuccessId = SuccessId;
            UpdateUI();
        }

        private void UpdateUI()
        {
            switch (ProfileSuccessId)
            {
                case 1:
                    viewModel.SuccessTitleLbl = "Email Updated";
                    viewModel.successCaptionLbl = "New Email Updated Successfully";
                    break;
                case 2:
                    viewModel.SuccessTitleLbl = "Mobile Number Updated";
                    viewModel.successCaptionLbl = "New Mobile Number Updated Successfully";
                    break;
                case 3:
                    viewModel.SuccessTitleLbl = "Password Updated";
                    viewModel.successCaptionLbl = "New Password Updated Sucessfully";
                    break;
                default:
                    break;
            }
        }

        void OnGoToProfileLblTapped(object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (ProfileSuccessId == 1)
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.VerificationPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigation.NavigationStack.ToList().Clear();
                    viewModel._navigationService.GoBack();
                }
                else { viewModel._navigationService.GoBack(); }
            });
        }
    }
}
