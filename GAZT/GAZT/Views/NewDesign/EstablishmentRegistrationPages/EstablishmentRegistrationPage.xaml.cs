using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
﻿using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentRegistrationPage : ContentPage
    {
        EstablishmentRegistrationPageViewModel viewModel;
        public EstablishmentRegistrationPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.EstablishmentRegistrationPage;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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

        async void dOBDateClicked(System.Object sender, System.EventArgs e)
        {

            //GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            //genericPickerModel.PickerTitle = "DateofBirthType";
            //genericPickerModel.PickerId = "DateofBirthTypePicker";
            //try
            //{
            //    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            //}
            //catch (GAZTUnlockAccountException ex)
            //{

            //}
            //catch (InternetException ex)
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
            //        viewModel._navigationService.GoBack();
            //    });
            //}


        }

        private void PassportIssueDateClicked(object sender, EventArgs e)
        {

        }

        private void PassportExpiryDateClicked(object sender, EventArgs e)
        {

        }
    }
}
