using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class OutletDetailsPageView : ContentPage
    {
        private OutletDetailsPageViewModel viewModel;
        private OutletNavigationModels _outletNavigation;
        public OutletDetailsPageView(OutletNavigationModels outletNavigation)
        {
            InitializeComponent();
            _outletNavigation = outletNavigation;
            viewModel = App.Locator.OutletDetailsPageView;
            BindingContext = viewModel;
            SetLTR();
        }
        private void SetLTR()
        {

            //if (!App.IsArabic)
            //{
                this.FlowDirection = FlowDirection.LeftToRight;
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.RightToLeft;
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
            {
                viewModel.currentTab = _outletNavigation.openedTab;
            }
        }
    }
}
