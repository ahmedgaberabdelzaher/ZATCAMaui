using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class ActivityItemPage : ContentPage
    {
        private ActivityNavigationModels _activityNavigation;
        private ActivityItemPageViewModel viewModel;
        public ActivityItemPage(ActivityNavigationModels activityNavigation)
        {
            InitializeComponent();
            _activityNavigation = activityNavigation;
            viewModel = App.Locator.ActivityItemPage;
            viewModel.taxPayerDetails = _activityNavigation.taxPayerDetails;
            viewModel.newNumber = _activityNavigation.nextNumber;
            viewModel.validateCR = _activityNavigation.validateCR;
            //viewModel.cRActivityItem = _activityNavigation.cRActivityItem;
            viewModel.goBackAction = _activityNavigation.goBackAction;
            viewModel.CurrentTab = _activityNavigation.openedTab;
            BindingContext = viewModel;
            SetLTR();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel?.OnDisappearing();
        }

        void CREntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            viewModel?.validateCRNumber();
        }

        async void CRSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            Console.WriteLine("CR Main " + CRMainActivity.IsOn);
            if (CRMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                var _mainItem = viewModel?.NregActivityList?.FirstOrDefault(i => i.Actcat == "M");
                if (_mainItem != null)
                {
                    var confirmPopup = new ZAKATOkCancelPopUpView("Are you sure you want to mark as main activity")
                    {
                        CloseWhenBackgroundIsClicked = false
                    };
                    confirmPopup.OnSelect = (str) =>
                    {
                        CRMainActivity.IsOn = str == "Yes";
                        if (str == "Yes") {
                            _mainItem.Actcat = "S";
                        }
                    };
                    await PopupNavigation.Instance.PushAsync(confirmPopup);
                }
            }
        }
        async void LicenseSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            Console.WriteLine("License Main " + LicenseMainActivity.IsOn);
            if (LicenseMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                var _mainItem = viewModel?.NregActivityList?.FirstOrDefault(i => i.Actcat == "M");
                if (_mainItem != null)
                {
                    var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };
                    confirmPopup.OnSelect = (str) =>
                    {
                        LicenseMainActivity.IsOn = str == "Yes";
                        if (str == "Yes")
                        {
                            _mainItem.Actcat = "S";
                        }
                    };
                    await PopupNavigation.Instance.PushAsync(confirmPopup);
                }
            }
        }
    }
}
