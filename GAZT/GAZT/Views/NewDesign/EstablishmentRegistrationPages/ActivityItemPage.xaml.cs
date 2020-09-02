using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
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
    }
}
